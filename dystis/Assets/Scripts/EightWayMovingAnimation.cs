using UnityEngine;
using System.Collections;

public class EightWayMovingAnimation : MonoBehaviour {
    bool wasMoving;
    int lastAnimSector;

    Animator animator;
    Transform sprite;
    Vector3 lastPosition;

    int[] mapSectorToFlipped = new int[] { 0, 1, 2, 3, 4, 3, 2, 1 };
    string[] animationNames =
        new string[] {"StoppedUp", "WalkUp", "StoppedUpRight", "WalkUpRight", "StoppedRight", "WalkRight",
                        "StoppedDownRight", "WalkDownRight", "StoppedDown", "WalkDown"};

    void SetSpriteFlip(bool flipped) {
        var scale = sprite.localScale;
        scale.x = (flipped ? -1 : 1) * Mathf.Abs(scale.x);
        sprite.localScale = scale;
    }

    void Awake() {
        animator = GetComponent<Animator>();
        sprite = transform.Find("Sprite");
    }

    void FixedUpdate() {
        var v = transform.forward;
        float shortestAngle = Vector3.Angle(Vector3.forward, v);
        float clockwiseAngle = v.x >= 0 ? shortestAngle : 360 - shortestAngle;
        int sector = ((int)(clockwiseAngle + 22.5f) % 360) / 45;
        bool nowMoving = Vector3.Distance(transform.position, lastPosition) > Mathf.Epsilon;

        int animSector = mapSectorToFlipped[sector];
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