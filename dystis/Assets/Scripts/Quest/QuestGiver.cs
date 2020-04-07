using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public RPGTalk rpgTalk;
    public Quest quest;
    public int questAcceptingChoice;

    //Jonin testiä
    public GameObject questPanel;

    public PlayerController playerController;
    
    //public GameObject questWindows;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text moneyText;

    void Start()
    {
        if (rpgTalk != null)
        {
            rpgTalk.OnMadeChoice += OnMadeChoice;
        }
    }


    //RPG-talk valintoihin liittyvää asiaa:
    //quest-kysymykseen myöntävästi vastaaminen käynnistää
    //questin. ChoiceID on kysymykseen vastaamis -näppäimen
    //indeksi (0 on ensimmäinen vastausvaihtoehto, 1 toinen,
    //jne.)
    void OnMadeChoice(string questionID, int choiceID)
    {
        Debug.Log("Aha! In the question " + questionID + " you choosed the option " + choiceID);
        if(questionID == "quest" && choiceID == questAcceptingChoice)
        {
            AcceptQuest(true);
        }
    }

    public void OpenQuestWindow() {
        //questWindows.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        moneyText.text = quest.moneyReward.ToString();
    }
    

    //if quest is accepted from NPC in a conversation
    //bool questFromTalk == true and quest window doesn't
    //pop up in middle of conversation
    public void AcceptQuest(bool questFromTalk) {
        //questWindows.SetActive(false);
        quest.isActive = true;
        playerController.quest = quest;
        titleText.text = quest.title;
        descriptionText.text = quest.description;

        //Jonin testiä
        if (!questFromTalk)
        {
            playerController.SwitchStateAndMovement(playerController.menuPanel);
            playerController.questPanel.SetActive(true);
        }

        //Text playerTitleText = playerController.questPanel.transform.GetChild(0).GetComponent<Text>();
        //Debug.Log(playerTitleText.text);
        //playerTitleText.text = titleText.text;
        //Debug.Log(playerTitleText.text);
        //Debug.Log(titleText.text);
        //Text playerDescriptionText = playerController.questPanel.transform.GetChild(1).GetComponent<Text>();
        //playerDescriptionText = descriptionText;
    }
}
