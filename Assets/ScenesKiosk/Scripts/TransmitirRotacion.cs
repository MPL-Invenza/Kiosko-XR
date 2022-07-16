using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitirRotacion : MonoBehaviour
{
    [SerializeField]
    private Axis ejeRotacion;
    private Vector3 movementAxisV;
    private float valorRotacion;
    [SerializeField]
    private ArrastrarObjeto objetoPrincipal;
    private bool ejeAsignado;
    public float multiplicador = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!ejeAsignado)
        {
            AsignarEjeMovimiento();
            ejeAsignado = true;
        }
        //valorRotacion = objetoPrincipal.controlMovementAxisF;
        if (objetoPrincipal.mover)
        {
            transform.Rotate(movementAxisV, objetoPrincipal.controlMovementAxisF * multiplicador);
        }

        valorRotacion = 0;
    }

    private void AsignarEjeMovimiento()
    {
        switch (ejeRotacion)
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
}
