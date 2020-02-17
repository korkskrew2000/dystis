using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, ITalkable {

    public RPGTalk rpgTalk;

    public float npcHealth = 100f;

    public void TalkWith() {
        print(" :: Blah Blah :: ");
        rpgTalk.NewTalk("1","4");
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

    void OnMouseDown() {
        ClickIt();
    }

    void isNPCDestroyed(float health) {
        if (health <= 0) {
            Destroy(gameObject, 0.5f);
        }
    }
}
