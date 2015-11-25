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
        switch(SharedSceneData.GameToLoad.GameStatus)
        {
            case Game.Status.PENDING:
                //load secret piece scene
                Debug.Log("game status pending");
                break;
            case Game.Status.PLAYING:
                Application.LoadLevel("GameScene");
                Debug.Log("game status playing");
                break;
            case Game.Status.COMPLETED:
                Debug.Log("Game completed");
                break;
            default:
                Debug.Log("ERR no game status");
                break;
        }
        
    }


}
