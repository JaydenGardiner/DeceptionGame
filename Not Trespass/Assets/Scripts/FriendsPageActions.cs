using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FriendsPageActions : MonoBehaviour {

    public ArrayList friends;
    public InputField EmailInput;
    public Text friend;
    

	// Use this for initialization
	public void Start () {
        friends = new ArrayList();
        //        EmailInput.contentType = InputField.ContentType.EmailAddress;\
    }
	
	// Update is called once per frame
	public void Update () {
	
	}

    public void addFriend()
    {
        if (EmailInput.text != "")
        {
            friends.Add(EmailInput.text);
            friend.text = EmailInput.text;
            EmailInput.text = "";
            
        }
    }

        public void removeFriend()
    {
        friends.Remove(EmailInput.text);
        friend.text = "";
    }

}
