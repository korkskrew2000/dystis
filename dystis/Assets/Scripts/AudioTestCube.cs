using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AudioTestCube : MonoBehaviour
{
    public UnityEvent myEventWhenPlayerCollides;

    public void OnCollisionEnter(Collision collision) {
        Debug.Log("Collision Detected.");
        TestAudio();
        //Some additional event here... like making NPC aggressive...
        myEventWhenPlayerCollides.Invoke();
    }

    void TestAudio() {
        AudioFW.Play("subwaytrip");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
