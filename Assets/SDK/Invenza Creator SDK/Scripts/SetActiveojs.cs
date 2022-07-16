using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveojs : MonoBehaviour
{
    public GameObject[] objs;

    public void Hideobjs(bool everything)
    {
        for (byte i = 0; i < objs.Length; i++)
        {
            if (everything)
                objs[i].SetActive(false);
            else
            {
                if (i < objs.Length - 1)
                    objs[i].SetActive(false);
                else
                    objs[i].SetActive(true);
            }
        }
    }



}
