using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZAxis : MonoBehaviour
{
    public static float zAxisValue;
    static Vector3 pos;

    public static void setZValue(float f)
    {
        zAxisValue = (f-0.5f)*10;
    }

    void Update()
    {
        pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, zAxisValue-1);
    }
}
