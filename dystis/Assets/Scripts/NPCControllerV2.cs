using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControllerV2 : MonoBehaviour, ITalkable {

    public float npcHealth = 100f;
    public bool npcFollows = false;
    public float npcFollowSpeed = 8f;
    public float npcFollowDistance = 4f;
    public bool npcLooksAtPlayer = true;
    public float npcLookDistance = 3f;

    GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
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
                //transform.LookAt(player.transform.position);
                //we want to rotate only around Z-axis
                transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
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
