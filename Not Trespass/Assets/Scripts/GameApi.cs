using System;

public class RemoteApi {
    private RemoteApi() { }
    public static RemoteApi getInstance(String user, String password) {
        return new RemoteApi();
    }

    
    public void updateGameState(int gameId, Piece[,] pieces) {
    
    }

    public Piece[,] getGameState(int gameId) {
        return null;
    }

    
}
