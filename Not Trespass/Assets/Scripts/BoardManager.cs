using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BoardManager : MonoBehaviour {

    //Prefab for instantiating piece, set in scene
    public GameObject PiecePrefab;

    //this allows me to set the tiles up in the scene and access them from code
    public GameObject[] tiles;

    //Get these from other stuff, just initialising for now.
    //public GamePlayer Player1;
    //public GamePlayer Player2;

    public int Player1Secret;
    public int Player2Secret;

    //the currently selected piece
    public Piece currentPiece;

    //Current team
    public int CurrentTeam;


    /* In row column order where the rows go downwards
     * 5 6 7 8 9  r=0
     * 0 1 2 3 4  r=1
     * - - - - -  r=2
     * - - - - -  r=3
     * 0 1 2 3 4  r=4
     * 5 6 7 8 9  r=5
     */
    //Use this for 2d array, is in row, column order
    public Tile[,] Tiles2D;

    public bool ZeroWins { get; private set; }
    public bool OneWins { get; private set; }

    public bool Moved
    {
        get;
        private set;
    }

    public Piece MovedPiece { get; private set; }

    private Tile revertTo;
    private Tile revertFrom;



    void Awake()
    {
        //Add tile script to all tiles.  This is done in awake to ensure that by Start(), all tiles have Tile script
        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }

    //change to game object
    public void UpdateBoard(Game g)
    {
       // if (arr == null)
        //{
            CreateBoard();
       // }
    }

    void CreateBoard()
    {
        OneWins = false;
        ZeroWins = false;
        Player1Secret = SharedSceneData.SecretNumber;
        //Create 2d arrays of positions and game objects and instantiate pieces
        Tiles2D = new Tile[6, 5];
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //Convert 1d tile array from scene to 2d representation.
                //Assumes tiles are ordered in scene, probably not best implementation but w.e
                GameObject tile = tiles[(i * 5) + j];
                tile.gameObject.GetComponentInChildren<MeshRenderer>().gameObject.AddComponent<BoxCollider>();
                Tiles2D[i, j] = tile.gameObject.GetComponent<Tile>();

                //Instantiate virtual board location
                Tiles2D[i, j].I = i;
                Tiles2D[i, j].J = j;
                //Instantiate pieces on tiles.
                if (i == 0 || i == 1 || i == 5 || i == 4)
                {
                    //Instantiate piece on center of tile
                    Vector3 center = tile.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
                    Object n_Obj = GameObject.Instantiate(PiecePrefab, center, Quaternion.identity);
                    GameObject n_GameObj = (GameObject)n_Obj;
                    //needs completion to center piece on square b/c unity instantiates corner of piece on center of tile
                    n_GameObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    n_GameObj.transform.Rotate(new Vector3(90, 0, 0));
                    n_GameObj.transform.Translate(0, 0, -2);
                    n_GameObj.AddComponent<MeshCollider>();
                    //for collision
                    n_GameObj.tag = "piece";
                    //Set tile's piece
                    Tiles2D[i, j].Piece = n_GameObj.GetComponent<Piece>();
                    Tiles2D[i, j].Piece.IsSecret = false;
                    //This is now unneeded, when I set the tile's piece, I also set the piece's tile (see Piece property in tile)
                    //Tiles2D[i, j].Piece.Tile = Tiles2D[i, j];
                    Tiles2D[i, j].Piece.transform.Find("Piece").GetComponent<Renderer>().material.SetColor("_TintColor", Color.red);


                    //Set team for each piece
                    if (i == 0 || i == 1)
                    {
                        Tiles2D[i, j].Piece.Team = 0;
                    }
                    else
                    {
                        Tiles2D[i, j].Piece.Team = 1;
                    }
                    //Debug.Log("this: " + center.ToString() + ", tile: " + Tiles2D[i, j].Location);
                }
                else
                {
                    Tiles2D[i, j].Piece = null;
                }

            }
        }
        CurrentTeam = 0;
        Tiles2D[1 - Player1Secret / 5, Player1Secret % 5].Piece.IsSecret = true;
        Tiles2D[5, 0].Piece.IsSecret = true;
    }


	// Use this for initialization
	void Start ()
    {
        UpdateBoard(SharedSceneData.GameToLoad);
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreach(Tile t in Tiles2D)
        {
            if (t.Piece != null)
            {
                t.Piece.HighlightPiece(CurrentTeam);
            }
        }
	}

    public void ChangeTurn()
    {
        if (Moved)
        {
            if (CurrentTeam == 0)
            {
                CurrentTeam = 1;
            }
            else
            {
                CurrentTeam = 0;
            }
            Moved = false;
        }
        foreach (Tile t in Tiles2D)
        {
            Piece p = t.Piece;
            if (p != null && p.IsSecret && (p.Team == 0) && (t.I == 5))
            {
                ZeroWins = true;
            }
            else if (p != null && p.IsSecret && (p.Team == 1) && (t.I == 0))
            {
                OneWins = true;
            }
        }
        
    }
    //Register new move so that it can be reverted / turn ended
    public void RegisterNewMove(Tile prevTile, Tile nextTile)
    {
        Moved = true;
        MovedPiece = nextTile.Piece;
        revertTo = prevTile;
        revertFrom = nextTile;
    }
    //Revert move before turn ended
    public void RevertMove()
    {
        if (Moved)
        {
            //Set the previous tile's piece back
            revertTo.Piece = revertFrom.Piece;
            //Set the next tile's piece to none
            revertFrom.Piece = null;
            //Move the actual position back
            revertTo.Piece.MoveToTile(revertTo, 0.5f);
            //No moved piece
            MovedPiece = null;
        }
        //Now we havent moved
        Moved = false;
    }


    public void RestoreAllTiles()
    {
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Tiles2D[i, j].Dehighlight();
            }
        }

    }
    // Displays tiles that the current piece ca move to
    public void FindMovementOptions()
    {
        for (int r = 0; r < 6; r++)
        {
            for (int c = 0; c < 5; c++)
            {
                Tiles2D[r, c].movementsToHere.Clear();
            }
        }
        Piece p = currentPiece;
        //multiplier to keep pieces moving "forward"
        int side = 1;
        if (p.Team == 1) side = -1;
        Debug.Log("finding movement options");
        int[,] marked = new int[6, 5];
        Stack<Tile> toVisit = new Stack<Tile>();
        int i = p.Tile.I;
        int j = p.Tile.J;
        while (i >= 0 && i < 6 && (Tiles2D[i, j].Piece == null || (i == p.Tile.I && j == p.Tile.J)))
        {
            Tiles2D[i, j].movementsToHere.Add(Tiles2D[i, j]);
            Tiles2D[i, j].movementsToHere.Add(Tiles2D[i, j]);
            toVisit.Push(Tiles2D[i, j]);
            int k = j + 1;
            while (k < 5 && Tiles2D[i, k].Piece == null)
            {
                Tiles2D[i, k].movementsToHere.Add(Tiles2D[i, j]);
                Tiles2D[i, k].movementsToHere.Add(Tiles2D[i, k]);
                toVisit.Push(Tiles2D[i, k]);            
                k++;
            }
            k = j - 1;
            while (k >= 0 && Tiles2D[i, k].Piece == null)
            {
                Tiles2D[i, k].movementsToHere.Add(Tiles2D[i, j]);
                Tiles2D[i, k].movementsToHere.Add(Tiles2D[i, k]);
                toVisit.Push(Tiles2D[i, k]);               
                k--;
            }
            
            i += side;
        }
        Debug.Log("highlighting tiles");
        while (toVisit.Count > 0)
        {
            Tile t = toVisit.Pop();
            t.Highlight();
            j = t.J;
            i = t.I;
            if (side == -1 && i > 1 && marked[i - 2, j] == 0 && (Tiles2D[i - 1, j].Piece != null && !(i - 1 == p.Tile.I && j == p.Tile.J)) && Tiles2D[i - 2, j].Piece == null)
            {
                Tiles2D[i - 2, j].movementsToHere.AddRange(Tiles2D[i, j].movementsToHere);
                Tiles2D[i - 2, j].movementsToHere.Add(Tiles2D[i - 2, j]);
                toVisit.Push(Tiles2D[i - 2, j]);
            }
            if (side == 1 && i < 4 && marked[i + 2, j] == 0 && (Tiles2D[i + 1, j].Piece != null && !(i + 1 == p.Tile.I && j == p.Tile.J)) && Tiles2D[i + 2, j].Piece == null)
            {
                Tiles2D[i + 2, j].movementsToHere.AddRange(Tiles2D[i, j].movementsToHere);
                Tiles2D[i + 2, j].movementsToHere.Add(Tiles2D[i + 2, j]);
                toVisit.Push(Tiles2D[i + 2, j]);
            }
            if (j > 1 && marked[i, j - 2] == 0 && (Tiles2D[i, j - 1].Piece != null && !(i == p.Tile.I && j - 1 == p.Tile.J)) && Tiles2D[i, j - 2].Piece == null)
            {
                Tiles2D[i, j - 2].movementsToHere.AddRange(Tiles2D[i, j].movementsToHere);
                Tiles2D[i, j - 2].movementsToHere.Add(Tiles2D[i, j - 2]);
                toVisit.Push(Tiles2D[i, j - 2]);
            }
            if (j < 3 && marked[i, j + 2] == 0 && (Tiles2D[i, j + 1].Piece != null && !(i == p.Tile.I && j + 1 == p.Tile.J)) && Tiles2D[i, j + 2].Piece == null)
            {
                Tiles2D[i, j + 2].movementsToHere.AddRange(Tiles2D[i, j].movementsToHere);
                Tiles2D[i, j + 2].movementsToHere.Add(Tiles2D[i, j + 2]);
                toVisit.Push(Tiles2D[i, j + 2]);
            }
            marked[i, j] = 1;
        }

        p.Tile.Dehighlight();
    }
}


