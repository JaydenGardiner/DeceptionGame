using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BoardManager : MonoBehaviour {

    public GameObject board;

    public GameObject PiecePrefab;

    public GameObject[] tiles;

 
    /* In row column order where the rows go downwards
     * 5 6 7 8 9  r=0
     * 0 1 2 3 4  r=1
     * - - - - -  r=2
     * - - - - -  r=3
     * 0 1 2 3 4  r=4
     * 5 6 7 8 9  r=5
     */
    public Vector3[,] positions2d;
    public GameObject[,] tiles2d;

	// Use this for initialization
	void Start () {
        //Create 2d arrays of positions and game objects and instantiate pieces
        positions2d = new Vector3[6, 5];
        tiles2d = new GameObject[6, 5];
        for(int i = 0; i < 6; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                GameObject tile = tiles[(i * 5) + j];
                Vector3 center = tile.gameObject.GetComponentInChildren<MeshRenderer>().bounds.center;
                positions2d[i, j] = center;
                tiles2d[i, j] = tile.gameObject;
                if (i == 0 || i == 1 || i == 5 || i == 4)
                {
                    Object n_Obj = GameObject.Instantiate(PiecePrefab, center, Quaternion.identity);
                    GameObject n_GameObj = (GameObject)n_Obj;
                    n_GameObj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    n_GameObj.transform.Rotate(new Vector3(90, 0, 0));
                    n_GameObj.transform.Translate(0, 0, -2);
                    n_GameObj.AddComponent<MeshCollider>();
                }
                
            }
        }

	}
	
	// Update is called once per frame
	void Update () {

	}
}
