using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public Camera miniMapCamera;
    public GameObject player;
    public GameObject playerMapMarker;
    public GameObject playerOverlayCanvas;
    public GameObject miniMapImage;
    public GameObject miniMapOff;
    public float mapUpdateWaitTime = 0.05f;
    public bool rotateMiniMapCamera = false;
    float timer = 0.0f;

    public int worldWidth = 1000;   // x
    public int worldLength = 1000;  // z

    void Start() {
        player = GameObject.FindWithTag("Player");
        playerMapMarker = GameObject.FindWithTag("PlayerMapMarker");
        playerOverlayCanvas = GameObject.FindWithTag("PlayerOverlayCanvas");
        miniMapImage = GameObject.FindWithTag("MiniMapImage");
        miniMapOff = GameObject.FindWithTag("MiniMapOff");
    }

    void Update() {

        // https://docs.unity3d.com/ScriptReference/Time-deltaTime.html

        timer += Time.deltaTime;

        if (timer > mapUpdateWaitTime) {

            if (rotateMiniMapCamera) {
                float playerCameraRotY = player.transform.eulerAngles.y;
                miniMapCamera.transform.eulerAngles = new Vector3(90, playerCameraRotY, 0);
            }
            miniMapCamera.transform.position = new Vector3(player.transform.position.x,miniMapCamera.transform.position.y, player.transform.position.z);
            timer -= mapUpdateWaitTime;
            //Debug.Log("Timer: " + timer);

            if(player.transform.position.y < -10) {
                playerMapMarker.SetActive(false);
                miniMapCamera.enabled = false;
                //miniMapImage.SetActive(false);
                miniMapOff.SetActive(true);
            } else {
                playerMapMarker.SetActive(true);
                miniMapCamera.enabled = true;
                //miniMapImage.SetActive(true);
                miniMapOff.SetActive(false);
            }
        }

        // transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);

    }
}
