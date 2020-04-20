using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestByTalk : ChoiceEvent
{
    PlayerController playerController;
    GameObject questCompletePanel;

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
            playerController.quest.Complete();
        }
    }
}
