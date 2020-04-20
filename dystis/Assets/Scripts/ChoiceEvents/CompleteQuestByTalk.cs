using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteQuestByTalk : ChoiceEvent
{
    PlayerController playerController;
    GameObject questCompletePanel;
    public Text questPanelTitle;

    //Kyseisen questin otsikko jonka aktiivisuus halutaan tarkistaa pelaajasta
    public string questTitle;

    public override void Start()
    {
        base.Start();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        questCompletePanel = GameObject.Find("QuestCompletePanel");
    }

    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword
            && choiceID == questAcceptingChoice
            && playerController.quest.title == questTitle) {
            Debug.Log("Quest " + questTitle + " completed!");
            questPanelTitle.text = questTitle;
            playerController.quest.Complete();
        }
    }
}
