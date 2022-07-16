using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Paso1 : MonoBehaviour
{
    float tiempo = 0;
    float tiempoa = 0;
    public GameObject Flecha;

    // Start is called before the first frame update
    void Start()
    {
        tiempoa = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo = Time.time - tiempoa;
        if (tiempo >= 0)
        {
            if (tiempo <= 2)
            {
                Flecha.SetActive(true);
            }
            else if (tiempo <= 7) {
                Flecha.SetActive(false);
            }
        }
    }
}
