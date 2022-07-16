using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransformVariable{
    Position, LocalPosition, Rotation, LocalRotation
}
public class MotionTransmission : MonoBehaviour
{
    public GameObject objectIn;
    public GameObject objectIn2;
    public GameObject objectIn3;
    public TransformVariable transformVariable = TransformVariable.LocalRotation;
    public XRHandleObject objectOut;
    public Vector2 inputRange;
    public Vector2 outputRange = new Vector2(-1,1);
    private Vector3 values;

    private void Start()
    {

        if (inputRange==Vector2.zero)
        {
            if (objectIn.GetComponent<XRHandleObject>())
            {
                inputRange = objectIn.GetComponent<XRHandleObject>().limits;
            }
        }
        this.enabled = false;

    }

    private void Update()
    {

        if (objectIn2.activeSelf == true)
        {
            objectIn = objectIn2;
        }
        if (objectIn3.activeSelf == true)
        {
            objectIn = objectIn3;
        }


        if (transformVariable==TransformVariable.Rotation)
        {
            switch (objectOut.sourceMovementAxis)
            {
                case Axis.X:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.eulerAngles.x, outputRange));
                    break;
                case Axis.Y:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.eulerAngles.y, outputRange));
                    break;
                case Axis.Z:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.eulerAngles.z, outputRange));
                    break;
            }
        }
        else if(transformVariable==TransformVariable.LocalRotation)
        {
            switch (objectOut.sourceMovementAxis)
            {
                case Axis.X:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.localEulerAngles.x, outputRange));
                    break;
                case Axis.Y:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.localEulerAngles.y, outputRange));
                    break;
                case Axis.Z:
                    objectOut.Handle(NormalizeValues.Normalize(inputRange, objectIn.transform.localEulerAngles.z, outputRange));
                    break;
            }
        }
    }
}
