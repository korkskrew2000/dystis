using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleTest : MonoBehaviour {

    public Transform objA;
    public Transform objB;
    
    /*
    float angleX = myTransform.rotation.eulerAngles.x;
    float angleY = myTransform.rotation.eulerAngles.y;
    float angleZ = myTransform.rotation.eulerAngles.z;
    */

    public static float CalculateAngle180_v3(Vector3 fromDir, Vector3 toDir) {
        float angle = Quaternion.FromToRotation(fromDir, toDir).eulerAngles.y;
        if (angle > 180) { return angle - 360f; }
        return angle;
    }

    void Start() {
        float a = CalculateAngle180_v3(new Vector3(0,0,0), new Vector3(1,1,1));
        print("Angle : " + a);
    }

    void Update() {

    }
}
