using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteQuestByTalk : ChoiceEvent
{
    PlayerController playerController;
    GameObject questCompletePanel;
    public Text questPanelTitle;
    public string questItemName;

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
            questPanelTitle.text = questTitle + ":";
            playerController.quest.Complete();

            Inventory inventory = Inventory.instance;
            for (int i = 0; i < inventory.items.Count; i++)
            {
                if (inventory.items[i].name == questItemName)
                {
                    inventory.items[i].isQuestItem = false;
                    inventory.items[i].RemoveFromInventory(false);
                }
            }
        }
    }
}
