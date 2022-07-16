using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion2 : MonoBehaviour
{
    public int x2 = 22;
    public int y2 = 2;
    public int z2 = -90;

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(x2, y2, z2);    
    }
}
