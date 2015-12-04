using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorHandler : MonoBehaviour {

    public static string ErrorMessage;
    public static string SceneToLoad;

    public Text ErrorText;

	// Use this for initialization
	void Start () {
        ErrorText.text = ErrorMessage;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void LoadMenu()
    {
        Application.LoadLevel(SceneToLoad);
    }
}
