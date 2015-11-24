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
	    if (!board.Moved || !board.IsMyTurn)
        {
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else
        {
            TurnButton.interactable = true;
            RevertButton.interactable = true;
        }
        if (board.ZeroWins)
        {
            StatusText.text = "PLAYER ONE WINS";
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else if (board.OneWins)
        {
            StatusText.text = "PLAYER TWO WINS";
            TurnButton.interactable = false;
            RevertButton.interactable = false;
        }
        else
        {
            if (board.CurrentTeam == 0)
            {
                StatusText.text = "PLAYER ONE'S TURN";
            }
            else
            {
                StatusText.text = "PLAYER TWO'S TURN";
            }
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
