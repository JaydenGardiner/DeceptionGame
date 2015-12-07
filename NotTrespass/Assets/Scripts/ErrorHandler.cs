using UnityEngine;
using UnityEngine.UI;
using System.Collections;


/// <summary>
/// Controls the error scene
/// </summary>
public class ErrorHandler : MonoBehaviour {

    /// <summary>
    /// Message to display
    /// </summary>
    public static string ErrorMessage;

    /// <summary>
    /// Scene to load wiwth button
    /// </summary>
    public static string SceneToLoad;

    /// <summary>
    /// Gameobject text
    /// </summary>
    public Text ErrorText;

	// Use this for initialization
	void Start () {
        ErrorText.text = ErrorMessage;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    /// <summary>
    /// Loads scene on button press
    /// </summary>
    public void LoadMenu()
    {
        Application.LoadLevel(SceneToLoad);
    }
}
