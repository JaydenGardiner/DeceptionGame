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


    int index;
    bool itemSelected;

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
        index = 0;

        string[] friendEmails;

        friendEmails = SharedSceneData.FriendEmails();
        List<Dropdown.OptionData> drops = new List<Dropdown.OptionData>();
        drops.Add(new Dropdown.OptionData("Select a friend..."));
        print(string.Join(", ", friendEmails));
        for (int i = 0; i < friendEmails.Length; i++)
        {
            if (friendEmails[i] != null && friendEmails[i] != "")
            {
                drops.Add(new Dropdown.OptionData(friendEmails[i]));
            }
        }
        Drop.options = drops;
    }

	// Use this for initialization
	public void Start () {
        UpdateFriends();
        

        
        
        //        EmailInput.contentType = InputField.ContentType.EmailAddress;\
    }

    void Update()
    {
        if (itemSelected && index >= 0)
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = true;
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = true;

        }
        else
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = false;
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = false;
        }
    }

    public void SelectFriend()
    {
        index = Drop.value-1;
        itemSelected = true;
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
        if (itemSelected && index > 0)
        {
            string selected = Drop.options[index].text;
            SharedSceneData.API.RemoveFriend(selected);
            itemSelected = false;
            Drop.value = 0;
        }

		
    }

    public void ChallengeSelected()
    {
        if (itemSelected && index > 0)
        {
            itemSelected = false;
            Drop.value = 0;
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
                //Create game
                SharedSceneData.GameToLoad = new Game(SharedSceneData.API.User, Drop.options[index].text);
                Application.LoadLevel("SecretPiece");
            }

			
        }
    }

}
