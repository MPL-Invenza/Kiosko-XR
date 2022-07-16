using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso2 : MonoBehaviour
{
    public GameObject flecha1;
    public GameObject flecha2;
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
                flecha1.SetActive(true);
            }
            else if (tiempo <= 7)
            {
                flecha1.SetActive(false);
                flecha2.SetActive(true);
            }
            else
            {
                flecha2.SetActive(false);
            }
        }
    }
}
