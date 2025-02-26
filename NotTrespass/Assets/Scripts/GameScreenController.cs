﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Controls the games canvas in the main menu
/// </summary>
public class GameScreenController : MonoBehaviour {

    Game curGame;

    public Dropdown Drop;

    private List<Game> m_gs;

	// Use this for initialization
	void Start() {
        UpdateGames();
	}

    /// <summary>
    /// Updates list of games from server
    /// </summary>
    public void UpdateGames()
    {
        try
        {
            m_gs = SharedSceneData.API.GetGames(SharedSceneData.my_user);
        }
        catch (NullReferenceException n)
        {
            Debug.Log(SharedSceneData.API);
            m_gs = new List<Game>();
            ErrorHandler.ErrorMessage = "No connection could be made to the server.";
            ErrorHandler.SceneToLoad = "MenuScreen";
            Application.LoadLevel("ErrorScene");
        }
        catch (Newtonsoft.Json.JsonReaderException j)
        {
            Debug.Log(SharedSceneData.API);
            m_gs = new List<Game>();
            ErrorHandler.ErrorMessage = "No connection could be made to the server.";
            ErrorHandler.SceneToLoad = "MenuScreen";
            Application.LoadLevel("ErrorScene");
        }
        
        List<Dropdown.OptionData> drops = new List<Dropdown.OptionData>();
        for (int i = 0; i < m_gs.Count; i++)
        {
            drops.Add(new Dropdown.OptionData(m_gs[i].getOtherPlayer(SharedSceneData.my_user) + "- " + m_gs[i].GameStatus));
            Debug.Log(m_gs[i].getOtherPlayer(SharedSceneData.my_user));
        }
        Drop.options = drops;
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    /// <summary>
    /// Loads the game depending on what is clicked
    /// </summary>
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
