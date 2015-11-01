using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private Piece m_Piece;
    public Piece Piece
    {
        get { return m_Piece; }
        set
        {
            if (value != null)
            {
                m_Piece = value;
                m_Piece.Tile = this;
            }
            else
            {
                m_Piece = null;
            }
            
        }
    }

    public int I;

    public int J;

    public bool isHighlighted;

    public List<Tile> movementsToHere;

    //to access actual tile object, use this.gameObject

	// Use this for initialization
    void Awake()
    {
        //This must be in awake, otherwise it will be an incorrect location
        m_Loc = this.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
        //set to correct height to move pieces to
        m_Loc.y += 2f;
        isHighlighted = false;
        movementsToHere = new List<Tile>();
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
        if ((I + J) % 2 == 0) gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.cyan);
        else gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.blue);
    }

    public void Dehighlight()
    {
        isHighlighted = false;
        if ((I + J) % 2 == 0) gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.white);
        else gameObject.GetComponentInChildren<Renderer>().material.SetColor("_TintColor", Color.gray);
    }
}
