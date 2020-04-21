using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCGender { female, male }
public enum NPCMood { aggressive, peaceful };

public class NPCControllerV2 : MonoBehaviour, ITalkable {

    [Header("Player gender")]
    public NPCGender npcGender;
    [Header("NPC mood/behaviour")]
    public NPCMood npcMood;
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
    bool npcLooksAtPlayer = true;
    [Tooltip("How fast NPC turns towards player.")]
    float npcTurningSpeed = 10f;
    [Tooltip("When NPC sees the player.")]
    float npcLookDistance = 100f;
    [Tooltip("NPC turns back to it's original direction after discussion")]
    public bool npcLookReset = true;
    [Range(1f, 100f), Tooltip("Damage Caused By NPC")]
    public int npcDamagePower = 1;
    public bool npcReadyToShoot = true;
    public float npcShootingDelayTime = 3f;
    public float npcShootingTimer = 0;
    public float npcShootingDistance = 20f;
    public float npcDistanceToPlayer = 0f;

    Ray npcShootingRay;
    RaycastHit npcShootingHit;
    //public LayerMask sightBlockingStructure;

    Quaternion npcOriginalRot;

    GameObject player;
    Animator animator;
    Transform sprite;

    //void Reset() {
    //    var npcGender = NPCGender.male;
    //}

    public void SetNPCMoodAggressive() {
        npcMood = NPCMood.aggressive;
    }

    public void SetNPCMoodPeaceful() {
        npcMood = NPCMood.peaceful;
    }

    void Awake() {
        sprite = transform.Find("Sprite");
        animator = sprite.GetComponent<Animator>();
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        npcOriginalRot = transform.rotation;
    }

    void Update() {

        if (player == null) {
            print("No Player Found!");
            return;
        }

        //NPC Shoots at player

        if (npcHealth > 0 && npcMood == NPCMood.aggressive) {

            npcDistanceToPlayer = (player.transform.position - transform.position).magnitude;

            if(!npcReadyToShoot)
                npcShootingTimer += Time.deltaTime;

            if (!npcReadyToShoot && npcShootingTimer > npcShootingDelayTime) {
                npcShootingTimer = 0f;
                npcReadyToShoot = true;
                Debug.Log("NPC reloading done.");
                AudioFW.Play("shotgunequip");
            }

            var playerHeading = (player.transform.position - transform.position).normalized;
            // Some randomness for NPC shooting.
            Vector3 dir = Random.insideUnitCircle * 1f;
            dir.z = npcDistanceToPlayer; 
            dir = transform.TransformDirection(dir.normalized);
            playerHeading += dir;
            // PS. NPC Eyes are at 1.8 meters (Vector3.up * 1.8f).
            npcShootingRay = new Ray(transform.position + Vector3.up * 1.8f, playerHeading);

            //If NPC actually sees the player...
            if (Physics.Raycast(npcShootingRay, out npcShootingHit, npcShootingDistance)) {
                Debug.DrawLine(npcShootingRay.origin, npcShootingHit.point, Color.green, 0.1f);
                if (npcReadyToShoot && npcShootingHit.transform.name == "Player") {
                    Debug.DrawLine(npcShootingRay.origin, npcShootingHit.point, Color.red, 2f);
                    Debug.Log("Raycast from NPC to Player got a hit!");
                    player.GetComponent<PlayerController>().health -= npcDamagePower;
                    animator.Play("FireGun");
                    AudioFW.Play("shotgunshot");
                    npcReadyToShoot = false;
                    npcShootingTimer = 0f;
                }
            }
        }

        // NPC follows player if npcFollow is true.
        if (npcHealth > 0 && npcFollows) {
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
        //print(" :: Blah Blah :: ");
    }

    public void DamageIt(float damageAmount) {
        if (npcGender == NPCGender.female) {
            AudioFW.Play("npctakeshitfemale");
        } else {
            AudioFW.Play("npctakeshitmale");
        }
        //Debug.Log("You damaged it.");
        npcHealth -= damageAmount;
        IsNPCDestroyed(npcHealth);
    }

    void IsNPCDestroyed(float health) {
        if (health <= 0) {
            if (npcGender == NPCGender.female) {
                AudioFW.Play("npcdiesfemale");
            } else {
                AudioFW.Play("npcdiesmale");
            }
            // Dead NPC sprite change here...
            //Destroy(gameObject, 1.0f);
            animator.Play("Dead");
            
        }
    }
}