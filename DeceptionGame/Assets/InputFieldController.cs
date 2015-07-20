using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldController : MonoBehaviour {


	// Use this for initialization
	void Start () {
        InputField input = GetComponent<InputField>();
        var se = new InputField.SubmitEvent();
        se.AddListener(SubmitInteger);
        input.onEndEdit = se;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SubmitInteger(string arg0)
    {
        Debug.Log("test");
        int number;
        if (int.TryParse(arg0, out number))
        {
            if (number <= 9 && number >= 0)
            {
                BoardManger.number = number;
                BoardManger.IsSet = true;
            }
        }
    }
}
