using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public Vector3 Location { get; private set; }

    public PieceMovement Piece;

    public int I;

    public int J;

    //to access actual tile object, use this.gameObject

	// Use this for initialization
    void Awake()
    {
        //This must be in awake, otherwise it will be an incorrect location
        Location = this.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
