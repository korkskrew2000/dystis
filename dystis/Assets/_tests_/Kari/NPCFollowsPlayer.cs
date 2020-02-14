using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowsPlayer : MonoBehaviour
{
    public float npcSpeed = 8f;
    public float npcDistanceFromPlayer = 4f;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update() {

        Vector3 distance = player.transform.position - transform.position;
        Vector3 direction = distance.normalized;
        Vector3 velocity = direction * npcSpeed;

        float distanceToTarget = distance.magnitude;
        if (distanceToTarget > npcDistanceFromPlayer) {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}
