using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Holds piece game object
/// </summary>
public class Piece : MonoBehaviour {

    public int PieceNumber;

    public Tile Tile;

    public bool IsSecret;

    public int Team;

    public bool IsSelected;

    public bool IsMarked;

    //private Vector3 m_Destination;

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

    /// <summary>
    /// Highlights piece based on team and secret number
    /// </summary>
    public void HighlightPiece()
    {
        //"_TintColor"
        /*
        GameObject outlineObj = this.gameObject;
        foreach(Transform child in transform)
        {
            if (child.CompareTag("pieceOutline"))
            {
                outlineObj = child.gameObject;
            }
        }*/
        if (IsSelected)
        {
            this.GetComponent<Renderer>().material.SetColor("_ColorTint", Color.blue);
        }
        else if (IsMarked)
        {
            this.GetComponent<Renderer>().material.SetColor("_ColorTint", Color.green);
        }
        else 
        {
            if (IsSecret && Team == SharedSceneData.my_team)
            {
             
                this.GetComponent<Renderer>().material.SetColor("_ColorTint", Color.yellow);   
            }
            else if(Team == 0)
            {
                this.GetComponent<Renderer>().material.SetColor("_ColorTint", Color.magenta);
            }
            else
            {
                this.GetComponent<Renderer>().material.SetColor("_ColorTint", Color.red);
            }
            
        }
        
        
    }

    /// <summary>
    /// Moves a piece to a location
    /// </summary>
    /// <param name="delay">
    /// How long before the move starts
    /// </param>
    /// <param name="duration">
    /// Time for the move to take
    /// </param>
    /// <param name="dest">
    /// Final target location for the move
    /// </param>
    /// <returns></returns>
    private IEnumerator WaitAndMove(float delay, float duration, Vector3 dest )
    {
        yield return new WaitForSeconds(delay);
        float startTime = Time.time;
        while(Time.time - startTime <= duration)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, dest, Time.time - startTime);
            //wait for next frame
            yield return 1;
        }
    }

    /// <summary>
    /// Moves piece on a path
    /// </summary>
    /// <param name="delay">
    /// How long before move starts
    /// </param>
    /// <param name="duration">
    /// Time for a move
    /// </param>
    /// <param name="movements">
    /// List of tiles for the movement to follow
    /// </param>
    /// <returns></returns>
    private IEnumerator WaitAndMoveOnPath(float delay, float duration, List<Tile> movements)
    {
        yield return new WaitForSeconds(delay);
        Vector3 cur = this.transform.position;
        for (int i = 0; i < movements.Count; i++)
        {
            if (cur.Equals(movements[i].Location)) continue;
            float startTime = Time.time;
            while (Time.time - startTime <= duration)
            {
                float t = 2 * (Time.time - startTime);   
                Vector3 temp = Vector3.Lerp(cur, movements[i].Location, t);
                if (i >= 2)
                {
                    float h = (-t) * (t - 1) * 100;
                    temp.y = movements[i].Location.y + h;
                }
                this.transform.position = temp;
                
                //wait for next frame
                yield return 1;
            }
            this.transform.position = movements[i].Location;
            cur = movements[i].Location;
        }
    }

    /// <summary>
    /// Moves to a tile
    /// </summary>
    /// <param name="tile">
    /// Tile to move to
    /// </param>
    /// <param name="duration">
    /// Length of time to take for move
    /// </param>
    public void MoveToTile(Tile tile, float duration)
    {
        Debug.Log("moving");
        Vector3 m_Destination = tile.Location;
        StartCoroutine(WaitAndMove(0, duration, m_Destination));
        this.Tile = tile;
    }

    /// <summary>
    /// Moves piece on a path to a tile
    /// </summary>
    /// <param name="tile">
    /// Tile to move to
    /// </param>
    /// <param name="duration">
    /// Time to take for the movement
    /// </param>
    public void MoveOnPathToTile(Tile tile, float duration)
    {
        Debug.Log("moving");
        List<Tile> movementList = tile.movementsToHere;
        for (int i = 0; i < movementList.Count; i++)
        {
            //Debug.Log(movementList[i].I);
            //Debug.Log(movementList[i].J);
        }
        //StartCoroutine(WaitAndMove(0, duration, movementList[movementList.Count - 1].Location));

        StartCoroutine(WaitAndMoveOnPath(0, duration, movementList));
        

        this.Tile = movementList[movementList.Count - 1];
    }
}
