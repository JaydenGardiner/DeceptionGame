using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

public class FriendsPageActions : MonoBehaviour {

    public InputField EmailInput;

    public Dropdown Drop;

    public Button AddFriendButton;
    public Button ChallengeFriendButton;
    public Button RemoveFriendButton;


    //int index;
    //bool itemSelected;

    static FriendsPageActions _instance;
    public static FriendsPageActions Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(FriendsPageActions)) as FriendsPageActions;

                // nope, create a new one
                if (!_instance)
                {
                    var obj = new GameObject("friend actions");
                    _instance = obj.AddComponent<FriendsPageActions>();
                    DontDestroyOnLoad(obj);
                }
            }

            return _instance;
        }
    }

    public void UpdateFriends()
    {
        string[] friendEmails;

        friendEmails = SharedSceneData.FriendEmails();
        List<Dropdown.OptionData> drops = new List<Dropdown.OptionData>();
        print(string.Join(", ", friendEmails));
        for (int i = 0; i < friendEmails.Length; i++)
        {
            if (friendEmails[i] != null && friendEmails[i] != "")
            {
                drops.Add(new Dropdown.OptionData(friendEmails[i]));
            }
        }
        Debug.Log(Drop);
        Drop.options = drops;
    }

	// Use this for initialization
	public void Start () {
        UpdateFriends();
        

        
        
        //        EmailInput.contentType = InputField.ContentType.EmailAddress;\
    }

    void Update()
    {
        if (true)//itemSelected)
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = true;
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = true;

        }
        else
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = false;
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = false;
        }
        if (EmailInput.text == "")
        {
            AddFriendButton.interactable = false;
        }
        else
        {
            AddFriendButton.interactable = true;
        }

    }

    public void SelectFriend()
    {
        Debug.Log("Selecting friend");
        //itemSelected = true;
    }

    public void addFriend()
    {
        try
        {
            SharedSceneData.API.AddFriend(EmailInput.text);
        }
        catch (NullReferenceException n)
        {
            Debug.Log(n.Message);
            Application.LoadLevel("ErrorScene");
            ErrorHandler.ErrorMessage = "Could not connect to database.";
        }
		
        List<Dropdown.OptionData> drops = Drop.options;
        drops.Add(new Dropdown.OptionData(EmailInput.text));
        Drop.options = drops;
	    EmailInput.text = "";
    }
    


    public void removeFriend()
    {
        if (true)//itemSelected)
        {
            Debug.Log("removing friend");
            string selected = Drop.options[Drop.value].text;
            SharedSceneData.API.RemoveFriend(selected);
            //itemSelected = false;
            Drop.value = 0;
            UpdateFriends();
        }

		
    }

    public void ChallengeSelected()
    {
        Debug.Log("index: " + Drop.value);
        if (true)//itemSelected)
        {
            //SharedSceneData.OpponentEmail = friends[onIndex];
            //SharedSceneData.FriendEmails = new List<string>();
           // foreach(string friend in friends.Values)
          //  {
          //      SharedSceneData.FriendEmails.Add(friend);
           // }
            //TODO check if game exists between these 2 people
            bool gameExists = false;
            if (gameExists)
            {
                //Load game
            }
            else
            {
                Debug.Log("start game");
                //Create game
                Debug.Log(Drop.value);
                Debug.Log(Drop.options.Count);
                SharedSceneData.GameToLoad = new Game(SharedSceneData.API.User, Drop.options[Drop.value].text);
                Application.LoadLevel("SecretPiece");
            }
            //itemSelected = false;
            Drop.value = 0;

			
        }
    }

}
