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
        "status": "pending"
    }
]

# TODO: this will get loaded from the current auth session
current_user_id = 1


class FriendList(Resource):
    def get(self):
        return {"friends": [users[i] for i in users[current_user_id]['friends']]}
    #TODO: post friend

class GameList(Resource):
    def get(self):
        return {"games": games}
    #TODO: post game

class Game(Resource):
    def get(self, gameId):
        return games[gameId]
    #TODO: put game updates
        
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
