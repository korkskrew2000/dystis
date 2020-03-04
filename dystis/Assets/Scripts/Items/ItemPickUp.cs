using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : Interactable
{
    public Item item;
    public GameObject pressKeyPanel;
    bool interactable = false;

    public override void Interact() {
        print(item.name + " added to inventory");
        PickUp();
    }

    void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp) Destroy(gameObject);
    }
}
