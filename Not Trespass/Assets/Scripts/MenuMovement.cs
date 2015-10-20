using UnityEngine;
using System.Collections;

public class MenuMovement : MonoBehaviour {

	public Vector3 startPos;
	public Vector3 endPos;
	public Quaternion startRot;
	public Quaternion endRot;
	public float startTime;
	public float totalTime;

	private tuple mainMenuPos;
	private tuple playMenuPos;
	private tuple friendsMenuPos;

	private Quaternion mainMenuRot;
	private Quaternion playMenuRot;
	private Quaternion friendsMenuRot;

	private int mode;
	private int currentPage;
					/**		main - 0
					 * 		play - 1
					 *  	friends - 2
					 * 		options - 3
					 */

	private GameObject mainCanvas, playCanvas, friendsCanvas, optionsCanvas;


	// Use this for initialization
	void Start () {

		mainCanvas = GameObject.Find ("MainScreen");
		playCanvas = GameObject.Find ("PlayScreen");
		friendsCanvas = GameObject.Find ("FriendsScreen");
		optionsCanvas = GameObject.Find ("OptionsScreen");

		mainCanvas.SetActive (true);
		playCanvas.SetActive (false);
		friendsCanvas.SetActive (false);
		optionsCanvas.SetActive (false);

		mainMenuPos = new tuple (410F,58F,357F);
		mainMenuRot = new Quaternion (39.28185F, 0F, 0F, 0F);

		playMenuPos = new tuple (505, 138, 425);
		playMenuRot = new Quaternion (54.18F,-90.67F,17.627F, 0F);

		friendsMenuPos = new tuple (0,0,0);
		friendsMenuRot = new Quaternion (0,0,0,0);

		startTime = Time.time;
		totalTime = 2F;
		mode = 0;


	}

	// Update is called once per frame
	void Update () {
		if (mode != 0) {
			float something = (Time.time - startTime) / totalTime;

			Vector3 pos = Vector3.Lerp (startPos, endPos, something);

			Camera.main.transform.position = pos;
			float rotateX = Random.Range (0, 50);
			Camera.main.transform.Rotate(Vector3.down, 10.0f * Time.deltaTime);

			if (pos == endPos) {
				mode = 0;
			}
		}
	}

	public void MoveToMainMenu(){

		if (currentPage == 1) {
			playCanvas.SetActive (false);
		} else if (currentPage == 2) {
			friendsCanvas.SetActive (false);
		} else if (currentPage == 3) {
			optionsCanvas.SetActive(false);
		}
		currentPage = 0;

		mainCanvas.SetActive (true);


		/*
		startPos = mainMenuPos.toVector ();
		endPos = playMenuPos.toVector ();

		startRot = mainMenuRot;
		endRot = mainMenuRot;

		startTime = Time.time;
*/
//		mode = 1;




//		Camera.main.transform.position = new Vector3(playMenuPos.getX(), playMenuPos.getY(), playMenuPos.getZ());
//		Camera.main.transform.rotation = Quaternion.Euler(54.18F,-90.67F,17.627F);

	}

	public void MoveToPlayScreen() {
		//mode = 2;

		currentPage = 1;
		mainCanvas.SetActive (false);
		playCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        friendsCanvas.SetActive(false);
		startTime = Time.time;
	}

	public void MoveToFriendsScreen() {
		//mode = 3;

		currentPage = 2;
		mainCanvas.SetActive (false);
        playCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
		friendsCanvas.SetActive(true);

		startTime = Time.time;
//		StartCoroutine(fadeOut(mainCanvas, 2.0f));
		//StartCoroutine(fadeIn(playCanvas, 2.0f));
	}

	public void MoveToOptionsScreen() {
		//mode = 4;
		currentPage = 3;
        playCanvas.SetActive(false);
        friendsCanvas.SetActive(false);
		mainCanvas.SetActive (false);
		optionsCanvas.SetActive(true);

		startTime = Time.time;

	}

	IEnumerator fadeIn(GameObject obj, float speed) {
		float increment;
		obj.SetActive(true);
		CanvasGroup cv = obj.GetComponent<CanvasGroup>();
		while (cv.alpha < 1) {
			increment = speed * Time.deltaTime;
			if (cv.alpha + increment > 1) cv.alpha = 1;
			else cv.alpha += speed * Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator fadeOut(GameObject obj, float speed) {
		float increment;
		CanvasGroup cv = obj.GetComponent<CanvasGroup>();
		while (cv.alpha > 0) {
			increment = speed * Time.deltaTime;
			if (cv.alpha - increment < 0) cv.alpha = 0;
			else cv.alpha -= speed * Time.deltaTime;
			yield return null;
		}
		obj.SetActive(false);
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

		public Vector3 toVector () {
			return new Vector3 (x, y, z);
		}
	}

}
