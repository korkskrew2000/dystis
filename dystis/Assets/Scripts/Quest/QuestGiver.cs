using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : ChoiceEvent
{
    public Quest quest;

    //Jonin testiä
    GameObject questPanel;

    PlayerController playerController;
    
    //public GameObject questWindows;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text moneyText;

    public override void Start()
    {
        base.Start();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        questPanel = GameObject.Find("QuestPanel");
        //titleText = questPanel.transform.Find("Title").GetComponent<Text>();
        //descriptionText = questPanel.transform.Find("Description").GetComponent<Text>();
    }

    //RPG-talk valintoihin liittyvää asiaa:
    //quest-kysymykseen myöntävästi vastaaminen käynnistää
    //questin. ChoiceID on kysymykseen vastaamis -näppäimen
    //indeksi (0 on ensimmäinen vastausvaihtoehto, 1 toinen,
    //jne.)
    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if(questionID == questKeyword && choiceID == questAcceptingChoice)
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
        Debug.Log("accepting quest");
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
