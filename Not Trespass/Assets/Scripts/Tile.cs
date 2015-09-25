using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    private Vector3 m_Loc;
    public Vector3 Location
    {
        get
        {
            return m_Loc;
        }
        private set
        {
            m_Loc = value;
        }
    }

    public Piece Piece;

    public int I;

    public int J;

    public bool isHighlighted;

    //to access actual tile object, use this.gameObject

	// Use this for initialization
    void Awake()
    {
        //This must be in awake, otherwise it will be an incorrect location
        m_Loc = this.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
        //set to correct height to move pieces to
        m_Loc.y += 2f;
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
        //Debug.Log("Tile " + I + " " + J + " " + (gameObject.GetComponentInChildren<Renderer>() == null));
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.blue);
    }

    public void Dehighlight()
    {
        isHighlighted = false;
        gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.white);
    }
}
