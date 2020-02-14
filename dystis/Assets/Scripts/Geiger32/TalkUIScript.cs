using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TalkUIScript : MonoBehaviour {

    string playerName;
    public List<string> playerNamesList;
    string playerQuote;
    public List<Text> nameText;
    public List<Text> quoteText;
    public Sprite playerPicture;
    public GameObject pPic;
    public List<Sprite> playerPicturesList;
    public List<GameObject> getPlayerInfo;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void SendUiInfo(int i, string a) {

        playerName = playerNamesList[i];
        playerQuote = a;
        nameText[0].text = playerName;
        quoteText[0].text = a;
        if (playerPicturesList[i] != null) {
            playerPicture = playerPicturesList[i];
            Image image;
            image = pPic.GetComponent<Image>();
            image.sprite = playerPicture;

        }

    }
    public void SendHintInfo(string hint, string text) {

        nameText[1].text = hint;
        quoteText[1].text = text;

    }
}

