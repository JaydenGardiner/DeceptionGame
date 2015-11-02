using System;
using System.Collections;
using System.Collections.Generic;

public static class SharedSceneData
{
    private static int m_SecretNumber;
	// TODO: this should get passed in
	private static String my_user = "georgep";
	public static GameApi API = GameApi.getInstance(my_user, "");
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
