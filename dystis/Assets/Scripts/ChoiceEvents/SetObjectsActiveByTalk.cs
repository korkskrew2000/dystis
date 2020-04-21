using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsActiveByTalk : ChoiceEvent
{
    public GameObject[] objectsToSetActive;

    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword && choiceID == questAcceptingChoice)
        {
            foreach (GameObject gO in objectsToSetActive)
            {
                gO.SetActive(true);
            }
        }
    }
}
