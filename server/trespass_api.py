from flask import Flask, jsonify
from flask_restful import Resource, Api, reqparse

import json

app = Flask(__name__)
api = Api(app)


parser= reqparse.RequestParser()
parser.add_argument('username')
parser.add_argument('game')

COMPLETED_GAME_STATUS = "completed"
###
### MOCK DATA
###

PENDING = 0
PLAYING = 1
COMPLETED = 2

users = {
   "thomas": {
        "friends": ["jayden"]
   },
   "jayden": {
        "friends": ["thomas"]
   },
   "david": {
        "friends": []
   },
   "kevin": {
        "friends": []
   },
   "pranav": {
        "friends": []
   }
}


games = [
#    {
#        u'GameStatus' : PENDING,
#        u'GameID': 0,
#        u'Board': [
#            [1, 1, 2, 1, 1],
#            [1, 1, 1, 1, 1],
#            [0, 0, 0, 0, 0],
#            [0, 0, 0, 0, 0],
#            [3, 3, 3, 3, 3],
#            [3, 3, 3, 3, 3]
#        ],
#
#        u'Player1': u'thomas',
#        u'Player2': u'jayden',
#        u'CurrentMove': u'', # Player 2's turn. This will be the state when the game is pending
#        u'Winner': ""
#    }
]


# TODO: this will get loaded from the current auth session
#current_user = "thomas"

def error(msg):
    return jsonify({"error": msg})
def info(msg):
    return jsonify({"result": msg})

class FriendList(Resource):
    def get(self, current_user):
        return jsonify({"friends": users[current_user]['friends']})
    def post(self, current_user):
        user=parser.parse_args()["username"]
        if user not in users:
            return error("Invalid user")
        if user in users[current_user]["friends"]:
            return info("Already friends with this person")
        if user == current_user:
            return error("You can never befriend your inner self.")
        users[current_user]["friends"].append(user)
        return info("Success")

#class Friend(Resource):
#    def delete(self, user):
#        if user not in users:
#            return error("Invalid user")
#        if user not in users[current_user]["friends"]:
#            return info("Not even friends with this person")
#        if user == current_user:
#            return error("You can never unfriend your inner self.")
#        users[current_user]["friends"].remove(user)
#        return info("Success")


class GameList(Resource):
    def get(self):
        return {"games": games}
    def post(self):
        game=json.loads(parser.parse_args()["game"])

        #technically non-atomic race condition but whatever
        game["GameID"] = len(games)
        games.append(game);
        print game
        return jsonify({"id": game["GameID"]})

    #TODO: post game
        #TODO:

class UserGames(Resource):
    def get(self, username):
        return jsonify({"games": [game for game in games if game["Player1"] == username or game["Player2"] == username]})

class Game(Resource):
    def get(self, gameId):
        if gameId < 0 or gameId >= len(games):
            return error("No such game")
        return jsonify(games[gameId])
    def post(self, gameId):
        game = json.loads(parser.parse_args()["game"])

        #if game["status"] == "pending":
            # TODO: check valid new secret number
        #else
            # TODO: Don't accept new secret number

        game["CurrentMove"] = game["Player1"] if game["CurrentMove"] == game["Player2"] else game["Player2"]

        # TODO: check board state

        game_id = game["GameID"];
        if game_id < 0 or game_id >= len(games):
            return error("No such game")

        if 4 in game["Board"][0]:
            game["GameStatus"] = COMPLETED
            game["Winner"] = game["Player2"]
        if 2 in game["Board"][-1]:
            game["GameStatus"] = COMPLETED
            game["Winner"] = game["Player1"]

        games[game["GameID"]] = game

        return jsonify(game)

class UserList(Resource):
    def get(self):
        return {"users": users}

    def post(self):
        username=parser.parse_args()["username"]
        if username in users:
            return error("User already exists")
        users[username] = {
            "friends": []
        }
        return info("Success")

class UserSearch(Resource):
    def get(self, userQuery):
        return jsonify({"users": [user for user in users if userQuery in user]})



class User(Resource):
    def get(self, userId):
        return users[userId]
    #TODO: put update user

api.add_resource(FriendList, '/friends/<string:current_user>')
#api.add_resource(Friend, '/friends/<string:user>')
api.add_resource(UserList, '/users/')
api.add_resource(UserSearch, '/users/search/<string:userQuery>')
api.add_resource(User, '/user/<int:userId>')
api.add_resource(GameList, '/games/')
api.add_resource(Game, '/game/<int:gameId>')
api.add_resource(UserGames, '/games/user/<string:username>')


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
