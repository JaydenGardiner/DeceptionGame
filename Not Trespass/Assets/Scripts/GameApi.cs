using UnityEngine;
using System;
using System.Net;
using SimpleJSON;


public class GameApi {
	private const String API_BASE = "http://localhost:5000";
	private  const String GAME_RES = "game";
	private  const String USERS_RES = "users";
	private  const String FRIENDS_RES = "friends";
	private  const String USER_RES = "user";

    private GameApi() { }
    public static GameApi getInstance(String user, String password) {
        return new GameApi();
    }

    
    public void updateGameState(Game g) {
        	
    }

    public Game getGameState(int gameId) {
		WebClient wc = new WebClient ();
		Console.WriteLine (wc.DownloadString (String.Join ("/", new string[] { API_BASE, GAME_RES, "" + gameId })));
       
		return null;
    }

    
 //   public Game createNewGame(Game game) {
        String boardString = "[";
//        for (int x = 0; x < board.Length; x++) {
//            boardString += "[";
//            for (int y = 0; y < board[x].Length; y++) {
//                if (board[x, y].Piece == null) {
//                    boardString += 0;
//                } else {
//                    boardString += (2 * board[x, y].Piece.Team) + 1
//                        + board[x, y].Piece.IsSecret ? 1 : 0;
//                }
//                boardString += ",";
//            }
//            boardString += "],";
//        }
//        boardString = boardString.trim(",");
	//}
        


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
		WWW www = new WWW(String.Join ("/",
		                               new string[] { API_BASE, FRIENDS_RES, ""}), form);

	}

	public string[] GetFriends() {
		WWW www = new WWW(String.Join("/", new string[] { API_BASE, FRIENDS_RES}));
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
