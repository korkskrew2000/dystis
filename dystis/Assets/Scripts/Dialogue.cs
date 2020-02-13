using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Dialogi-luokka toimii tyhjänä pohjana eri npc-hahmojen dialogeille

[System.Serializable]
public class Dialogue{
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
