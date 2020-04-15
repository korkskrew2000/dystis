using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestByTalk : ChoiceEvent
{
    PlayerController playerController;

    //Kyseisen questin otsikko jonka aktiivisuus halutaan tarkistaa pelaajasta
    public string questTitle;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public virtual void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword
            && choiceID == questAcceptingChoice
            && playerController.quest.title == questTitle) {
            Debug.Log("Quest " + questTitle + " completed!");
            playerController.quest = null;
        }
    }
}
