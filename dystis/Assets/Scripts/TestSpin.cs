using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpin : MonoBehaviour
{
    public float spinSpeed = 1f;

    void Update()
    {
        transform.Rotate (spinSpeed,0,0 * Time.deltaTime);
    }
}
