using System;
using System.Collections;
using System.Collections.Generic;

public static class SharedSceneData
{
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

    public static string Email;

    public static List<string> FriendEmails;

    //change to Game object
    public static int[][] GameToLoad;

}
