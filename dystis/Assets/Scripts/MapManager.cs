using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public Camera miniMapCamera;
    public Transform player;
    public float mapUpdateWaitTime = 0.05f;
    public bool rotateMiniMapCamera = false;
    float timer = 0.0f;

    public int worldWidth = 1000;   // x
    public int worldLength = 1000;  // z

    void Start() {

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
        }

        // transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);

    }
}
