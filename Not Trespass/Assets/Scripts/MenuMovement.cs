using UnityEngine;
using System.Collections;

public class MenuMovement : MonoBehaviour {

	public Transform startMarker;
	public Transform endMarker;
	public float speed = 1.0F;
	private float startTime;
	private float journeyLength;

	private tuple mainMenuPos;
	private tuple playMenuPos;
	private tuple friendsMenuPos;

	private tuple mainMenuRot;
	private tuple playMenuRot;
	private tuple friendsMenuRot;



	// Use this for initialization
	void Start () {
		mainMenuPos = new tuple (410F,58F,357F);
		mainMenuRot = new tuple (39.28185F, 0F, 0F);

		playMenuPos = new tuple (505, 138, 425);
		playMenuRot = new tuple (54.18F,-90.67F,17.627F);

		friendsMenuPos = new tuple (0,0,0);
		friendsMenuRot = new tuple (0,0,0);

		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);


	}

	// Update is called once per frame
	void Update () {
	
	}

	public void MoveToMainMenu(){
		Camera.main.transform.position = new Vector3(505, 138, 425);
		Camera.main.transform.rotation = Quaternion.Euler(54.18F,-90.67F,17.627F);

		/*
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		Camera.main.transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fracJourney);
		*/
	}

	struct tuple {
		private float x;
		private float y;
		private float z;
		public tuple (float a, float b, float c) {
			x = a;
			y = b;
			z = c;
		}

		public float getX() {
			return x;
		}

		public float getY() {
			return y;
		}

		public float getZ() {
			return z;
		}
	}

}
