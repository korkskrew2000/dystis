using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestComplete : MonoBehaviour
{
    public GameObject questCompletePanel;
    Quest quest;
    PlayerController playerController;
    Text questTitle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerController = other.GetComponent<PlayerController>();
            if (playerController.quest.isActive == true) {
                quest = playerController.quest;
                questCompletePanel.transform.Find("Title").GetComponent<Text>().text = quest.title;
                playerController.SwitchStateAndMovement(questCompletePanel);

                playerController.quest = null;
            }
        }
    }

    public void ClosePanel()
    {
        playerController.SwitchStateAndMovement(questCompletePanel);
    }
}
