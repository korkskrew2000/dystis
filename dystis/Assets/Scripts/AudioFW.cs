using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFW : MonoBehaviour {
    // how to use:
    // put sound effects in their own objects under SFX
    // then anywhere in the code, call 'AudioFW.Play(id)'
    // where id is the name of the sound effect object.

    Dictionary<string, AudioSource> sfx = new Dictionary<string, AudioSource>();
    Dictionary<string, AudioSource> loops = new Dictionary<string, AudioSource>();
    public static void Play(string id) {
        instance.PlayImpl(id);
    }
    public static void PlayLoop(string id) {
        instance.PlayLoopImpl(id);
    }
    public static void StopLoop(string id) {
        instance.StopLoopImpl(id);
    }

    public static void AdjustPitch(string id, float pitch) {
        instance.AdjustPitchImpl(id, pitch);
    }
    void PlayImpl(string id) {
        if (!sfx.ContainsKey(id)) {
            Debug.LogError("No sound with ID " + id);
            return;
        }
        sfx[id].PlayOneShot(sfx[id].clip);
    }
    void PlayLoopImpl(string id) {
        if (!loops.ContainsKey(id)) {
            Debug.LogError("No sound with ID " + id);
            return;
        }
        if (!loops[id].isPlaying) {
            loops[id].Play();
        }
    }
    void StopLoopImpl(string id) {
        if (!loops.ContainsKey(id)) {
            Debug.LogError("No sound with ID " + id);
            return;
        }
        loops[id].Stop();
    }
    void AdjustPitchImpl(string id, float pitch) {
        if (!loops.ContainsKey(id)) {
            Debug.LogError("No sound with ID " + id);
            return;
        }
        loops[id].pitch = Mathf.Clamp(pitch, -3f, 3f);
        //print("Pitch adjusted");
    }
    static public AudioFW instance {
        get {
            if (!_instance) {
                var a = GameObject.FindObjectsOfType<AudioFW>();
                if (a.Length == 0)
                    Debug.LogError("No AudioFW in scene");
                else if (a.Length > 1)
                    Debug.LogError("Multiple AudioFW in scene");
                _instance = a[0];
            }
            return _instance;
        }
    }
    static AudioFW _instance;

    void FindAudioSources() {
        var audioSources = transform.Find("SFX").GetComponentsInChildren<AudioSource>();
        foreach (var a in audioSources) {
            sfx.Add(a.name, a);
        }
        var audioSources2 = transform.Find("Loops").GetComponentsInChildren<AudioSource>();
        foreach (var a in audioSources2) {
            loops.Add(a.name, a);
        }
    }

    void Awake() {
        FindAudioSources();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A))
            DebugPrint();
    }

    void DebugPrint() {
        string s = "Audio loaded: ";
        foreach (var id in sfx.Keys)
            s += id + " ";
        print(s);
    }
}
