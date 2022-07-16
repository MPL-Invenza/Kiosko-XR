using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso13 : MonoBehaviour
{
    public GameObject palanca;
    public GameObject mano;
    float tiempo = 0;
    float tiempoa = 0;
    float x1 = -2;
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
            if (tiempo <= 5)
            {
                mano.SetActive(true);
                if (x1 > -55)
                {
                    x1 -= Time.deltaTime * 10f;
                    palanca.transform.localRotation = Quaternion.Euler(x1, 0, 0);
                }
            }
            else
            {
                mano.SetActive(false);
            }
        }
    }
}
