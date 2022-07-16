using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRotationInicial : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float z = 0;

    public float xr = 0;
    public float yr = 0;
    public float zr = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(xr, yr, zr);
        transform.localPosition = new Vector3(x, y, z);
    }
}
