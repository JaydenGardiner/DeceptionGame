using System;
using System.Collections;
using System.Collections.Generic;

public static class SharedSceneData
{
    private static int m_SecretNumber;
	private static GameApi api = GameApi.getInstance("", "");
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

    public static string Email;

    public static string[] FriendEmails() {
		return api.GetFriends ();
	}

    //change to Game object
    public static int[][] GameToLoad;

}
