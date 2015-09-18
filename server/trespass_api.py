from flask import Flask
from flask_restful import Resource, Api

app = Flask(__name__)
api = Api(app)

users = [
    "thomas",
    "pranav",
    "david",
    "jayden",
    "kevin"
]

class UserList(Resource):
    def get(self):
        return {"users": users}

api.add_resource(UserList, '/users')

if __name__ == '__main__':
    app.run(debug=True)
