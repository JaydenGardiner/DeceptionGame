using System;

public class Game {
    public enum Status { PENDING, PLAYING, COMPLETED };
    public Status GameStatus { get; private set; }
    public Nullable<Int32> GameID { get; private set; }
    public int[][] Board;

	private readonly int[][] DefaultBoard = {
		new int[] {1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 1, 1},
		new int[] {0, 0, 0, 0, 0},
		new int[] {0, 0, 0, 0, 0},
		new int[] {3, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3}
	};


    // The following return usernames 
    public String Player1 { get; private set; }
    public String Player2 { get; private set; }
    public String CurrentMove { get; private set; }
    
	public Game(String Player1, String Player2) {
		GameStatus = Status.PENDING;
		this.Player1 = Player1;
		this.Player2 = Player2;
		this.Board = (int[][]) DefaultBoard.Clone ();
	}

	public void SetSecretNumber(int pieceNum) {
		// TODO: update this to work for player 2 as well
		Board [(1 - (pieceNum / 5))] [pieceNum % 5] = 2;
	}

}
