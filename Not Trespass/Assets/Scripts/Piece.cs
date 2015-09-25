using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    public int PieceNumber;

    public Tile Tile;

    public bool IsSecret;

    public int Team;

    private Vector3 m_Destination;
    private bool m_IsMoving;
    private float m_TimeToMove;


    void Awake()
    {
        IsSecret = false;
    }
	// Use this for initialization
	void Start () {
        m_TimeToMove = 1.0f;
        if (IsSecret)
        {
            HighlightPiece();
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void HighlightPiece()
    {
        if (Team == 0)
        {
            this.gameObject.GetComponentsInChildren<Renderer>()[1].material.SetColor("_TintColor", Color.yellow);
        }
        else
        {
            this.gameObject.GetComponentsInChildren<Renderer>()[1].material.SetColor("_TintColor", Color.green);
        }
    }

    public void OnSelect()
    {
        Debug.Log("piece select");
        //deprecated
        //change color, set to move, etc.....
    }

    public override bool Equals(object o)
    {
        if (o == null) { return false; }
        Piece p = o as Piece;
        if ((System.Object)p == null) { return false; }
        return (this.PieceNumber == p.PieceNumber);
    }

    private IEnumerator WaitAndMove(float delay)
    {
        yield return new WaitForSeconds(delay);
        float startTime = Time.time;
        while(Time.time - startTime <= 1)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, m_Destination, Time.time - startTime);
            //wait for next frame
            yield return 1;
        }
    }

    public void MoveToTile(Tile tile)
    {
        Debug.Log("moving");
        m_Destination = tile.Location;
        StartCoroutine(WaitAndMove(0));
        Tile = tile;
    }

}
