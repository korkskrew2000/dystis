using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [Tooltip("Destination teleporter.")]
    public Transform teleportDestination;
    GameObject player;
    [Tooltip("What is the longest distance to activate teleport.")]
    public float teleportActDistance = 2f ;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        //Hide teleporter meshes during runtime
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnMouseDown() {
        //Debug.Log("Mouse Down");

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 playerPos = player.transform.position;
        Vector3 distance = player.transform.position - transform.position;

        if (Physics.Raycast(ray, out hit, teleportActDistance)) {
            //Debug.DrawLine(ray.origin, hit.point, Color.red, 5f);
            if (hit.collider != null && (hit.transform.tag == "Teleporter") ) {
                //Debug.Log("We Clicked: " + this.name);
                AudioFW.Play("teleport");
                player.transform.position = teleportDestination.transform.Find("TeleporterExit").position + new Vector3(0, 2, 0);
            }


        }
    }
}
