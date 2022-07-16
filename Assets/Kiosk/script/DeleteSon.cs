using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSon : MonoBehaviour
{
    public void Delete()
    {
        int a = 0;
        a=transform.childCount;
        //Debug.Log("aqui:"+a);

        //while (a > 0) {
        //Destroy(transform.GetChild(1));
        for (int i = a; i> 0; i--) {
            Destroy(transform.GetChild(i-1).gameObject);
        }
            //    a = transform.childCount;
        //}
        //a = transform.childCount;
        //Debug.Log("aqui2:" + a);
    }
}
