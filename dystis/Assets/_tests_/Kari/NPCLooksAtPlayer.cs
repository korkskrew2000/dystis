using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLooksAtPlayer : MonoBehaviour {
    
    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {
        if (player != null) {
            //transform.LookAt(player.transform.position);
            //we want to rotate only around Z-axis
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        } else {
            print("No Player Found!");
        }
    }
}
