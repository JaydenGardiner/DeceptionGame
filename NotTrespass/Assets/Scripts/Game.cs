using System;
using System.Collections.Generic;

public class Game {
    /// <summary>
    /// The current game status.
    /// Pending = waiting for players to accept
    /// Playing = Both players accepted and possible made moves
    /// Completed = One player has won
    /// </summary>
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
    public int[] LastMoved = {0, 0};
    public List<int[]> Player1Marked = new List<int[]>();
    public List<int[]> Player2Marked = new List<int[]>();


    // The following return usernames 
    
    
    /// <summary>
    /// Create a game between 2 players
    /// </summary>
    /// <param name="Player1">
    /// </param>
    /// <param name="Player2"></param>
	public Game(String Player1, String Player2) {
		GameStatus = Status.PENDING;
		this.Player1 = Player1;
		this.Player2 = Player2;
		this.Board = (int[][]) DefaultBoard.Clone ();
	}

    /// <summary>
    /// Sets secret piece for a team
    /// </summary>
    /// <param name="pieceNum">
    /// The secret piece
    /// </param>
    /// <param name="team">
    /// Which team we are setting the piece for
    /// </param>
	public void SetSecretNumber(int pieceNum, int team) {
		// TODO: update this to work for player 2 as well
        int row = 1 - (pieceNum / 5);
		Board [team == 1 ? 5 - row: row] [pieceNum % 5] = 2 + (team * 2);
	}

    /// <summary>
    /// Gets the other player from a player in the game
    /// </summary>
    /// <param name="player">
    /// The player we know
    /// </param>
    /// <returns>
    /// The other player in the game
    /// </returns>
    public String getOtherPlayer(String player)
    {
        if (player == Player1) return Player2;
        else if (player == Player2) return Player1;
        else throw new System.ArgumentException("invalid player name");
    }

    /// <summary>
    /// Determine if 2 Games are equal
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(System.Object obj)
    {
        if (obj == null) return false;

        Game g = obj as Game;
        if ((System.Object)g == null) return false;

        return CurrentMove == g.CurrentMove && BoardsEqual(Board, g.Board) 
            && GameID == g.GameID && GameStatus == g.GameStatus 
            && Player1 == g.Player1 && Player2 == g.Player2;
    }

    public static bool BoardsEqual(int[][] b1, int[][] b2) {
        if (b1.Length != b2.Length) return false;

        for (int i = 0; i < b1.Length; i++) {
            if (b1[i].Length != b2[i].Length) return false;
            for (int j = 0; j < b1[i].Length; j++) {
                if (b1[i][j] != b2[i][j]) return false;
            }
        }

        return true;
    }

}
