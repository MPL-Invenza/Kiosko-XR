using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Movimiento
{
    Desplazar,
    Rotar
}

public class ArrastrarObjeto : MonoBehaviour
{

    public float movementSpeed;
    [HideInInspector]
    public bool mover = false;

    [SerializeField]
    private Movimiento movimiento;
    [SerializeField]
    private Axis movementAxis;
    [SerializeField]
    private Axis comparacionAxis;
    [SerializeField]
    private Axis controlMovementAxis;

    [SerializeField]
    private bool limitar = true;
    [SerializeField]
    private float limiteMovimientoInferior, limiteMovimientoSuperior;
    private Vector3 movementAxisV;
    [HideInInspector]
    public float controlMovementAxisF;
    public bool movimientoControl=false;
    private bool ejeAsignado;
    public bool local=true;
    private Transform movingObject;
    private float valorComparacion;
    public float valorEje;

    private void Start()
    {
        if (local)
            movingObject = transform;
        else movingObject = transform.parent;
        switch (comparacionAxis)
            {
                case Axis.X:
                        valorEje = movingObject.localEulerAngles.x;
                    break;
                case Axis.Y:
                        valorEje = movingObject.localEulerAngles.y;
                    break;
                case Axis.Z:
                        valorEje = movingObject.localEulerAngles.z;
                    break;
            }
    }

    private void Update()
    {
        if (!ejeAsignado)
        {
            AsignarEjeMovimiento();
            ejeAsignado = true;
        }
        if (!mover)
        {
            controlMovementAxisF = 0;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            print($"{gameObject.name}: {transform.localEulerAngles}");
        }

        switch (comparacionAxis)
            {
                case Axis.X:
                        valorEje = movingObject.localEulerAngles.x;
                    break;
                case Axis.Y:
                        valorEje = movingObject.localEulerAngles.y;
                    break;
                case Axis.Z:
                        valorEje = movingObject.localEulerAngles.z;
                    break;
            }
    }

    private void OnDisable() {
        mover = false;
    }

    private void AsignarEjeMovimiento()
    {
        switch (movementAxis)
            {
                case Axis.X:
                        movementAxisV = Vector3.right;
                    break;
                case Axis.Y:
                        movementAxisV = Vector3.up;
                    break;
                case Axis.Z:
                        movementAxisV = Vector3.forward;
                    break;
                case Axis.mX:
                        movementAxisV = -Vector3.right;
                    break;
                case Axis.mY:
                        movementAxisV = -Vector3.up;
                    break;
                case Axis.mZ:
                        movementAxisV = -Vector3.forward;
                    break;
            }
    }

    public void MovimientoControl(float disX, float disY, float disZ)
    {
        //Eje de movimiento del control
        switch (controlMovementAxis)
        {
            case Axis.X:
                controlMovementAxisF = disX * movementSpeed;
                break;
            case Axis.Y:
                controlMovementAxisF = disY * movementSpeed;
                break;
            case Axis.Z:
                controlMovementAxisF = disZ * movementSpeed;
                break;
            case Axis.mX:
                controlMovementAxisF = -disX * movementSpeed;
                break;
            case Axis.mY:
                controlMovementAxisF = -disY * movementSpeed;
                break;
            case Axis.mZ:
                controlMovementAxisF = -disZ * movementSpeed;
                break;
        }

        if (movimiento==Movimiento.Desplazar)
        {
            switch (comparacionAxis)
            {
                case Axis.X:
                        valorComparacion = movingObject.localPosition.x + controlMovementAxisF;
                    break;
                case Axis.Y:
                        valorComparacion = movingObject.localPosition.y + controlMovementAxisF;
                    break;
                case Axis.Z:
                        valorComparacion = movingObject.localPosition.z + controlMovementAxisF;
                    break;
            }

            if (limitar)
            {
                if (valorComparacion >= limiteMovimientoInferior && valorComparacion <= limiteMovimientoSuperior)
                    mover = true;
                else
                    mover = false;
            }
            else
                mover = true;

            if (mover)
                movingObject.Translate(controlMovementAxisF * movementAxisV, Space.Self);
        }
        else
        {

            switch (comparacionAxis)
            {
                case Axis.X:
                        valorComparacion = movingObject.localEulerAngles.x + controlMovementAxisF;
                    break;
                case Axis.Y:
                        valorComparacion = movingObject.localEulerAngles.y + controlMovementAxisF;
                    break;
                case Axis.Z:
                        valorComparacion = movingObject.localEulerAngles.z + controlMovementAxisF;
                    break;
            }
            if (valorComparacion<0)
                valorComparacion = 360.0f + valorComparacion;

            if (limitar)
            {
                if (valorComparacion >= limiteMovimientoInferior && valorComparacion <= limiteMovimientoSuperior)
                    mover = true;
                else
                    mover = false;
            }
            else
                mover = true;

            if (mover)
                movingObject.Rotate(movementAxisV, controlMovementAxisF);

        }
    }

}
