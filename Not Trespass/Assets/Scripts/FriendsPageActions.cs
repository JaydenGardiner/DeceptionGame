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



	// Use this for initialization
	public void Start () {
        FriendCanvas = this.GetComponent<Canvas>();
        friends = new Dictionary<int, string>();
        Toggles = new Dictionary<int, Toggle>();
        index = 0;
        numOn = 0;
        
        if (SharedSceneData.FriendEmails != null)
        {
            print(string.Join(", ", SharedSceneData.FriendEmails.ToArray()));
            int maxIndex = Mathf.Min(SharedSceneData.FriendEmails.Count, 3);
            for (int i = 0; i < maxIndex; i++)
            {
                if (SharedSceneData.FriendEmails[i] != null || SharedSceneData.FriendEmails[i] != "")
                {
                    EmailInput.text = SharedSceneData.FriendEmails[i];
                    addFriend();
                }
            }
        }
        
        
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
            AddFriendButton.interactable = false;
        }
        if (numOn != 1)
        {
            ChallengeFriendButton.interactable = false;
        }
        else
        {
            ChallengeFriendButton.interactable = true;
        }
        if (numOn >= 1)
        {
            RemoveFriendButton.interactable = true;
        }
        else
        {
            RemoveFriendButton.interactable = false;
        }
    }

    private void CreateText(string email)
    {
        GameObject tObject = Instantiate(FriendTextPrefab);
        tObject.tag = "friendOption";
        tObject.transform.SetParent(FriendCanvas.transform);
        RectTransform rt = tObject.GetComponent<RectTransform>();
        rt.pivot = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(600, startPos + index * increment);
        tObject.GetComponent<Text>().text = EmailInput.text;
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
        Debug.Log("index: " + index.ToString());
        if (Regex.IsMatch(EmailInput.text, ".*@.*..*"))
        {
            if (EmailInput.text.Length <= 30)
            {
                friends.Add(index, EmailInput.text);
                //friend.text = EmailInput.text;
                CreateText(EmailInput.text);
                EmailInput.text = "";
            }
        }
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
            SharedSceneData.FriendEmails = new List<string>();
            foreach(string friend in friends.Values)
            {
                SharedSceneData.FriendEmails.Add(friend);
            }
            Application.LoadLevel("SecretPiece");
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
