using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMoodByTalk : ChoiceEvent
{
    public GameObject npcToChangeMood;
    NPCControllerV2 npcController;
    public enum moodToChange { aggressive, peaceful };
    public moodToChange mood;

    public override void Start()
    {
        base.Start();
        npcController = npcToChangeMood.GetComponent<NPCControllerV2>();
    }

    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword && choiceID == questAcceptingChoice)
        {
            if((int)mood == 0)
            {
                npcController.getAngry = true;
            }
            if((int)mood == 1)
            {
                npcController.getAngry = false;
            }
        }
    }
}
