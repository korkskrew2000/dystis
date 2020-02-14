using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    public Transform teleportDestination;
    public GameObject playerToTeleport;
    bool teleportInPhase = false;

    // Start is called before the first frame update
    void Start() {

    }

    private void OnMouseDown() {
        //Debug.Log("Mouse Down");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 playerPos = playerToTeleport.transform.position;

        if (Physics.Raycast(ray, out hit)) {

            //Vector3 targetPos = hit.transform.position;
            //var direction = playerPos - targetPos;
            //Debug.DrawRay(playerPos, direction, Color.green, 0.5f);
            //Debug.Log("Raycast hit OK name: " + hit.transform.name);
            
            if (hit.collider != null && (hit.transform.tag == "Teleporter")) {
                Debug.Log("We Clicked: " + this.name);
                //teleportInPhase = true;
                AudioFW.Play("teleport");
                playerToTeleport.transform.position = teleportDestination.transform.Find("TeleporterExit").position + new Vector3(0, 2, 0);
            }


        }
    }
}
