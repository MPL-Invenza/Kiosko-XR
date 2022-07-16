using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInicial : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float z = 180;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(x, y, z);
    }
}
