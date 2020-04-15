using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorByTalk : ChoiceEvent
{
    public TeleportActivation teleportActivatorToUnlock;

    //RPG-talk valintoihin liittyvää asiaa:
    //quest-kysymykseen myöntävästi vastaaminen käynnistää
    //questin. ChoiceID on kysymykseen vastaamis -näppäimen
    //indeksi (0 on ensimmäinen vastausvaihtoehto, 1 toinen,
    //jne.)
    public override void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword && choiceID == questAcceptingChoice)
        {
            teleportActivatorToUnlock.tpLocked = false;
        }
    }
}