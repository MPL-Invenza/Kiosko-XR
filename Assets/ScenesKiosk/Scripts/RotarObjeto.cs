using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarObjeto : MonoBehaviour
{
    public float rotationalSpeed;
    private bool rotar = false;

    [SerializeField]
    private Axis rotationAxis;
    [SerializeField]
    private Axis controlRotationAxis;
    [SerializeField]
    private Axis comparacionAxis;

    private Vector3 rotationAxisV;
    private float controlRotationAxisF;

    [SerializeField]
    private bool limitar = true;

    [SerializeField]
    private float limiteRotacionInferior, limiteRotacionSuperior;
    [SerializeField]
    private bool destruir;
    private bool ejeAsignado;


    private void Update()
    {
        if (!ejeAsignado)
        {
            AsignarEjeRotacion();
            ejeAsignado = true;
        }
    }

    private void AsignarEjeRotacion()
    {
        switch (rotationAxis)
        {
            case Axis.X:
                    rotationAxisV = Vector3.right;
                break;
            case Axis.Y:
                    rotationAxisV = Vector3.up;
                break;
            case Axis.Z:
                    rotationAxisV = Vector3.forward;
                break;
            case Axis.mX:
                    rotationAxisV = -Vector3.right;
                break;
            case Axis.mY:
                    rotationAxisV = -Vector3.up;
                break;
            case Axis.mZ:
                    rotationAxisV = -Vector3.forward;
                break;
        }
    }

    public void GiroControl(float angX, float angY, float angZ)
    {
        switch (controlRotationAxis)
        {
            case Axis.X:
                controlRotationAxisF = angX * rotationalSpeed;
                break;
            case Axis.Y:
                controlRotationAxisF = angY * rotationalSpeed;
                break;
            case Axis.Z:
                controlRotationAxisF = angZ * rotationalSpeed;
                break;
            case Axis.mX:
                controlRotationAxisF = -angX * rotationalSpeed;
                break;
            case Axis.mY:
                controlRotationAxisF = -angY * rotationalSpeed;
                break;
            case Axis.mZ:
                controlRotationAxisF = -angZ * rotationalSpeed;
                break;
        }


        switch (comparacionAxis)
        {
            case Axis.X:
                {
                    if (limitar)
                    {
                        if ((transform.localEulerAngles.x + controlRotationAxisF) >= limiteRotacionInferior
                            && (transform.localEulerAngles.x + controlRotationAxisF) <= limiteRotacionSuperior)
                            rotar = true;
                        else
                            rotar = false;

                        if (destruir && (transform.localEulerAngles.x + controlRotationAxisF) >= limiteRotacionSuperior)
                            Destroy(transform.parent.gameObject);
                    }
                    else
                        rotar = true;

                }
                break;
            case Axis.Y:
                {
                    if (limitar)
                    {
                        if ((transform.localEulerAngles.y + controlRotationAxisF) >= limiteRotacionInferior
                            && (transform.localEulerAngles.y + controlRotationAxisF) <= limiteRotacionSuperior)
                            rotar = true;
                        else
                            rotar = false;

                        if (destruir && (transform.localEulerAngles.y + controlRotationAxisF) >= limiteRotacionSuperior)
                            Destroy(transform.parent.gameObject);
                    }
                    else
                        rotar = true;

                }
                break;
            case Axis.Z:
                {
                    if (limitar)
                    {
                        if ((transform.localEulerAngles.z + controlRotationAxisF) >= limiteRotacionInferior
                            && (transform.localEulerAngles.z + controlRotationAxisF) <= limiteRotacionSuperior)
                            rotar = true;
                        else
                            rotar = false;

                        if (destruir && (transform.localEulerAngles.z + controlRotationAxisF) >= limiteRotacionSuperior)
                            Destroy(transform.parent.gameObject);
                    }
                    else
                        rotar = true;

                }
                break;
        }
        if (rotar)
            transform.Rotate(rotationAxisV, controlRotationAxisF);
    }


}
