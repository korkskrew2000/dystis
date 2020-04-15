using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceEvent : MonoBehaviour
{
    //RPG-talk holder johon liittyvän puheen valintoihin halutaan
    //saada eventtejä
    public RPGTalk rpgTalk;

    //questin hyväksyvän valinnan indeksi "käsikirjoituksessa"
    //esim. vaihtoehdoilla KYLLÄ / EI ensimmäinen vaihtoehto
    //(eli KYLLÄ) vastaa indeksiä 0 (joka on myös tämän
    //muuttujan oletusarvo)
    public int questAcceptingChoice;

    //Kyseisen npc:n käsikirjoituksessa esiintyvä quest ID
    //eli questin aloittavan kysymyksen otsikko.
    //Tätä tarvitaan OnMadeChoice funktiossa identifikoimaan
    //tietty kysymys oikeaan questiin,
    public string questKeyword;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (rpgTalk != null)
        {
            rpgTalk.OnMadeChoice += OnMadeChoice;
        }
    }

    //RPG-talk valintoihin liittyvää asiaa:
    //quest-kysymykseen myöntävästi vastaaminen käynnistää
    //questin. ChoiceID on kysymykseen vastaamis -näppäimen
    //indeksi (0 on ensimmäinen vastausvaihtoehto, 1 toinen,
    //jne.)
    public virtual void OnMadeChoice(string questionID, int choiceID)
    {
        if (questionID == questKeyword && choiceID == questAcceptingChoice)
        {
            Debug.Log("Doing something because player answered " + choiceID +
            " on question " + questionID);
        }
    }
}
