using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BoardManger : MonoBehaviour {

    public InputField secretNumber;
    public static int number;
    public static bool IsSet;


    public GameObject[] numbers;
    public List<Color> originals;
    public static GameObject secretPiece;

	// Use this for initialization
	void Start () {
        secretNumber.characterValidation = InputField.CharacterValidation.Integer;
        IsSet = false;
        originals = new List<Color>();
        foreach(GameObject n in numbers)
        {
            originals.Add(n.GetComponent<SpriteRenderer>().color);
        }
	}
	
	// Update is called once per frame
	void Update () {
        


	    if (IsSet)
        {
            for(int i = 0; i <= 9; i++)
            {
                if (i == number)
                {
                    GameObject num = GameObject.Find(number.ToString());
                    num.GetComponent<SpriteRenderer>().color = Color.black;
                }
                else
                {
                    GameObject num = GameObject.Find(i.ToString());
                    num.GetComponent<SpriteRenderer>().color = originals[i];
                }
            }
            

        }
	}

    
}
