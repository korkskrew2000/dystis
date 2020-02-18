using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, ITalkable {

    //public RPGTalk rpgTalk;
    public float npcHealth = 100f;

    public GameObject player;

    public void DisablePlayerMovement() {
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = false;
        player.GetComponent<FirstPersonAIO>().playerCanMove = false;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = false;
        player.GetComponent<FirstPersonAIO>().autoCrosshair = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EnablePlayerMovement() {
        player.GetComponent<FirstPersonAIO>().playerCanMove = true;
        player.GetComponent<FirstPersonAIO>().enableCameraMovement = true;
        player.GetComponent<FirstPersonAIO>().autoCrosshair = true;
        //player.GetComponent<FirstPersonAIO>().lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TalkWith() {
        print(" :: Blah Blah :: ");
        //rpgTalk.NewTalk("1","4");
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
