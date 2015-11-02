public class Game {
    public enum Status { PENDING, PLAYING, COMPLETED };
    public Status Status { get; public set; }
    public Nullable<Int32> GameID { get; public set; }
    public int[][] Board { get; public set; }

    // The following return usernames 
    public String Player1 { get; public set; }
    public String Player2 { get; public set; }
    public String CurrentMove { get; public set; }
    
	public Game() {
	}
}
