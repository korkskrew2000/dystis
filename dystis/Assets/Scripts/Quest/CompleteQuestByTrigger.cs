using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteQuestByTrigger : MonoBehaviour
{
    public string questTitle;
    GameObject questCompletePanel;
    Quest quest;
    PlayerController playerController;
    public Text questPanelTitle;

    private void Start()
    {
        //questPanelTitle = questCompletePanel.transform.Find("Title").GetComponent<Text>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        questCompletePanel = GameObject.Find("QuestCompletePanel");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerController.quest.isActive == true && questTitle == playerController.quest.title) {
                quest = playerController.quest;
                questPanelTitle.text = quest.title;
                playerController.SwitchStateAndMovement(questCompletePanel);

                playerController.quest.Complete();
            }
        }
    }
}
