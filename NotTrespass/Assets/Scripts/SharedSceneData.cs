using System;
using System.Collections;
using System.Collections.Generic;

public static class SharedSceneData
{
    private static int m_SecretNumber;
    //David- this now comes from LoginSceneController LoadMenu method
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

    public static string[] FriendEmails() {
		return API.GetFriends ();
	}

    //change to Game object
	public static Game GameToLoad;

}
