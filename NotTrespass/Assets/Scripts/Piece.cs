using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Piece : MonoBehaviour {

    public int PieceNumber;

    public Tile Tile;

    public bool IsSecret;

    public int Team;

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

    public void HighlightPiece(int curTeam)
    {
        //"_TintColor"
        GameObject outlineObj = this.gameObject;
        foreach(Transform child in transform)
        {
            if (child.CompareTag("pieceOutline"))
            {
                outlineObj = child.gameObject;
            }
        }
        if (Team == 0)
        {
            if (IsSecret)
            {
                if (curTeam == 0)
                {
                    outlineObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_OutlineColor", Color.yellow);
                }
                else
                {
                    outlineObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_OutlineColor", Color.magenta);
                }
                
            }
            else
            {
                outlineObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_OutlineColor", Color.magenta);
            }
            
        }
        else
        {
            if (IsSecret)
            {
                //dont highlight other players piece
                //outlineObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_OutlineColor", Color.green);
            }
            else
            {
                outlineObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_OutlineColor", Color.red);
            }
            
        }

        
    }

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

    public void MoveToTile(Tile tile, float duration)
    {
        Debug.Log("moving");
        Vector3 m_Destination = tile.Location;
        StartCoroutine(WaitAndMove(0, duration, m_Destination));
        this.Tile = tile;
    }

    public void MoveOnPathToTile(Tile tile, float duration)
    {
        Debug.Log("moving");
        List<Tile> movementList = tile.movementsToHere;
        for (int i = 0; i < movementList.Count; i++)
        {
            Debug.Log(movementList[i].I);
            Debug.Log(movementList[i].J);
        }
        //StartCoroutine(WaitAndMove(0, duration, movementList[movementList.Count - 1].Location));

        StartCoroutine(WaitAndMoveOnPath(0, duration, movementList));
        

        this.Tile = movementList[movementList.Count - 1];
    }
}
