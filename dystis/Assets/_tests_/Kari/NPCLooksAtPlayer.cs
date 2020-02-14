using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCLooksAtPlayer : MonoBehaviour {
    public Transform target;

    // Update is called once per frame
    void Update() {
        if (target != null) {
            transform.LookAt(target);
        }
    }
}
