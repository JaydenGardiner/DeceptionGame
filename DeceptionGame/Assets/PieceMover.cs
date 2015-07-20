using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]
public class PieceMover : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
    void Update()
    {

    }

// Attach this script to an orthographic camera.
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Debug.Log("RUNNING MOUSE DRAG");
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
}
