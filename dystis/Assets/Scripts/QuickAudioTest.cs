using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickAudioTest : MonoBehaviour {
    Dictionary<KeyCode, string> bindings = new Dictionary<KeyCode, string>();

    void Start() {
        bindings.Add(KeyCode.C, "hyppy");
        bindings.Add(KeyCode.V, "gunshot");
        bindings.Add(KeyCode.X, "shotgun");

    }

    // Update is called once per frame
    void Update() {
        foreach (var kc in bindings.Keys) {
            if (Input.GetKeyDown(kc))
                AudioFW.Play(bindings[kc]);
        }
    }
}
