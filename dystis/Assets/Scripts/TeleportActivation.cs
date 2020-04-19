using UnityEngine;

public class TeleportActivation : Interactable {
    
    public Transform tpDestination;

    Transform player;

    public bool tpLocked = false;   // Player is not allowed to use this tp.
    public bool tpSubway = false;   // Is this teleport actually tp for subway fast travel?
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
        if (!tpLocked) {
            //Debug.Log("Activating teleport: " + transform.name);
            player.GetComponent<PlayerController>().tpDestination = tpDestination;
            player.GetComponent<PlayerController>().teleportStarting = true;
        } else {
            Debug.Log("Teleport locked: " + transform.name);
            AudioFW.Play("doorlocked");
        }

    }
}
