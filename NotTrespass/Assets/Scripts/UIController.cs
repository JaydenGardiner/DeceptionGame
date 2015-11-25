using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
    public BoardManager board;

    public Text StatusText;
    public Text LockScreenText;
    public Button TurnButton;
    public Button RevertButton;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (!board.Moved || SharedSceneData.GameToLoad.CurrentMove != SharedSceneData.my_user)
        {
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else
        {
            TurnButton.interactable = true;
            RevertButton.interactable = true;
        }
        
        if (SharedSceneData.GameToLoad.GameStatus == Game.Status.COMPLETED)
        {
            StatusText.text = SharedSceneData.GameToLoad.Winner + "WINS!!";
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
        if(CameraMovement.LockScreen)
        {
            LockScreenText.text = "Lock Camera";
        }
        else
        {
            LockScreenText.text = "Unlock Camera";
        }
    }

    public void MarkSelectedPieceMethod()
    {
        board.MarkSelectedPiece();
    }
}
