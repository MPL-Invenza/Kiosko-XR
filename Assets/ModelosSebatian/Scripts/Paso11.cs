using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso11 : MonoBehaviour
{
    public GameObject flecha;
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
                flecha.SetActive(true);
            }
            else
            {
                flecha.SetActive(false);
            }
        }
    }
}
