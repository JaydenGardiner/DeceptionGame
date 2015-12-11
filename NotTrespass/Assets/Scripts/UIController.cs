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

    public Canvas MainCanvas;
    public Canvas MessageCanvas;
    public Text MessageText;

    public static bool IsGameEnabled;

	// Use this for initialization
    /// <summary>
    /// Inherited from MonoBehaviour- initializes variables
    /// </summary>
	void Start () {
        if (CameraMovement.LockScreen)
        {
            LockText.text = "Lock Camera";
        }
        else
        {
            LockText.text = "Unlock Camera";
        }
        MainCanvas.enabled = true;
        MessageCanvas.enabled = false;
        IsGameEnabled = true;
	}


	// Update is called once per frame
    /// <summary>
    /// Update inherited from MonoBehaviour-sets button interactivity
    /// </summary>
	void Update () {
        if (board.IsNetworkConnection)
        {
            IsGameEnabled = true;
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
                MainCanvas.enabled = false;
                MessageCanvas.enabled = true;
                MessageText.text = SharedSceneData.GameToLoad.Winner + " WINS!!";
                IsGameEnabled = false;
                //enableTurn = false;
                //enableRevert = false;
                //TurnButton.interactable = false;
                //RevertButton.interactable = false;
            }
            else if (SharedSceneData.GameToLoad.GameStatus == Game.Status.PENDING)
            {
                MainCanvas.enabled = false;
                MessageCanvas.enabled = true;
                MessageText.text = "Waiting for other player to accept";
                IsGameEnabled = false;
            }
            else
            {
                MainCanvas.enabled = true;
                MessageCanvas.enabled = false;
                IsGameEnabled = true;
                StatusText.text = SharedSceneData.GameToLoad.CurrentMove + "'s move";
            }
        }
        else
        {
            MainCanvas.enabled = false;
            MessageCanvas.enabled = true;
            MessageText.text = "Waiting for network connection...";
            IsGameEnabled = false;
        }
	}

    /// <summary>
    /// Interfaces with button to go back to main menu
    /// </summary>
    public void BackButtonMethod()
    {
        Application.LoadLevel("MenuScreen");
    }

    /// <summary>
    /// Interfaces with button to end turn
    /// </summary>
    public void TurnButtonMethod()
    {
        board.ChangeTurn();
    }

    /// <summary>
    /// Interfaces with button to revert move
    /// </summary>
    public void RevertButtonMethod()
    {
        board.RevertMove();
    }

    /// <summary>
    /// Interfaces with button to lock screen
    /// </summary>
    public void LockScreenMethod()
    {
        CameraMovement.LockScreen = !CameraMovement.LockScreen;
        Debug.Log("LOCK screen");
        if(CameraMovement.LockScreen)
        {
            LockText.text = "Lock Camera";
        }
        else
        {
            LockText.text = "Unlock Camera";
        }
    }

    /// <summary>
    /// Interfaces with button to mark piece
    /// </summary>
    public void MarkSelectedPieceMethod()
    {
        board.MarkSelectedPiece();
    }
}
