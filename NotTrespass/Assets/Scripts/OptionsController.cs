using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

    public InputField Enter;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeIP()
    {
        string newIP = Enter.text;
        GameApi.API_BASE = "http://" + newIP + ":5000";
        Debug.Log(GameApi.API_BASE);
    }
}
