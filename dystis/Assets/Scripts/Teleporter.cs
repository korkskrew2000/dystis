using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [Tooltip("Destination teleporter.")]
    public Transform teleportDestination;
    GameObject player;
    bool teleportInPhase = false;
    [Tooltip("What is the longest distance teleport activates.")]
    public float teleportMaxActivationDistance = 5f ;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnMouseDown() {
        //Debug.Log("Mouse Down");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        Vector3 distance = player.transform.position - transform.position;

        if (Physics.Raycast(ray, out hit)) {

            //Vector3 targetPos = hit.transform.position;
            //var direction = playerPos - targetPos;
            //Debug.DrawRay(playerPos, direction, Color.green, 0.5f);
            //Debug.Log("Raycast hit OK name: " + hit.transform.name);
            Debug.Log("Distance to teleporter" + distance.magnitude);
            //if (hit.collider != null && (hit.transform.tag == "Teleporter") && (distance.magnitude < teleportMaxActivationDistance)) {
            if (hit.collider != null && (hit.transform.tag == "Teleporter") ) {
            Debug.Log("We Clicked: " + this.name);
                //teleportInPhase = true;
                AudioFW.Play("teleport");
                player.transform.position = teleportDestination.transform.Find("TeleporterExit").position + new Vector3(0, 2, 0);
            }


        }
    }
}
