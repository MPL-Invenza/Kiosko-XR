using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso12 : MonoBehaviour
{
    public GameObject llave;
    public GameObject mano;
    float tiempo = 0;
    float tiempoa = 0;
    float x1 = 35;
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
            if (tiempo <= 2)
            {
                mano.SetActive(true);
            }
            else if (tiempo <= 7)
            {
                if (x1 > 65) { 
                    x1 -= Time.deltaTime * 3f;
                    llave.transform.localRotation = Quaternion.Euler(x1, 0, 0);
                }
            }
            else
            {
                mano.SetActive(false);
            }
        }
    }
}
