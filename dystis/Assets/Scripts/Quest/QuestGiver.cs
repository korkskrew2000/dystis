using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    public PlayerController playerController;
    
    //public GameObject questWindows;
    public Text titleText;
    public Text descriptionText;
    public Text experienceText;
    public Text moneyText;

    public void OpenQuestWindow() {
        //questWindows.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        moneyText.text = quest.moneyReward.ToString();
    }
    
    public void AcceptQuest() {
        //questWindows.SetActive(false);
        quest.isActive = true;
        playerController.quest = quest;

    }
}
