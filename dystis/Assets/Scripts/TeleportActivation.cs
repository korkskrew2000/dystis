﻿using UnityEngine;

public class TeleportActivation : Interactable {
    
    public Transform tpDestination;

    Transform player;

    public bool tpShowDuringPlay = false;

    public override void Interact() {

        base.Interact();

        TeleportActivate();
    }

    void Awake() {
        if (!tpShowDuringPlay) {
            GetComponent<MeshRenderer>().enabled = false;
            transform.parent.Find("TeleporterExit").GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void TeleportActivate() {
        //Debug.Log("Activating teleport: " + transform.name);
        player.GetComponent<PlayerController>().tpDestination = tpDestination;
        player.GetComponent<PlayerController>().teleportStarting = true;

    }
}
