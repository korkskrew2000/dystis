using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItemByTalk : ChoiceEvent
{
    public Item itemToGive;

    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword && choiceID == questAcceptingChoice)
        {
            Inventory.instance.Add(itemToGive);
        }
    }
}
