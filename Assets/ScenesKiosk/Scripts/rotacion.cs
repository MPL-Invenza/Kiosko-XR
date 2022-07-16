using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion : MonoBehaviour
{
    public int x1 = 0;
    public int y1 = 2;
    public int z1 =-90;


    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(x1, y1, z1);
    }
}
