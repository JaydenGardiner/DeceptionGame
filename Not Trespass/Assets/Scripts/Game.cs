using System;

public class Game {
    public enum Status { PENDING, PLAYING, COMPLETED };
    public Status GameStatus { get; private set; }
    public Nullable<Int32> GameID { get; private set; }
    public int[][] Board { get; private set; }

    // The following return usernames 
    public String Player1 { get; private set; }
    public String Player2 { get; private set; }
    public String CurrentMove { get; private set; }
    
	public Game() {
	}
}
