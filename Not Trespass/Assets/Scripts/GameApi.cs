using System;
using System.Net;
using System.Web.Helpers;

public class RemoteApi {
	private const String API_BASE = "http://localhost:5000";
	private  const String GAME_RES = "game";
	private  const String USER_RES = "user";

    private RemoteApi() { }
    public static RemoteApi getInstance(String user, String password) {
        return new RemoteApi();
    }

    
    public void updateGameState(Game g) {
        	
    }

    public Game getGameState(int gameId) {
		WebClient wc = new WebClient ();
		Console.WriteLine (wc.DownloadString (String.Join ("/", new string[] { API_BASE, GAME_RES, "" + gameId })));
       
		return null;
    }

    
    public Game createNewGame(Game game) {
        String boardString = "[";
        for (int x = 0; x < board.Length; x++) {
            boardString += "[";
            for (int y = 0; y < board[x].Length; y++) {
                if (board[x, y].Piece == null) {
                    boardString += 0;
                } else {
                    boardString += (2 * board[x, y].Piece.Team) + 1
                        + board[x, y].Piece.IsSecret ? 1 : 0;
                }
                boardString += ",";
            }
            boardString += "],";
        }
        boardString = boardString.trim(",");
	}
        


    public void createNewUser(String name) {
		WebClient wc = new WebClient ();
        // TODO POST
		wc.DownloadString (String.Join ("/", new string[] { API_BASE, USER_RES, name}));

    }
}
