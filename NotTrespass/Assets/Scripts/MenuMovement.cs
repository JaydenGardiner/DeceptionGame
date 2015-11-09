using UnityEngine;
using System.Collections;

public class MenuMovement : MonoBehaviour {

	public Vector3 startPos;
	public Vector3 endPos;
	public Quaternion startRot;
	public Quaternion endRot;
	public float startTime;
	public float totalTime;

	private Vector3 mainMenuPos;
	private Vector3 playMenuPos;
	private Vector3 friendsMenuPos;

	private Quaternion mainMenuRot;
	private Quaternion playMenuRot;
	private Quaternion friendsMenuRot;

    private float speed = 2F;

    private int mode;
	private int state;
					/**		main - 1
					 * 		play - 2
					 *  	friends - 3
					 * 		options - 5
                     *      game - 4
					 */

	private GameObject mainCanvas, playCanvas, friendsCanvas, optionsCanvas, gamesCanvas;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Landscape;
    }

	// Use this for initialization
	void Start () {

        state = 1;      // start on main menu screen

        // initialize global canvas objects
		mainCanvas = GameObject.Find ("MainScreen");
		playCanvas = GameObject.Find ("PlayScreen");
		friendsCanvas = GameObject.Find ("FriendsScreen");
		optionsCanvas = GameObject.Find ("OptionsScreen");
        gamesCanvas = GameObject.Find("GamesScreen");

        // set main menu active and rest inactive
        mainCanvas.SetActive (true);
		playCanvas.SetActive (false);
		friendsCanvas.SetActive (false);
		optionsCanvas.SetActive (false);
        gamesCanvas.SetActive(false);

		mainMenuPos = new Vector3 (410F,58F,357F);
		mainMenuRot = new Quaternion (39.28185F, 0F, 0F, 0F);

		playMenuPos = new Vector3 (505, 138, 425);
		playMenuRot = new Quaternion (54.18F,-90.67F,17.627F, 0F);

		friendsMenuPos = new Vector3 (0,0,0);
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

    public void LoadSecretScreen()
    {
        Application.LoadLevel("SecretPiece");
    }

	public void MoveToMainMenu(){
        moveMenu(1);

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
        moveMenu(2);

        //mode = 2;

    }

	public void MoveToFriendsScreen() {
        moveMenu(3);
    }

	public void MoveToOptionsScreen() {
        moveMenu(5);
    }

    public void MoveToGamesScreen()
    {
        moveMenu(4);
    }


    /** moveMenu method
    * @param: newState is the state we are moving to
    * It uses the global state variable to initiate the fade out
    * of the old canvas and fade in of the new canvas.
    * Then it changes the global state.
    */
    private void moveMenu(int newState)
    {
        // set the current canvas and next canvas
        // we fade out the current and fade in the next
        GameObject currentCanvas = null;
        GameObject nextCanvas = null;
        switch (state)
        {
            case 1:
                currentCanvas = mainCanvas;
                break;
            case 2:
                currentCanvas = playCanvas;
                break;
            case 3:
                currentCanvas = friendsCanvas;
                break;
            case 4:
                currentCanvas = optionsCanvas;
                break;
            case 5:
                currentCanvas = gamesCanvas;
                break;
        }
        switch (newState)
        {
            case 1:
                nextCanvas = mainCanvas;
                break;
            case 2:
                nextCanvas = playCanvas;
                break;
            case 3:
                nextCanvas = friendsCanvas;
                break;
            case 4:
                nextCanvas = optionsCanvas;
                break;
            case 5:
                nextCanvas = gamesCanvas;
                break;
        }
        StartCoroutine(fadeOut(currentCanvas, speed));  // fade out 
        StartCoroutine(fadeIn(nextCanvas, speed));  // fade in
        state = newState;   // set new state
    }

    /** fadeIn method
    * @ param   obj     -   gameobject containing canvas to be faded in
    * @ param speed     -   float dictating the speed of the fade
    * fades in the canvas
    * method credit goes to: Bruno Araújo
    */
    IEnumerator fadeIn(GameObject obj, float speed) {
		float increment;
		CanvasGroup cv = obj.GetComponent<CanvasGroup>();
        cv.alpha = 0;
        obj.SetActive(true);
        while (cv.alpha < 1) {
			increment = speed * Time.deltaTime;
			if (cv.alpha + increment > 1) cv.alpha = 1;
			else cv.alpha += speed * Time.deltaTime;
			yield return null;
		}
    }

    /** fadeOut method
    * @ param   obj     -   gameobject containing canvas to be faded out
    * @ param speed     -   float dictating the speed of the fade
    * fades out the canvas
    * method credit goes to: Bruno Araújo
    */
    IEnumerator fadeOut(GameObject obj, float speed) {
		float increment;
		CanvasGroup cv = obj.GetComponent<CanvasGroup>();
        cv.alpha = 1;
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
