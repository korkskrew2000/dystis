using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int experience = 0;
    public float money = 0.95f;

    // Very basic item...
    public void QuestTransferItem() {
        experience += 1;
    }

    public void QuestDeliverMysteriousLetter() {
        experience += 10;
        money += 50;
    }

    public void QuestDeliverPizza() {
        experience += 5;
        money += 5;
    }

    public void QuestKillNPCHuman() {
        experience += 10;
        health -= 10;
    }

    public void QuestKillNPCSnake() {
        experience += 1;
        health -= 1;
    }

}
