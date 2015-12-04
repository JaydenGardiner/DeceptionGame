using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginSceneController : MonoBehaviour {

    public InputField UserF;
    public InputField PassF;

    public Text UserErr;
    public Text PassErr;

    public Button CreateButton;
    public Button LoginButton;

    private bool m_validUser, m_validPass;
    private string m_userS;
    private string m_passwordS;

    private string m_passwordErrorString = "Password must be less than 16 characters";
    private string m_userErrorString = "Username must be less than 10 characters";

	// Use this for initialization
	void Start () {
        m_validUser = false;
        m_validPass = false;
        UserF.onEndEdit.AddListener(SubmitUser);
        PassF.onEndEdit.AddListener(SubmitPassword);
        PassF.inputType = InputField.InputType.Password;
	}
	
	// Update is called once per frame
	void Update () {
	    if (m_validUser && m_validPass)
        {
            CreateButton.enabled = true;
            LoginButton.enabled = true;
            UserErr.text = "";
            PassErr.text = "";
        }
        else if (m_validUser && !m_validPass)
        {
            CreateButton.enabled = false;
            LoginButton.enabled = false;
            PassErr.text = m_passwordErrorString;
            UserErr.text = "";
        }
        else if (!m_validUser && m_validPass)
        {
            CreateButton.enabled = false;
            LoginButton.enabled = false;
            UserErr.text = m_userErrorString;
            PassErr.text = "";
        }
        else
        {
            CreateButton.enabled = false;
            LoginButton.enabled = false;
            UserErr.text = m_userErrorString;
            PassErr.text = m_passwordErrorString;
        }
	}

    public void CreateAccount()
    {
        //TODO  if access to server
        bool connected = true;
        if (connected)
        {
            //TODO Create account, handle errors
            bool noErrors = true;
            if (noErrors)
            {
                LoadMenu();
            }
        }
    }

    public void LoginToAccount()
    {
        //TODO  if access to server
        bool connected = true;
        if (connected)
        {
            //TODO Login, hangle errors
            bool noErrors = true;
            if (noErrors)
            {
                LoadMenu();
            }
        }
    }

    private void LoadMenu()
    {
        //TODO Set any game options here like player
        SharedSceneData.my_user = m_userS;
        SharedSceneData.API = GameApi.getInstance(SharedSceneData.my_user, "");
        Debug.Log("loading menu");
        Application.LoadLevel("MenuScreen");
    }

    private void SubmitUser(string arg)
    {
        if (IsValidUsername(arg))
        {
            m_userS = arg;
            m_validUser = true;
        }
        else
        {
            m_validUser = false;
        }
    }

    private void SubmitPassword(string arg)
    {
        if (IsValidPassword(arg))
        {
            m_passwordS = arg;
            m_validPass = true;
        }
        else
        {
            m_validPass = false;
        }
    }

    private bool IsValidUsername(string user)
    {
        if (user.Length <= 10 && user.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsValidPassword(string pass)
    {
        if (pass.Length <= 16 && pass.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
