using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private GameObject triggeringNPC;
    private bool triggering;
    private bool conversating;
    public GameObject npcText;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "NPC") {
            npcText.SetActive(true);
            triggering = true;
            triggeringNPC = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "NPC") {
            npcText.SetActive(false);
            triggering = false;
            triggeringNPC.GetComponent<DialogueManager>().EndDialogue();
            triggeringNPC = null;
        }
    }

    // Update is called once per frame
    void Update() {
        if (triggering) {
            if (Input.GetKeyDown(KeyCode.E)) {
                triggering = false;
                npcText.SetActive(false);
                conversating = true;
                triggeringNPC.GetComponent<DialogueManager>().TriggerDialogue();
            }
        }
        if (conversating) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                triggeringNPC.GetComponent<DialogueManager>().DisplayNextSentence();
            }
        }
    }
}
