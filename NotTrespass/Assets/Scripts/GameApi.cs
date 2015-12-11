using UnityEngine;
using System;
using System.Net;
using Newtonsoft.Json;
using SimpleJSON;
using System.Collections.Generic;

/// <summary>
/// Data holder for games
/// </summary>
class GameList
{
    public List<Game> Games;
}

/// <summary>
/// API for the network interface
/// </summary>
public class GameApi {

	//private const String API_BASE = "http://143.215.206.36:5000";
    private const String API_BASE = "http://128.61.105.217:5000";
	private  const String GAME_RES = "game";
	private  const String GAMES_RES = "games";
	private  const String USERS_RES = "users";
	private  const String FRIENDS_RES = "friends";
	private  const String USER_RES = "user";

	public String User { get; private set; }

    /// <summary>
    /// Initializes API for a user
    /// </summary>
    /// <param name="user"></param>
    private GameApi(String user) {
		this.User = user;
	}
    public static GameApi getInstance(String user, String password) {
        return new GameApi(user);
    }

    /// <summary>
    /// Get list of games for a certain user
    /// </summary>
    /// <param name="user"></param>
    /// <returns>
    /// List of all games for a user
    /// </returns>
    public List<Game> GetGames(String user) {
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAMES_RES, USER_RES, user}));
        while (!www.isDone) {  }
        
        //var gameArray = JSON.Parse(www.text)["games"];
        Debug.Log(www.text);
        GameList games = JsonConvert.DeserializeObject<GameList>(www.text);
        return games.Games;
        
	}

    /// <summary>
    /// Updates game state for a game
    /// </summary>
    /// <param name="id">
    /// The id of the game to update
    /// </param>
    /// <returns>
    /// The updated game
    /// </returns>
    public Game updateGameState(int id) {
        //string json = JsonConvert.SerializeObject (g);
		//WWWForm form = new WWWForm ();
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAME_RES, id + ""}));
        //time out
        //TODO- make this better
        while (!www.isDone) { }

		return  JsonConvert.DeserializeObject<Game>(www.text);
    }

    /// <summary>
    /// Updates a game from a game
    /// </summary>
    /// <param name="g">
    /// The new game instance
    /// </param>
    /// <returns>
    /// The updated game after network confirmation
    /// </returns>
    public Game updateGameState(Game g) {
        string json = JsonConvert.SerializeObject (g);
		WWWForm form = new WWWForm ();
		form.AddField ("game", json);
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, GAME_RES, g.GameID + ""}), form);
		while (!www.isDone) {}

		return  JsonConvert.DeserializeObject<Game>(www.text);
    }

    /// <summary>
    /// Gets game state
    /// </summary>
    /// <param name="gameId">
    /// The game id to get the game state for
    /// </param>
    /// <returns>
    /// The game state
    /// </returns>
    public Game getGameState(int gameId) {
		WebClient wc = new WebClient ();
		Console.WriteLine (wc.DownloadString (String.Join ("/", new string[] { API_BASE, GAME_RES, "" + gameId })));

		return null;
    }

    /// <summary>
    /// Creates a new game from a game object
    /// </summary>
    /// <param name="game">
    /// The new game
    /// </param>
    /// <returns>
    /// The new game's gameid
    /// </returns>
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


    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="name">
    /// new user's name
    /// </param>
    public void createNewUser(String name) {
        WWWForm form = new WWWForm();
        form.AddField("username", name);
        WWW www = new WWW(String.Join ("/",
		                               new string[] { API_BASE, USERS_RES, name}), form);

    }


    /// <summary>
    /// Searches users..
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Adds a friend for the current user
    /// </summary>
    /// <param name="username">
    /// The friend to add
    /// </param>
	public void AddFriend(String username) {
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		WWW www = new WWW(String.Join ("/", new string[] { API_BASE, FRIENDS_RES, this.User}), form);
	}

    /// <summary>
    /// Removes a friend for the current user
    /// </summary>
    /// <param name="username">
    /// The friend toremove
    /// </param>
	public void RemoveFriend(String username) {
		WebRequest req = WebRequest.Create (String.Join ("/", new string[] { API_BASE, FRIENDS_RES, this.User, username}));
		req.Method = "DELETE";
		Debug.Log (req.GetResponse ());
	}

    /// <summary>
    /// Gets friends for current user
    /// </summary>
    /// <returns></returns>
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
