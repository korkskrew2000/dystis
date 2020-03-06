using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereActivation : Interactable {

    //public Item item;

    public override void Interact() {

        base.Interact();

        ActivateSphere();
    }

    void ActivateSphere() {
        Debug.Log("Activating Sphere." + transform.name);
        //bool wasPickedUp = Inventory.instance.Add(item);
        //if (wasPickedUp) {
        //    Destroy(gameObject);
        //}
    }
}
