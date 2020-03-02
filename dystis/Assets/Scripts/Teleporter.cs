using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [Tooltip("Destination teleporter.")]
    public Transform teleportDestination;
    GameObject player;
    [Tooltip("Longest distance to activate teleport.")]
    float teleportActDistance = 2f;
    float teleportFadespeed = 0.5f;
    bool teleportStarting = false;
    bool teleportOnGoing = false;
    bool teleportEnding = false;
    CanvasGroup fadeOverlay;

    Vector3 tpDest;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        fadeOverlay = GameManager.FindObjectOfType<CanvasGroup>();
        //Hide teleporter meshes during runtime
        //gameObject.GetComponent<MeshRenderer>().enabled = false;

        //tpDest = teleportDestination.transform.Find("TeleporterExit").position;
        tpDest = teleportDestination.transform.position;
        
        Debug.Log("START: tpDest name = " + teleportDestination.transform.name);

    }

    private void Update() {

        if (Input.GetMouseButtonDown(0)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Vector3 playerPos = player.transform.position;
            //Vector3 distance = player.transform.position - transform.position;

            if (Physics.Raycast(ray, out hit, teleportActDistance) && hit.collider != null && hit.transform.tag == "Teleporter") {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 5f);
                //if (hit.collider != null && hit.transform.tag == "Teleporter") {
                    Debug.Log("We Clicked: " + hit.transform.name);
                    //teleportStarting = true;

                    Debug.Log("Teleport destination:" + teleportDestination.name);
                    //Debug.Log("Teleport destination:" + hit.transform.GetComponentInParent<Transform>().Find("TeleporterExit").GetComponent<Transform>().name);
                    player.transform.position = tpDest + new Vector3(0, 2, 0);

                //}
            }

        }

        //    if (teleportStarting) {
        //        fadeOverlay.alpha += Time.deltaTime * teleportFadespeed;
        //        Debug.Log("Faderoverlay Alpha: " + fadeOverlay.alpha);
        //        if (fadeOverlay.alpha >= 1f) // when the screen is fully Black
        //        {
        //            teleportStarting = false;
        //            teleportOnGoing = true;
        //            teleportEnding = false;

        //            //Debug.Log("Faderoverlay Alpha: " + fadeOverlay.alpha);
        //        }
        //    }

        //    if(teleportOnGoing) {
        //        Debug.Log("Teleport destination:" + teleportDestination.name);
        //        AudioFW.Play("teleport");
        //        tpDest = teleportDestination.transform.Find("TeleporterExit").position;
        //        player.transform.position = tpDest + new Vector3(0, 2, 0);
        //        //player.transform.position = teleportDestination.transform.fin
        //        teleportStarting = false;
        //        teleportOnGoing = false;
        //        teleportEnding = true;
        //    }

        //    if (teleportEnding) {
        //        fadeOverlay.alpha -= Time.deltaTime * teleportFadespeed;
        //        if (fadeOverlay.alpha <= 0f)
        //        {
        //            teleportStarting = false;
        //            teleportOnGoing = false;
        //            teleportEnding = false;
        //        }
        //    }
    }

}
