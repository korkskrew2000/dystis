using UnityEngine;

public class TeleportActivation : Interactable {
    
    public Transform tpDestination;

    public override void Interact() {

        base.Interact();

        TeleportActivate();
    }

    private void Start() {
        //Transform player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void TeleportActivate() {
        Debug.Log("Activating teleport." + transform.name);
        //bool wasPickedUp = Inventory.instance.Add(item);
        //if (wasPickedUp) {
        //    Destroy(gameObject);
        //}
        //player.position = tpDestination.position + new Vector3(0, 2, 0);
        
    }
}
