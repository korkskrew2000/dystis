using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    GameObject player;
    public GameObject inventoryPanel;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void DisablePlayerMovement() {
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = false;
        player.GetComponent<FirstPersonAIO>().playerCanMove = false;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
        player.GetComponent<FirstPersonAIO>().autoCrosshair = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnablePlayerMovement() {
        player.GetComponent<FirstPersonAIO>().playerCanMove = true;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        player.GetComponent<FirstPersonAIO>().autoCrosshair = true;
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
   void Update() {
        if (Input.GetButtonDown("Inventory")) {
            SwitchState(inventoryPanel);
        }
   //    if (Input.GetKeyDown(KeyCode.Mouse0)) {
   //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   //        RaycastHit hit;
   //        if (Physics.Raycast(ray, out hit)) {
   //            Debug.Log(hit.transform.name);
   //            if (hit.transform.tag == "NPC") {
   //                Debug.Log(" :: NPC Clicked :: ");
   //                //var c = hit.collider.GetComponent<ITalkable>();
   //                var c = hit.collider.GetComponentInParent<ITalkable>();
   //                if (c != null)
   //                    //Debug.Log(" :: c != null :: ");
   //                    //c.TalkWith();
   //                    hit.transform.parent.GetComponent<NPCController>().TalkWith();
   //            }
   //        }
   //    }
   }

    void SwitchState(GameObject gO) {
        if (gO.activeSelf == true) {
            gO.SetActive(false);
            EnablePlayerMovement();
        }
        else {
            gO.SetActive(true);
            DisablePlayerMovement();
        }
    }
}
