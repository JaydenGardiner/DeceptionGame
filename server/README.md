#Setup
To setup the server, you'll need to install [Flask](http://flask.pocoo.org/docs/0.10/installation/) and [flask-restful](https://flask-restful.readthedocs.org/en/0.3.4/installation.html#installation)

The preferred way to do this is by setting up a virtualenv:

        virtualenv env
        source env/bin/activate
        pip install Flask
        pip install flask-restful

#Run
Once you've install dependencies, run the server on the dev machine with:

        python trespass_api.py
