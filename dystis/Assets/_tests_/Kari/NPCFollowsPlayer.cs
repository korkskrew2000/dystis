using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowsPlayer : MonoBehaviour, ITalkable {
    public float npcSpeed = 8f;
    public float npcDistance = 4f;

    GameObject player;
   
    public void TalkWith() {

    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        if (player != null) {
            Vector3 distance = player.transform.position - transform.position;
            Vector3 direction = distance.normalized;
            Vector3 velocity = direction * npcSpeed;
            velocity.y = 0;

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            float distanceToPlayer = distance.magnitude;
            if (distanceToPlayer > npcDistance) {
                transform.Translate(velocity * Time.deltaTime);
            }
        } else {
            print("No Player Found!");
        }
    }
}
