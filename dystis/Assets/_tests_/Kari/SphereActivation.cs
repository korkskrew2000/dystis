using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SphereActivation : Interactable {

    //public Item item;

    public UnityEvent myEvent;

    
    public override void Interact() {

        base.Interact();

        ActivateSphere();
        
        myEvent.Invoke();

    }

    void ActivateSphere() {
        Debug.Log("Activating Sphere." + transform.name);
        //bool wasPickedUp = Inventory.instance.Add(item);
        //if (wasPickedUp) {
        //    Destroy(gameObject);
        //}
    }
}
