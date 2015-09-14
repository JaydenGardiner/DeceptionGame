using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

    public GameObject board;

	// Use this for initialization
	void Start () {
        board = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(this.transform.position);
	}
}
