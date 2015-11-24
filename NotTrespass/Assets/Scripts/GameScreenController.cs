using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameScreenController : MonoBehaviour {

    Game curGame;

    public Text gButton;

	// Use this for initialization
	void Start () {
        Game[] gs = SharedSceneData.API.GetGames("thomas");
        curGame = gs[0];
        gButton.text = curGame.GameID.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnGameClick()
    {
        SharedSceneData.GameToLoad = curGame;
        Application.LoadLevel("GameScene");
    }


}
