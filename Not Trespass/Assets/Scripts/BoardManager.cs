using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

    public GameObject board;

	// Use this for initialization
	void Start () {
        board = this.gameObject;
        GameObject n_Obj = board.GetComponent<GameObject>();
        Debug.Log(n_Obj.name);

	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(this.transform.position);
	}
}
