using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FriendsPageActions : MonoBehaviour {

    public Dictionary<int, string> friends;
    public InputField EmailInput;
    

    public GameObject FriendTextPrefab;
    public Button AddFriendButton;
    public Button ChallengeFriendButton;
    public Button RemoveFriendButton;

    private Canvas FriendCanvas;
    private int startPos = -220;
    private int increment = -80;
    private bool m_IsTap;

    private Dictionary<int, Toggle> Toggles;
    int index;
    int numOn;

    static FriendsPageActions _instance;
    public static FriendsPageActions Instance
    {
        get
        {
            // Don't allow new instances to be created when the application is quitting to avoid the GOKit object never being destroyed.
            // These dangling instances can't be found with FindObjectOfType and so you'd get multiple instances in a scene.
            if (!_instance)
            {
                // check if there is a GO instance already available in the scene graph
                _instance = FindObjectOfType(typeof(FriendsPageActions)) as FriendsPageActions;

                // possible Unity bug with FindObjectOfType workaround
                //_instance = FindObjectOfType( typeof( Go ) ) ?? GameObject.Find( "GoKit" ).GetComponent<Go>() as Go;

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
        FriendCanvas = this.GetComponent<Canvas>();
        friends = new Dictionary<int, string>();
        Toggles = new Dictionary<int, Toggle>();
        index = 0;
        numOn = 0;


        string[] friendEmails = SharedSceneData.FriendEmails();
        print(string.Join(", ", friendEmails));
        int maxIndex = Mathf.Min(friendEmails.Length, 3);
        for (int i = 0; i < maxIndex; i++)
        {
            if (friendEmails[i] != null || friendEmails[i] != "")
            {
                EmailInput.text = friendEmails[i];
                addFriend();
            }
        }
    }

	// Use this for initialization
	public void Start () {
        UpdateFriends();

        
        
        //        EmailInput.contentType = InputField.ContentType.EmailAddress;\
    }

    void Update()
    {
        numOn = 0;
        foreach (Toggle t in Toggles.Values)
        {
            if (t.isOn)
            {
                numOn++;
            }
        }

        if (index > 3)
        {
            if (AddFriendButton != null) AddFriendButton.interactable = false;
        }
        if (numOn != 1)
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = false;
        }
        else
        {
            if (ChallengeFriendButton != null) ChallengeFriendButton.interactable = true;
        }
        if (numOn >= 1)
        {
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = true;
        }
        else
        {
            if (RemoveFriendButton != null) RemoveFriendButton.interactable = false;
        }
    }

    private void CreateText(string username)
    {
        GameObject tObject = Instantiate(FriendTextPrefab);
        tObject.tag = "friendOption";
        tObject.transform.SetParent(FriendCanvas.transform);
        RectTransform rt = tObject.GetComponent<RectTransform>();
        rt.pivot = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(600, startPos + index * increment);
        tObject.GetComponent<Text>().text = username;
        Toggles.Add(index, tObject.GetComponent<Toggle>());
        tObject.GetComponent<Toggle>().onValueChanged.AddListener(SelectItem);
        index++;
    }

    void SelectItem(bool status)
    {
        int onCount = 0;
        print("selecting");
        foreach (Toggle t in Toggles.Values)
        {
            if (t.isOn)
            {
                onCount++;
                t.GetComponent<Text>().color = Color.red;
            }
            else
            {
                t.GetComponent<Text>().color = Color.black;
            }
        }
        numOn = onCount;
    }

    public void addFriend()
    {
		SharedSceneData.API.AddFriend(EmailInput.text);
	    friends.Add(index, EmailInput.text);
	    CreateText(EmailInput.text);
	    EmailInput.text = "";
    }
    


    public void removeFriend()
    {
        print("removing");
        Dictionary<int, Toggle> nToggles = new Dictionary<int, Toggle>();
        Dictionary<int, string> strings = new Dictionary<int, string>();
        int nIndex = 0;
        foreach(int i in Toggles.Keys)
        {
            Toggle t = Toggles[i];
            if (t.isOn)
            {
				SharedSceneData.API.RemoveFriend(friends[i]);
				friends.Remove(i);
                Destroy(t.gameObject);
            }
            else
            {
                nToggles.Add(nIndex, t);
                strings.Add(nIndex, friends[i]);
                nIndex++;
            }
        }
        Toggles = nToggles;
        friends = strings;
        index = nIndex;
        ShiftList();
        //friends.Remove(EmailInput.text);
        //friend.text = "";
    }

    public void ChallengeSelected()
    {
		print ("hello world");
        int count = 0;
        int onIndex = 0;
        foreach (int i in Toggles.Keys)
        {
            Toggle t = Toggles[i];
            if (t.isOn)
            {
                if (count == 0)
                {
                    onIndex = i;
                }
                count++;
            }
        }
        print("coutn: " + count);
        if (count == 1)
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
                //Create game
                SharedSceneData.GameToLoad = new Game(SharedSceneData.API.User, friends[onIndex]);
                Application.LoadLevel("SecretPiece");
            }

			
        }
    }

    private void ShiftList()
    {
        foreach(int i in Toggles.Keys)
        {
            RectTransform rt = Toggles[i].gameObject.GetComponent<RectTransform>();
            print(i);
            rt.anchoredPosition = new Vector2(300, startPos + i * increment);
        }
    }

}
