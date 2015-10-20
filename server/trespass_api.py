from flask import Flask
from flask_restful import Resource, Api

app = Flask(__name__)
api = Api(app)

###
### MOCK DATA
###
users = [
    {
        "name": "Thomas Shields",
        "friends": [1]
    },
    {
        "name": "Jayden Gardiner",
        "friends": [0, 2]
    },
    {
        "name": "Pranav Shenoy",
        "friends": [1]
    }
]

games = [
    {
        "player1": 0,
        "player2": 2,
        "turn": 2 # Player 2's turn. This will be the state when the game is pending
        "board": [
            [0, 1, 2, 1], 
            [1, 1, 1, 0],
            [0, 0, 0, 0],
            [3, 3, 3, 3],
            [0, 0, 4, 3]
        ]
    }
]

# TODO: this will get loaded from the current auth session
current_user_id = 1

def error(msg):
    return {"error": msg}
def info(msg):
    return {"result": msg}

class FriendList(Resource):
    def get(self):
        return {"friends": [users[i] for i in users[current_user_id]['friends']]}
    def post(self, user_id):
        if user_id < 0 or user_id >= len(users):
            return error("Invalid user id")
        if user_id in users[current_user_id]["friends"]:
            return info("Already friends with this person")
        if user_id == current_user_id:
            return error("You can never befriend your inner self.")
        users[current_user_id]["friends"].append(user_id)
            return info("Success")
    def delete(self, user_id):
        if user_id < 0 or user_id >= len(users):
            return error("Invalid user id")
        if user_id not in users[current_user_id]["friends"]:
            return info("Not even friends with this person")
        if user_id == current_user_id:
            return error("You can never unfriend your inner self.")
        users[current_user_id]["friends"].remove(user_id)
            return info("Success")


class GameList(Resource):
    def get(self):
        return {"games": games}
    def post(self, player1, player2):
    #TODO: post game
        #TODO:

class Game(Resource):
    def get(self, gameId):
        return games[gameId]
    def put(self, game_id, board_state):
        if game_id < 0 or game_id >= len(games):
            return error("Invalid game id")
        games[game_id]["turn"] = 2 if games[game_id][turn] == 1 else 1
        games[game_id]["board"] = board_state;
        # TODO: check board state
       
class UserList(Resource):
    def get(self):
        return {"users": users}
    #TODO: post new user

class User(Resource):
    def get(self, userId):
        return users[userId]
    #TODO: put update user

api.add_resource(FriendList, '/friends')
api.add_resource(UserList, '/users')
api.add_resource(User, '/user/<int:userId>')
api.add_resource(GameList, '/games')
api.add_resource(Game, '/game/<int:gameId>')

if __name__ == '__main__':
    app.run(debug=True)
