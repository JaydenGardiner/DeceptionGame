from flask import Flask
from flask_restful import Resource, Api

app = Flask(__name__)
api = Api(app)


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
   }
}


games = [
    {
        "player1": "thomas",
        "player2": "jayden",
        "turn": 2 # Player 2's turn. This will be the state when the game is pending
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
    return {"error": msg}
def info(msg):
    return {"result": msg}

class FriendList(Resource):
    def get(self):
        return {"friends": users[current_user]['friends']}
    def post(self, user):
        if user not in users: 
            return error("Invalid user")
        if user in users[current_user]["friends"]:
            return info("Already friends with this person")
        if user == current_user:
            return error("You can never befriend your inner self.")
        users[current_user]["friends"].append(user)
            return info("Success")
    # TODO: update this 
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
    def post(self, game):
        games.append(game);
    }

    #TODO: post game
        #TODO:

class Game(Resource):
    def get(self, gameId):
        return games[gameId]
    def get(self, username):
        return [games[gameId] if games[gameId]["status"] != COMPLETED_GAME_STATUS for gameId in games]
    def put(self, game_id, board_state):
        if game_id < 0 or game_id >= len(games):
            return error("Invalid game id")

        if game[game_id]["status"] == "pending":
            # TODO: check valid new secret number
            games[game_id]["board"] = board_state;
            # GAME status change

        games[game_id]["turn"] = 2 if games[game_id][turn] == 1 else 1
        # Don't accept new secret number
        games[game_id]["board"] = board_state;
        # TODO: check board state
       
class UserList(Resource):
    def get(self):
        return {"users": users}
    def post(self, username):
        users.append({
            name: username,
            friends: []
        })


class User(Resource):
    def get(self, userId):
        return users[userId]
    def get(self, userQuery):
        return {"users": [user if userQuery in user for user in users]}
    #TODO: put update user

api.add_resource(FriendList, '/friends')
api.add_resource(UserList, '/users')

api.add_resource(User, '/user/<int:userId>')
api.add_resource(User, '/user/<str:userQuery>')

api.add_resource(GameList, '/games')
api.add_resource(Game, '/game/<int:gameId>')

if __name__ == '__main__':
    app.run(debug=True)
