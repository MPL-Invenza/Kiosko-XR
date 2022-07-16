using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paso3 : MonoBehaviour
{
    public GameObject mano1;
    public GameObject mano2;
    public GameObject palanca1;
    public GameObject palanca2;
    float tiempo = 0;
    float tiempoa = 0;
    float x1 = 214;
    float x2 = 214;
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
            if (tiempo <= 6)
            {
                mano1.SetActive(true);
                if (x1 > 146)
                {
                    x1 -= Time.deltaTime * 30f;
                    palanca1.transform.localRotation = Quaternion.Euler(0, 0, x1);
                }
            }
            else if (tiempo <= 10)
            {
                mano1.SetActive(false);
                mano2.SetActive(true);
                if (x2 > 146)
                {
                    x2 -= Time.deltaTime * 30f;
                    palanca2.transform.localRotation = Quaternion.Euler(0, 0, x2);
                }
            }
                else
            {
                mano2.SetActive(false);
            }
        }
    }
}
