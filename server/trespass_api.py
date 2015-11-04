from flask import Flask, jsonify
from flask_restful import Resource, Api, reqparse

app = Flask(__name__)
api = Api(app)


parser= reqparse.RequestParser()
parser.add_argument('username')
parser.add_argument('game')

COMPLETED_GAME_STATUS = "completed"
###
### MOCK DATA
###
users = {
   "thomas": {
        "friends": ["jayden"]
   },
   "jayden": {
        "friends": []
   },
   "bobby": {
        "friends": []
   }
}


games = [
    {
        "player1": "thomas",
        "player2": "jayden",
        "turn": 2, # Player 2's turn. This will be the state when the game is pending
        "board": [
            [0, 1, 2, 1, 1], 
            [0, 1, 1, 1, 1], 
            [0, 0, 1, 1, 0], 
            [0, 0, 3, 3, 0], 
            [3, 3, 3, 3, 3],
            [0, 0, 4, 3, 3]
        ]
    }
]


# TODO: this will get loaded from the current auth session
current_user = "thomas"

def error(msg):
    return jsonify({"error": msg})
def info(msg):
    return jsonify({"result": msg})

class FriendList(Resource):
    def get(self):
        return jsonify({"friends": users[current_user]['friends']})
    def post(self):
        user=parser.parse_args()["username"]
        if user not in users: 
            return error("Invalid user")
        if user in users[current_user]["friends"]:
            return info("Already friends with this person")
        if user == current_user:
            return error("You can never befriend your inner self.")
        users[current_user]["friends"].append(user)
        return info("Success")

class Friend(Resource):    
    def delete(self, user):
        if user not in users: 
            return error("Invalid user")
        if user not in users[current_user]["friends"]:
            return info("Not even friends with this person")
        if user == current_user:
            return error("You can never unfriend your inner self.")
        users[current_user]["friends"].remove(user)
        return info("Success")


class GameList(Resource):
    def get(self):
        return {"games": games}
    def post(self):
        game=parser.parse_args()["game"]
        print game
        games.append(game);
        return jsonify({"id": len(games) - 1})

    #TODO: post game
        #TODO:

class Game(Resource):
    def get(self, gameId):
        return games[gameId]
    def get(self, username):
        return [games[gameId] for gameId in games  if games[gameId]["status"] != COMPLETED_GAME_STATUS]
    def put(self, game_id, board_state):
        if game_id < 0 or game_id >= len(games):
            return error("Invalid game id")

        game = games[game_id]

        if game["status"] == "pending":
            # TODO: check valid new secret number
           game["board"] = board_state;
            # TODO change status of game


        game["turn"] = game["player1"] if game[turn] == game["player2"] else game["player2"] 
        # Don't accept new secret number
        game["board"] = board_state;
        # TODO: check board state
       
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

api.add_resource(FriendList, '/friends/')
api.add_resource(Friend, '/friends/<string:user>')
api.add_resource(UserList, '/users/')
api.add_resource(UserSearch, '/users/search/<string:userQuery>')
api.add_resource(User, '/user/<int:userId>')
api.add_resource(GameList, '/games/')
api.add_resource(Game, '/game/<int:gameId>')


if __name__ == '__main__':
    app.run(debug=True, host='0.0.0.0')
