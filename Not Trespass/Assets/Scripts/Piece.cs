using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {

    public int PieceNumber;

    public bool IsSecret;

    public int Team;

    void Awake()
    {
        IsSecret = false;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSelect()
    {
        Debug.Log("piece select");
        //change color, set to move, etc.....
    }

    public override bool Equals(object o)
    {
        if (o == null) { return false; }
        Piece p = o as Piece;
        if ((System.Object)p == null) { return false; }
        return (this.PieceNumber == p.PieceNumber);
    }

}
