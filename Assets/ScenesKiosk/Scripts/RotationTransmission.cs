using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTransmission : MonoBehaviour
{
    public XRHandleObject objectIn;
    public XRHandleObject objectOut;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (objectIn.move)
        {
            objectOut.Handle(objectIn.movementValue);
        }
    }
}
