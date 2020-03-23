using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCGender { female, male }

public class NPCControllerV2 : MonoBehaviour, ITalkable {

    [Header("Player gender")]
    public NPCGender npcGender;
    [Header("NPC Health:")]
    [Range(0f, 100f), Tooltip("Health")]
    public float npcHealth = 100f;
    [Header("NPC Follow parameters:")]
    [Tooltip("Does NPC follow the player.")]
    public bool npcFollows = false;
    [Tooltip("How fast NPC follows the player.")]
    public float npcFollowSpeed = 8f;
    [Tooltip("What is the closest distance between NPC and player.")]
    public float npcFollowDistance = 4f;
    [Header("NPC look/turn parameters:")]
    public bool npcLooksAtPlayer = true;
    [Tooltip("How fast NPC turns towards player.")]
    public float npcTurningSpeed = 1f;
    [Tooltip("When NPC sees the player.")]
    public float npcLookDistance = 5f;
    [Tooltip("NPC turns back to it's original direction after discussion")]
    public bool npcLookReset = true;

    Quaternion npcOriginalRot;

    GameObject player;

    //void Reset() {
    //    var npcGender = NPCGender.male;
    //}

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        npcOriginalRot = transform.rotation;
    }

    void Update() {

        if (player == null) {
            print("No Player Found!");
            return;
        }

        // NPC follows player if npcFollow is true.
        if (npcFollows) {
            Vector3 distance = player.transform.position - transform.position;
            Vector3 direction = distance.normalized;
            Vector3 velocity = direction * npcFollowSpeed;
            velocity.y = 0;

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            float distanceToPlayer = distance.magnitude;
            if (distanceToPlayer > npcFollowDistance) {
                transform.Translate(velocity * Time.deltaTime);
            }
        }

        // NPC looks at player if ncpLooksAtPlayer is true and distance is short enough.
        if (npcLooksAtPlayer) {
            Vector3 distance = player.transform.position - transform.position;
            float distanceToPlayer = distance.magnitude;

            if (distanceToPlayer < npcLookDistance) {
                //transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
                var dir = (player.transform.position - transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0f, dir.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * npcTurningSpeed);
            } else {
                transform.rotation = Quaternion.Slerp(transform.rotation, npcOriginalRot, Time.deltaTime * npcTurningSpeed);
            }
        }
    }

    public void TalkWith() {
        print(" :: Blah Blah :: ");
    }

    public void DamageIt(float damageAmount) {
        if (npcGender == NPCGender.female) {
            AudioFW.Play("npctakeshitfemale");
        } else {
            AudioFW.Play("npctakeshitmale");
        }
        //Debug.Log("You damaged it.");
        npcHealth -= damageAmount;
        isNPCDestroyed(npcHealth);
    }

    //void OnMouseDown() {
    //    ClickIt();
    //}

    void isNPCDestroyed(float health) {
        if (health <= 0) {
            if (npcGender == NPCGender.female) {
                AudioFW.Play("npcdiesfemale");
            } else {
                AudioFW.Play("npcdiesmale");
            }
            Destroy(gameObject, 1.0f);
        }
    }
}