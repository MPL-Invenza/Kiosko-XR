using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso6 : MonoBehaviour
{
    public GameObject mano;
    public GameObject mano2;
    float tiempo = 0;
    float tiempoa = 0;
    // Start is called before the first frame update
    void Start()
    {
        tiempoa = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo = Time.time - tiempoa;
        if (tiempo >= 1)
        {
            if (tiempo <= 4)
            {
                mano.SetActive(true);
            }
            else if (tiempo <= 7)
            {
                mano.SetActive(false);
                mano2.SetActive(true);
            }
            else
            {
                mano2.SetActive(false);
            }
        }
    }
}
