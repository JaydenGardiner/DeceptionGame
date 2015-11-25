using System;

public class Game {
    public enum Status { PENDING, PLAYING, COMPLETED };


    public String CurrentMove;
    public int[][] Board;
    public Nullable<Int32> GameID;
    public Status GameStatus;
    public String Player1;
    public String Player2;
    public String Winner;
	private readonly int[][] DefaultBoard = {
		new int[] {1, 1, 1, 1, 1},
		new int[] {1, 1, 1, 1, 1},
		new int[] {0, 0, 0, 0, 0},
		new int[] {0, 0, 0, 0, 0},
		new int[] {3, 3, 3, 3, 3},
		new int[] {3, 3, 3, 3, 3}
	};


    // The following return usernames 
    
    
    
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

    public String getOtherPlayer(String player)
    {
        if (player == Player1) return Player2;
        else if (player == Player2) return Player1;
        else throw new System.ArgumentException("invalid player name");
    }

}
