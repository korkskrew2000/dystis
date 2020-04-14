using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    public Camera miniMapCamera;
    public Transform player;
    public float mapUpdateWaitTime = 0.05f;
    public bool rotateMapCamera = false;
    float timer = 0.0f;

    void Start() {

    }

    void Update() {

        // https://docs.unity3d.com/ScriptReference/Time-deltaTime.html

        timer += Time.deltaTime;

        if (timer > mapUpdateWaitTime) {
            if (rotateMapCamera) {
                float mapCameraY = player.transform.eulerAngles.y;
                transform.eulerAngles = new Vector3(0, mapCameraY, 0);
            }
            miniMapCamera.transform.position = new Vector3(player.transform.position.x,miniMapCamera.transform.position.y, player.transform.position.z);
            timer -= mapUpdateWaitTime;
            //Debug.Log("Tick...");
        }



    }
}
