using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    public Transform teleportDestination;
    public GameObject playerObject;
    bool teleportInPhase = false;

    // Start is called before the first frame update
    void Start() {

    }

    private void OnMouseDown() {
        Debug.Log("Mouse Down");
    }
    // Update is called once per frame
    void Update() {
        //Vector3 target = teleportTarget.transform.Find("TeleporterPlate").position;
        //Debug.DrawRay(playerObject.transform.position, target, Color.green);

        if (Input.GetMouseButtonDown(0)) {


            //Debug.Log("Mouse button pressed");

            //if (!teleportInPhase) {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 playerPos = playerObject.transform.position;

                if (Physics.Raycast(ray, out hit)) {

                    Vector3 targetPos = hit.transform.position;
                //var direction = playerPos - targetPos;
                //Debug.DrawRay(playerPos, direction, Color.green, 0.5f);

                    //Debug.Log("Raycast hit OK name: " + hit.transform.name);
                    if (hit.collider != null && (hit.transform.name == "TeleporterActivation")) {
                        var tpClicked = hit.transform.parent;
                        Debug.Log("TP Clicked" + tpClicked.name);
                        //teleportInPhase = true;
                        Debug.Log("we hit: " + hit.transform.name + " in: " + hit.transform.parent.name);
                        playerObject.transform.position = teleportDestination.transform.Find("TeleporterPlate").position + new Vector3(0, 2, 0);
                    }
                }
                //teleportInPhase = false;
            //}
        }
                   
    }
}
