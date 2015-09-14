using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float speed = 5f;
    private Vector3 target;
	// Use this for initialization
	void Start () {
        //Renderer render = board.GetComponent<Renderer>();
        //target = render.bounds.center;
        target = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.LookAt(target);
        //this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        this.transform.RotateAround(target, Vector3.up, Input.GetAxis("Mouse X") * speed);
	}
}
