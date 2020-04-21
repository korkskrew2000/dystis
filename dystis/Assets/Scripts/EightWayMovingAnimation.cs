using UnityEngine;
using System.Collections;

public class EightWayMovingAnimation : MonoBehaviour {
    bool wasMoving;
    int lastAnimSector;

    Animator animator;
    public Transform sprite;

    Vector3 lastPosition;

    GameObject player;

    GameObject tempGO;

    float npcHealth;

    public float shortestAngle;
    public float clockwiseAngle;
    public int sector;
    public bool nowMoving;
    public int animSector;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    int[] mapSectorToFlipped = new int[] { 0, 1, 2, 3, 4, 3, 2, 1 };

    string[] animationNames =
        new string[] {"StoppedUp", "WalkUp", "StoppedUpRight", "WalkUpRight", "StoppedRight", "WalkRight",
                        "StoppedDownRight", "WalkDownRight", "StoppedDown", "WalkDown"};

    void SetSpriteFlip(bool flipped) {
        //Debug.Log("SetSpriteFlip: " + flipped);
        var scale = sprite.localScale;
        //scale.x = (flipped ? -1 : 1) * Mathf.Abs(scale.x);
        scale.x = (flipped ? 1 : -1) * Mathf.Abs(scale.x);
        sprite.localScale = scale;
    }

    void Awake() {
        //alkup.
        //animator = GetComponent<Animator>();
        //sprite = transform.Find("Sprite");

        sprite = transform.Find("Sprite");
        animator = sprite.GetComponent<Animator>();

        //if (sprite != null) {
        //    Debug.Log("Sprite found!" + sprite.name);
        //}
        tempGO = new GameObject();
    }

    void FixedUpdate() {

        npcHealth = GetComponent<NPCControllerV2>().npcHealth;
        if (npcHealth < 0) return;

        //alkup.
        //var v = transform.forward;
        //float shortestAngle = Vector3.Angle(Vector3.forward, v);
        //float clockwiseAngle = v.x >= 0 ? shortestAngle : 360 - shortestAngle;
        //int sector = ((int)(clockwiseAngle + 22.5f) % 360) / 45;
        //bool nowMoving = Vector3.Distance(transform.position, lastPosition) > Mathf.Epsilon;

        // Transform.InverseTransformDirection
        // https://docs.unity3d.com/ScriptReference/Transform.InverseTransformDirection.html

        var t = tempGO.transform;
        t.position = player.transform.position;
        t.LookAt(transform.position);

        var v = t.InverseTransformDirection(transform.forward);
        shortestAngle = Vector3.Angle(Vector3.forward, v);
        clockwiseAngle = v.x >= 0 ? shortestAngle : 360 - shortestAngle;
        //Debug.Log("v.x : " + v.x);
        sector = ((int)(clockwiseAngle + 22.5f) % 360) / 45;
        nowMoving = Vector3.Distance(transform.position, lastPosition) > Mathf.Epsilon;


        animSector = mapSectorToFlipped[sector];
        SetSpriteFlip(sector != animSector);
                
        if (lastAnimSector != animSector) {
            animator.Play(animationNames[animSector * 2 + (nowMoving ? 1 : 0)]);
        } else if (wasMoving && !nowMoving) {
            animator.Play(animationNames[animSector * 2]);
        } else if (!wasMoving && nowMoving) {
            animator.Play(animationNames[animSector * 2 + 1]);
        }

        lastAnimSector = animSector;
        lastPosition = transform.position;
        wasMoving = nowMoving;
    }
}
