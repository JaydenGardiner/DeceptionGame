using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {
    public BoardManager board;

    public Text StatusText;
    public Text LockText;
    public Button TurnButton;
    public Button RevertButton;
    //public Dropdown MenuDrop;
    //public Dropdown MoveDrop;



	// Use this for initialization
	void Start () {



        //MenuDrop.options = new List<Dropdown.OptionData>();
        //string lockText = "";
        if (CameraMovement.LockScreen)
        {
            LockText.text = "Lock Camera";
        }
        else
        {
            LockText.text = "Unlock Camera";
        }
        //MenuDrop.options.Add(new Dropdown.OptionData("Options..."));
        //MenuDrop.options.Add(new Dropdown.OptionData(lockText));
        //MenuDrop.options.Add(new Dropdown.OptionData("Main Menu"));
        //MenuDrop.value = 0;
        //MoveDrop.options = new List<Dropdown.OptionData>();
        //MoveDrop.options.Add(new Dropdown.OptionData("Move..."));
        //MoveDrop.options.Add(new Dropdown.OptionData("Mark Piece"));
        //MoveDrop.options.Add(new Dropdown.OptionData("Revert Move"));
        //MoveDrop.options.Add(new Dropdown.OptionData("End Turn"));
        
        //MoveDrop.value = 0;
	}
    /*
    public void SelectMenuOption()
    {
        Debug.Log("selecting menu item: " + MenuDrop.value);
        if (MenuDrop.value == 0)
        {
            LockScreenMethod();
        }
        else if (MenuDrop.value == 1)
        {
            BackButtonMethod();
        }
    }

    public void SelectMoveOption()
    {
        if (MoveDrop.value == 0)
        {
            MarkSelectedPieceMethod();
        }
        else if (MoveDrop.value == 1)
        {
            RevertButtonMethod();
        }
        else if (MoveDrop.value == 2)
        {
            TurnButtonMethod();
        }
    }*/

    bool enableTurn;
    bool enableRevert;

	// Update is called once per frame
	void Update () {
	    if (!board.Moved || SharedSceneData.GameToLoad.CurrentMove != SharedSceneData.my_user)
        {
            TurnButton.interactable = false;
            //enableTurn = false;
            //enableRevert = false;
            RevertButton.interactable = false;
        }
        else
        {
            //enableTurn = true;
            //enableRevert = true;
            TurnButton.interactable = true;
            RevertButton.interactable = true;
        }
        
        if (SharedSceneData.GameToLoad.GameStatus == Game.Status.COMPLETED)
        {
            StatusText.text = SharedSceneData.GameToLoad.Winner + "WINS!!";
            //enableTurn = false;
            //enableRevert = false;
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else
        {
            StatusText.text = SharedSceneData.GameToLoad.CurrentMove + "'s move";
        }
	}

    public void BackButtonMethod()
    {
        Application.LoadLevel("MenuScreen");
    }

    public void TurnButtonMethod()
    {
        board.ChangeTurn();
    }

    public void RevertButtonMethod()
    {
        board.RevertMove();
    }

    public void LockScreenMethod()
    {
        CameraMovement.LockScreen = !CameraMovement.LockScreen;
        Debug.Log("LOCK screen");
        if(CameraMovement.LockScreen)
        {
            LockText.text = "Lock Camera";
            //MenuDrop.options[0].
            //MenuDrop.options[0] = new Dropdown.OptionData("Lock Camera");
        }
        else
        {
            LockText.text = "Unlock Camera";
            //MenuDrop.options[0] = new Dropdown.OptionData("Unlock Camera");
        }
    }

    public void MarkSelectedPieceMethod()
    {
        board.MarkSelectedPiece();
    }
}
