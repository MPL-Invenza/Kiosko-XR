using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso10 : MonoBehaviour
{
    public GameObject caja;
    public GameObject candado;
    public GameObject llave;
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
                candado.SetActive(false);
                caja.transform.localRotation = Quaternion.Euler(0, 90, 180);
            }
            else if (tiempo <= 7)
            {
            }
            else if (tiempo <= 10)
            {
                llave.SetActive(true);
            }
            else if (tiempo <= 13)
            {
                caja.transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                candado.SetActive(true);
            }
        }
    }
}
