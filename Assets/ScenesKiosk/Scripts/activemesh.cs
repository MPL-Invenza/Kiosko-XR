using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activemesh : MonoBehaviour
{
    public GameObject obj;
    bool flag=false;

    // Update is called once per frame
    public void activacion()
    {
        flag ^= true;
        obj.SetActive(flag);
    }
}
