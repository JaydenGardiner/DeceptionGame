using System;
using System.Net;

public class RemoteApi {
	private const String API_BASE = "http://localhost:5000";
	private  const String GAME_RES = "game";

    private RemoteApi() { }
    public static RemoteApi getInstance(String user, String password) {
        return new RemoteApi();
    }

    
    public void updateGameState(int gameId, Piece[,] pieces) {
	
    }

    public Piece[,] getGameState(int gameId) {
		WebClient wc = new WebClient ();
		Console.WriteLine (wc.DownloadString (String.Join ("/", new string[] { API_BASE, GAME_RES, "" + gameId })));
       
		return null;
    }
   
}
