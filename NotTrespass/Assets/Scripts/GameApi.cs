using UnityEngine;
using System;
using System.Net;
using Newtonsoft.Json;
using SimpleJSON;


public class GameApi {
	private const String API_BASE = "http://143.215.206.36:5000";
	private  const String GAME_RES = "game";
	private  const String GAMES_RES = "games";
	private  const String USERS_RES = "users";
	private  const String FRIENDS_RES = "friends";
	private  const String USER_RES = "user";

	public String User { get; private set; }

    private GameApi(String user) {
		this.User = user;
	}
    public static GameApi getInstance(String user, String password) {
        return new GameApi(user);
    }

    public Game[] GetGames(String user) {
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAME_RES, USER_RES, user}));
		while (!www.isDone) {}

		var gameArray = JSON.Parse (www.text) ;
		Game[] games = new Game[gameArray.Count];
		for (int i = 0; i < gameArray.Count; i++) {
			games[i] = JsonConvert.DeserializeObject<Game>(gameArray[i]);
		}
		Debug.Log (games);
		return games;

	}

    public Game updateGameState(int id) {
        //string json = JsonConvert.SerializeObject (g);
		//WWWForm form = new WWWForm ();
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAMES_RES, id + ""}));
		while (!www.isDone) {}

		return  JsonConvert.DeserializeObject<Game>(www.text);
    }


    public Game updateGameState(Game g) {
        string json = JsonConvert.SerializeObject (g);
		WWWForm form = new WWWForm ();
		form.AddField ("game", json);
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAME_RES, g.GameID + ""}), form);
		while (!www.isDone) {}

		return  JsonConvert.DeserializeObject<Game>(www.text);
    }

    public Game getGameState(int gameId) {
		WebClient wc = new WebClient ();
		Console.WriteLine (wc.DownloadString (String.Join ("/", new string[] { API_BASE, GAME_RES, "" + gameId })));

		return null;
    }

    public int CreateNewGame(Game game) {
		string json = JsonConvert.SerializeObject (game);
		WWWForm form = new WWWForm ();
		form.AddField ("game", json);
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAMES_RES, ""}), form);
		while (!www.isDone) {}

		int gameId = JSON.Parse (www.text) ["id"].AsInt;
		Debug.Log (gameId);
		return gameId;
	}



    public void createNewUser(String name) {
        WWWForm form = new WWWForm();
        form.AddField("username", name);
        WWW www = new WWW(String.Join ("/",
		                               new string[] { API_BASE, USERS_RES, name}), form);

    }

    public string[] SearchUsers(String query) {
        WWW www = new WWW(String.Join("/", new string[] { API_BASE, USERS_RES,
            "search", query }));
		while (!www.isDone) {}

		var userArray = JSON.Parse (www.text) ["users"];
		string[] users = new string[userArray.Count];
		for (int i = 0; i < userArray.Count; i++) {
			users[i] = userArray[i];
		}
		Debug.Log (users);
		return users;

    }

	public void AddFriend(String username) {
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		WWW www = new WWW(String.Join ("/", new string[] { API_BASE, FRIENDS_RES, this.User}), form);
	}

	public void RemoveFriend(String username) {
		WebRequest req = WebRequest.Create (String.Join ("/", new string[] { API_BASE, FRIENDS_RES, this.User, username}));
		req.Method = "DELETE";
		Debug.Log (req.GetResponse ());
	}

	public string[] GetFriends() {
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, FRIENDS_RES, this.User}));
		while (!www.isDone) {}

		var userArray = JSON.Parse (www.text) ["friends"];
		string[] users = new string[userArray.Count];
		for (int i = 0; i < userArray.Count; i++) {
			users[i] = userArray[i];
		}
		Debug.Log (users);
		return users;

	}
}
