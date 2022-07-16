using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation3 : MonoBehaviour
{
    public int x1 = 0;
    public int y1 = 2;
    public int z1 = -90;
    public int x2 = 0;
    public int y2 = 2;
    public int z2 = -90;
    bool flag=false;

    private void Start()
    {
        
    }

    // Update is called once per frame
    public void rotar()
    {
        flag ^= true;
        if (flag)
        {
            transform.localRotation = Quaternion.Euler(x1, y1, z1);
        }
        else {
            transform.localRotation = Quaternion.Euler(x2, y2, z2);
        }
    }
}