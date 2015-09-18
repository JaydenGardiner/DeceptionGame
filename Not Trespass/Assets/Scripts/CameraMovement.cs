using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class CameraMovement : MonoBehaviour {


    public Text m_T;
    private float m_Speed = 2.5f;
    private Vector3 m_Target;
    private Vector3 m_OtherPt;
	// Use this for initialization
	void Start () {
        //Renderer render = board.GetComponent<Renderer>();
        //target = render.bounds.center;
        m_Target = Vector3.zero;
        m_OtherPt = new Vector3(1, 0, 0);
	}


    private float fingerStartTime  = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
  
    private bool isSwipe = false;
    private float minSwipeDist  = 50.0f;
    private float maxSwipeTime = 0.5f;

	// Update is called once per frame
    void Update()
    {

        this.transform.LookAt(m_Target);


        m_T.text = "updating lol";

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        m_T.text = "begin touch";
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        m_T.text = "cancelled touch";
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Moved:
                        m_T.text = "ending  touch beginning movement";
                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if (isSwipe)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                // the swipe is horizontal:
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                // the swipe is vertical:
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if (swipeType.x != 0.0f)
                            {
                                if (swipeType.x > 0.0f)
                                {
                                    // MOVE RIGHT
                                    
                                    //this.transform.Translate(Vector3.right * Time.deltaTime * speed);
                                    this.transform.RotateAround(m_Target, Vector3.up, Input.GetAxis("Mouse X") * m_Speed);
                                    m_OtherPt = RotateAroundPivot(m_OtherPt, Vector3.up, Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_Speed, 0));
                                }
                                else
                                {

                                    //this.transform.Translate(Vector3.right * Time.deltaTime * speed);
                                    this.transform.RotateAround(m_Target, Vector3.up, Input.GetAxis("Mouse X") * m_Speed);
                                    m_OtherPt = RotateAroundPivot(m_OtherPt, Vector3.up, Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_Speed, 0));
                                }
                            }

                            if (swipeType.y != 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Input.GetAxis("Mouse Y") * m_Speed);
                                }
                                else
                                {
                                    this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Input.GetAxis("Mouse Y") * m_Speed);
                                }
                            }

                        }

                        break;
                }
            }

        }
    }

    public Vector3 RotateAroundPivot(Vector3 Point, Vector3 Pivot, Quaternion Angle)
    {
        return Angle * (Point - Pivot) + Pivot;
    }
}
