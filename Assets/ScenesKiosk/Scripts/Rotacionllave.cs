using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacionllave : MonoBehaviour
{
    public float x1 = 65;
    public float y1 = 0;
    public float z1 = 0;
    public float x2 = 133;
    public float y2 = 0;
    public float z2 = 0;

    // Update is called once per frame
    void Update()
    {
        if (x1 < x2) { 
            x1 += Time.deltaTime * 3;
            transform.localRotation = Quaternion.Euler(x1, y1, z1);
        }
    }
}
