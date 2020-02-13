using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public Dialogue dialogue;

    public void TriggerDialogue() {
        StartDialogue(dialogue);
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    //Aloittaa keskustelun kyseisen npc:n kanssa
    public void StartDialogue(Dialogue dialogue) {
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
