using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Yksinkertainen dialogimanageri NPC:lle. NPC:lle voi kirjoittaa ennalta "vuorosanat",
//jotka se käy läpi keskustelun aikana. Keskustelu aloitetaan painamalla E-kirjainta
//NPC:n lähellä. Keskustelussa edetään hiiren vasenta näppäintä klikkaamalla. NPC
//havaitsee vain Player-tagilla merkityn pelaajan.


public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public GameObject pressKeyText;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public DialogueOwn dialogue;
    private bool triggering;
    private bool conversating;


    // Start is called before the first frame update
    void Start() {
        sentences = new Queue<string>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            pressKeyText.SetActive(true);
            triggering = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            pressKeyText.SetActive(false);
            triggering = false;
            EndDialogue();
        }
    }

    // Update is called once per frame
    void Update() {
        if (triggering) {
            if (Input.GetKeyDown(KeyCode.E)) {
                triggering = false;
                pressKeyText.SetActive(false);
                conversating = true;
                StartDialogue(dialogue);
            }
        }
        if (conversating) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                DisplayNextSentence();
            }
        }
    }

    //Aloittaa keskustelun
    public void StartDialogue(DialogueOwn dialogue) {
        animator.SetBool("isOpen", true);
        sentences.Clear();
        nameText.text = dialogue.name;

        foreach(string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    //Näyttää jonossa seuraavana olevan keskustelunpätkän
    public void DisplayNextSentence() {
        if(sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    //Päättää keskustelun
    public void EndDialogue() {
        animator.SetBool("isOpen", false);
    }
}
