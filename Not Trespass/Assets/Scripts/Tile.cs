using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public Vector3 Location { get; private set; }

    public Piece Piece;

    public int I;

    public int J;

    public bool isHighlighted;

    //to access actual tile object, use this.gameObject

	// Use this for initialization
    void Awake()
    {
        //This must be in awake, otherwise it will be an incorrect location
        Location = this.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
        isHighlighted = false;
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Highlight()
    {
        isHighlighted = true;
        Debug.Log("Tile " + I + " " + J + " " + (gameObject.GetComponentInChildren<Renderer>() == null));
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.blue);
    }

    public void Dehighlight()
    {
        isHighlighted = false;
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.white);
    }
}
