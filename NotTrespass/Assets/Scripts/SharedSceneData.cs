using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Holds data that needs to be transfered between scenes
/// </summary>
public static class SharedSceneData
{
    //David- this now comes from LoginSceneController LoadMenu method
    //Current user
    public static String my_user = null;
    public static int my_team
    {
        get
        {
            if (GameToLoad == null) return -1;
            else if (my_user == GameToLoad.Player1) return 0;
            else if (my_user == GameToLoad.Player2) return 1;
            else return -1;
        }
    }

    public static bool my_turn
    {
        get
        {
            return my_user == GameToLoad.CurrentMove && GameToLoad.GameStatus != Game.Status.COMPLETED;
        }
    }
    public static GameApi API = null;
    
    //Todo- possibly unnecessary
    private static int m_SecretNumber;
    public static int SecretNumber
    {
        get { return m_SecretNumber; }
        set
        {
            if (value >= 0 && value <= 9)
            {
                m_SecretNumber = value;
            }
            else
            {
                throw new Exception("Failed to set secret number");
            }
        }
    }

    /// <summary>
    /// Get friends for user
    /// </summary>
    /// <returns>
    /// Friends list
    /// </returns>
    public static string[] FriendEmails() {
		return API.GetFriends ();
	}

    //Game to load
	public static Game GameToLoad;

}
