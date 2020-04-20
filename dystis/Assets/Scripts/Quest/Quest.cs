using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public float moneyReward;

    GameObject questPanel;

    PlayerController playerController;

    public Text titleText;
    public Text descriptionText;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        questPanel = GameObject.Find("QuestPanel");
        //titleText = questPanel.GetComponentInChildren<Text>();
        //descriptionText = questPanel.GetComponentInChildren<Text>();
    }

    public void Complete()
    {
        isActive = false;
        titleText.text = null;
        descriptionText.text = null;
    }
}
