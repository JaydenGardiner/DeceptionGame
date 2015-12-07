using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Controls how the game scene's camera movement behaves
/// </summary>
public class CameraMovement : MonoBehaviour {

    public static bool LockScreen;

    private float m_Speed = 2.5f;
    private Vector3 m_Target;
    private Vector3 m_OtherPt;

    private Vector3 prevPos;
    private Quaternion prevRot;
    public float m_MaxCameraY;
    public float m_MinCameraY;

    private Camera m_MainCamera;

	// Use this for initialization
	void Start () {
        //Renderer render = board.GetComponent<Renderer>();
        //target = render.bounds.center;
        m_Target = Vector3.zero;
        m_OtherPt = new Vector3(1, 0, 0);
        m_MaxCameraY = 111f;
        m_MinCameraY = 15f;
        m_MainCamera = this.GetComponent<Camera>();
        //this.transform.position = new Vector3(this.transform.position.x, m_MaxCameraY, this.transform.position.z);
	}


    private float fingerStartTime  = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;
  
    private bool isSwipe = false;
    //private float minSwipeDist  = 50.0f;
    //private float maxSwipeTime = 0.5f;

    private float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    private float orthoZoomSpeed = 0.5f;

	// Update is called once per frame
    void Update()
    {
        
        //Debug.Log("rotation: " + transform.rotation);
        this.transform.LookAt(m_Target);
        prevPos = this.transform.position;
        prevRot = this.transform.rotation;

        if (Input.touchCount > 0 && Input.touchCount < 2 && LockScreen)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        /* this is a new touch */
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;

                    case TouchPhase.Canceled:
                        /* The touch is being canceled */
                        isSwipe = false;
                        break;

                    case TouchPhase.Moved:
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
                                    if (this.transform.position.y < m_MinCameraY || this.transform.position.y >= m_MaxCameraY)
                                    {
                                        this.transform.position = prevPos;
                                        this.transform.rotation = prevRot;
                                    }
                                }
                                else
                                {
                                    this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Input.GetAxis("Mouse Y") * m_Speed);                                    
                                    if (this.transform.position.y < m_MinCameraY || this.transform.position.y >= m_MaxCameraY)
                                    {
                                        this.transform.position = prevPos;
                                        this.transform.rotation = prevRot;
                                    }
                                }
                            }

                        }

                        break;
                }
            }

        }
        else if (Input.touchCount >= 2 && LockScreen)
        {
            //zooming
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            
            if (m_MainCamera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                m_MainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                m_MainCamera.orthographicSize = Mathf.Max(m_MainCamera.orthographicSize, 0.1f);
                m_MainCamera.orthographicSize = Mathf.Min(m_MainCamera.orthographicSize, 50f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                m_MainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                m_MainCamera.fieldOfView = Mathf.Clamp(m_MainCamera.fieldOfView, 10f, 120f);
            }
        }


//controls functionality in editor
#if UNITY_EDITORx
        this.transform.RotateAround(m_Target, Vector3.up, Input.GetAxis("Mouse X") * m_Speed);
        m_OtherPt = RotateAroundPivot(m_OtherPt, Vector3.up, Quaternion.Euler(0, Input.GetAxis("Mouse X") * m_Speed, 0));
        this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Input.GetAxis("Mouse Y") * m_Speed);  
        if (this.transform.position.y < m_MinCameraY || this.transform.position.y >= m_MaxCameraY)
        {
            print("camera: " + this.transform.rotation);
            this.transform.position = prevPos;
            this.transform.rotation = prevRot;
        }
        /*
        if (this.transform.position.y >= 15 && this.transform.position.y <= 90)
        {
            this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Input.GetAxis("Mouse Y") * m_Speed);
        }
        else if (this.transform.position.y < 15)
        {
            Debug.Log("limit lower");
            this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), -1*Mathf.Abs(Input.GetAxis("Mouse Y") * m_Speed));
        }
        else if (this.transform.position.y >= 90)
        {
            Debug.Log("limit upper");
            this.transform.RotateAround(m_Target, (m_OtherPt - m_Target), Mathf.Abs(Input.GetAxis("Mouse Y") * m_Speed));
        }
        else
        {
            Debug.Log("other");
        }*/
        


#endif

    }

    public Vector3 RotateAroundPivot(Vector3 Point, Vector3 Pivot, Quaternion Angle)
    {
        return Angle * (Point - Pivot) + Pivot;
    }
}
