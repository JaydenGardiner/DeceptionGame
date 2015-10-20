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

    private Canvas FriendCanvas;
    private int startPos = -220;
    private int increment = -80;
    private bool m_IsTap;

    private Dictionary<int, Toggle> Toggles;
    int index;

	// Use this for initialization
	public void Start () {
        FriendCanvas = this.GetComponent<Canvas>();
        friends = new Dictionary<int, string>();
        Toggles = new Dictionary<int, Toggle>();
        index = 0;
        //        EmailInput.contentType = InputField.ContentType.EmailAddress;\
    }

    void Update()
    {
        if (index > 3)
        {
            AddFriendButton.interactable = false;
        }
    }

    private void CreateText(string email)
    {
        GameObject tObject = Instantiate(FriendTextPrefab);
        tObject.tag = "friendOption";
        tObject.transform.SetParent(FriendCanvas.transform);
        RectTransform rt = tObject.GetComponent<RectTransform>();
        rt.pivot = new Vector2(1f, 0f);
        rt.anchoredPosition = new Vector2(300, startPos + index * increment);
        tObject.GetComponent<Text>().text = EmailInput.text;
        Toggles.Add(index, tObject.GetComponent<Toggle>());
        tObject.GetComponent<Toggle>().onValueChanged.AddListener(SelectItem);
        index++;
    }

    void SelectItem(bool status)
    {
        print("selecting");
        foreach (Toggle t in Toggles.Values)
        {
            if (t.isOn)
            {
                t.GetComponent<Text>().color = Color.red;
            }
            else
            {
                t.GetComponent<Text>().color = Color.black;
            }
        }
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
        if (count == 1)
        {
            SharedSceneData.OpponentEmail = friends[onIndex];
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
