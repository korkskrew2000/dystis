using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMarker : MonoBehaviour {
    
    public void enableMarker() {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void disableMarker() {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Awake() {
        transform.Find("MarkerPositioner").GetComponent<MeshRenderer>().enabled = false;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
