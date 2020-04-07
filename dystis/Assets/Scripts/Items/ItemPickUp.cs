using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : Interactable
{

    public Item item;

    public override void Interact() {

        base.Interact();

        PickUp();
    }

    void PickUp() {
        Debug.Log("Picking up item." + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        //AudioFW.Play("itempickup");
        if (wasPickedUp) {
            Destroy(gameObject);
        }

        // If item has a quest giver script, then accept the quest in it.
        var questGiverScript = this.GetComponent<QuestGiver>();
        if (questGiverScript != null) {
            questGiverScript.AcceptQuest(false);
        }
    }
}
