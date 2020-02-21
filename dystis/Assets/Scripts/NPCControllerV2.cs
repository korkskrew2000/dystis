using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControllerV2 : MonoBehaviour, ITalkable {

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

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        npcOriginalRot = transform.rotation;
    }

    void Update() {

        if(player == null) {
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
            }
            else {
                transform.rotation = Quaternion.Slerp(transform.rotation, npcOriginalRot, Time.deltaTime * npcTurningSpeed);
            }
        }
    }

 




    public void TalkWith() {
        print(" :: Blah Blah :: ");
    }

    public void HitSomething(GameObject whoHit) {
        // NPC hits/bounces into something and takes minor damage.
        Debug.Log(gameObject.name + ": " + whoHit.name + " hit me!");
        //npcHealth -= 1f;
        if (whoHit.tag == "Player") {
            //audioCrateLandsOnSand.Play();
        } else {
            //audioCrateTakesHits.Play();

        }

        isNPCDestroyed(npcHealth);
    }

    public void ClickIt() {
        // Player clicks NPC and it takes tiny amount of damage.
        Debug.Log("Somebody clicked " + gameObject.name + ".");
        //npcHealth -= 0.1f;
        //isCrateDestroyed(crateHealth);
    }

    public void DamageIt(float damageAmount) {
        Debug.Log("You damaged it.");
        //npcHealth -= damageAmount;
        isNPCDestroyed(npcHealth);
    }

    void OnCollisionEnter(Collision collision) {
        HitSomething(collision.gameObject);
    }

    //void OnMouseDown() {
    //    ClickIt();
    //}

    void isNPCDestroyed(float health) {
        if (health <= 0) {
            Destroy(gameObject, 0.5f);
        }
    }
}
