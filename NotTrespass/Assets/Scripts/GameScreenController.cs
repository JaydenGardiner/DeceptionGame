using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameScreenController : MonoBehaviour {

    Game curGame;

    public Dropdown Drop;

    private List<Game> m_gs;

	// Use this for initialization
	void Start () {
        UpdateGames();
	}

    public void UpdateGames()
    {
        m_gs = SharedSceneData.API.GetGames(SharedSceneData.my_user);
        List<Dropdown.OptionData> drops = new List<Dropdown.OptionData>();
        for (int i = 0; i < m_gs.Count; i++)
        {
            drops.Add(new Dropdown.OptionData(m_gs[i].getOtherPlayer(SharedSceneData.my_user) + "- " + m_gs[i].GameStatus));
        }
        Drop.options = drops;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void OnGameClick()
    {
        SharedSceneData.GameToLoad = m_gs[Drop.value];
        Game.Status status = SharedSceneData.GameToLoad.GameStatus;
        if (status == Game.Status.PENDING && SharedSceneData.GameToLoad.Player1 != SharedSceneData.my_user)
        {
            Application.LoadLevel("SecretPiece");
            Debug.Log("game status pending");
        }
        else if (status == Game.Status.PENDING || status == Game.Status.PLAYING)
        {
            Application.LoadLevel("GameScene");
            Debug.Log("game status playing");
        }
        else if (status == Game.Status.COMPLETED)
        {
            Debug.Log("Game completed");
        }
        else {
            Debug.Log("ERR no game status");
        }
        
    }


}
