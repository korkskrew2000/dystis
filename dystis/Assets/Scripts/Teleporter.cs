using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
    [Tooltip("Destination teleporter.")]
    public Transform teleportDestination;
    GameObject player;
    Camera cam;
    [Tooltip("Longest distance to activate teleport.")]
    public float teleportActDistance = 2f;
    public bool teleportInProgress;

    CanvasGroup fadeOverlay;


    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        fadeOverlay = GameManager.FindObjectOfType<CanvasGroup>();
        //Hide teleporter meshes during runtime
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    private void Update() {

        if (Input.GetMouseButtonDown(1)) {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 playerPos = player.transform.position;
            Vector3 distance = player.transform.position - transform.position;

            if (Physics.Raycast(ray, out hit, teleportActDistance)) {
                //Debug.DrawLine(ray.origin, hit.point, Color.red, 5f);
                if (hit.collider != null && (hit.transform.tag == "Teleporter")) {
                    Debug.Log("We Clicked: " + GetComponentInParent<Transform>().name);
                    teleportInProgress = true;
                player.transform.position = teleportDestination.transform.Find("TeleporterExit").position + new Vector3(0, 2, 0);
                }
            }

            if (teleportInProgress) {
                AudioFW.Play("teleport");
                fadeOverlay.alpha = (fadeOverlay.alpha + (Time.deltaTime * 0.1f));
                //player.transform.position = teleportDestination.transform.Find("TeleporterExit").position + new Vector3(0, 2, 0);

                if (fadeOverlay.alpha == 1f) // when the screen is fully Black
                {
                    teleportInProgress = false;
                }
            }

        }
    }

}
