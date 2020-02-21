using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public GameObject pressKeyPanel;
    bool interactable = false;

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            pressKeyPanel.SetActive(true);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            pressKeyPanel.SetActive(false);
            interactable = false;
        }
    }

    private void Update() {
        if (interactable) {
            if (Input.GetKeyDown(KeyCode.E)) {
                print(item.name + " added to inventory");
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
        }
    }
}
