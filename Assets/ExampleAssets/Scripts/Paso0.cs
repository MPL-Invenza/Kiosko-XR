using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Paso0 : MonoBehaviour
{
    
    public UnityEvent upperLimitReached;
    public float Limit;
    public bool Mayor;
    public bool posicion;
    public bool ejeX;
    public bool ejeY;
    public bool ejeZ;
    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        //print(transform.localRotation.eulerAngles.x);
        if (posicion)
        {
            if (ejeX)
            {
                if (Mayor)
                {
                    if (transform.localPosition.x < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localPosition.x > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
            else if (ejeY)
            {
                if (Mayor)
                {
                    if (transform.localPosition.y < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localPosition.y > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
            else if (ejeZ)
            {
                if (Mayor)
                {
                    if (transform.localPosition.z < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localPosition.z > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
        }
        else {
            if (ejeX)
            {
                if (Mayor)
                {
                    if (transform.localRotation.eulerAngles.x < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localRotation.eulerAngles.x > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
            else if (ejeY)
            {
                if (Mayor)
                {
                    if (transform.localRotation.eulerAngles.y < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localRotation.eulerAngles.y > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
            else if (ejeZ)
            {
                if (Mayor)
                {
                    if (transform.localRotation.eulerAngles.z < Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
                else
                {
                    if (transform.localRotation.eulerAngles.z > Limit)
                    {
                        upperLimitReached.Invoke();
                    }
                }
            }
        }
    }
}
