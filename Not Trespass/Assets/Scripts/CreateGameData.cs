using System;
using System.Collections;

public class CreateGameData
{

    private int m_SecretNumber;
    public int SecretNumber
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

    public string Email;

    public string OpponentEmail;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
