using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
    public BoardManager board;

    public Button TurnButton;
    public Button RevertButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (!board.Moved)
        {
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else
        {
            TurnButton.interactable = true;
            RevertButton.interactable = true;
        }
	}

    public void TurnButtonMethod()
    {
        board.ChangeTurn();
    }

    public void RevertButtonMethod()
    {
        board.RevertMove();
    }
}
