using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRGrabObject : MonoBehaviour
{
    //     //
    //Blue arrow
    [SerializeField]
    [Tooltip("Ejes locales respecto al control")]
    private Axis localZAxis;

    private Vector3 localZAxisVector;

    [SerializeField]
    [Tooltip("Direccion flecha azul cuando se agarra")]
    private Axis forwardDirection = Axis.Y;
    [SerializeField]
    [Tooltip("Direccion flecha verde cuando se agarra")]
    private Axis upDirection = Axis.Z;

    private Vector3 forwardDirectionVector;
    private Vector3 upDirectionVector;

    [SerializeField]
    [Space]
    private float movementSpeed;

    [SerializeField]
    [Space]
    private Vector2 movementLimits;

    public bool useGravity = true;
    [HideInInspector]
    public Vector3 initialPosition;
    private Vector3 initialRotation;

    [HideInInspector]
    public bool blocked;

    [HideInInspector]
    public bool grabbing;

    private Rigidbody rg;
    private float movementValue = 0;
    public byte snapGroup;
    [HideInInspector]
    public bool onTrigger;

    public bool keepInteractable = false;
    //public bool detenerSimulacionCable = true;
    public bool forwardMovement = true;

    void Start()
    {
        //Almacenar transform inicial
        initialPosition = transform.position;
        initialRotation = transform.eulerAngles;
        rg = GetComponent<Rigidbody>();
#if !UNITY_EDITOR
        //movementSpeed = movementSpeed / 10;
#endif
    }

    private void Update()
    {
        if (grabbing)
        {
            rg.velocity = new Vector3(0, 0, 0);
        }
    }
    #region Agarre

    public void FirstGrab()
    {
        //Ajustar físicas para facilitar el transporte del objeto
        rg.useGravity = false;
        rg.freezeRotation = true;
        rg.isKinematic = false;
        grabbing = true;

        //Orientación Agarre
        switch (forwardDirection)
        {
            case Axis.X:
                forwardDirectionVector = transform.parent.right;
                break;
            case Axis.Y:
                forwardDirectionVector = transform.parent.up;
                break;
            case Axis.Z:
                forwardDirectionVector = transform.parent.forward;
                break;
            case Axis.mX:
                forwardDirectionVector = -transform.parent.right;
                break;
            case Axis.mY:
                forwardDirectionVector = -transform.parent.up;
                break;
            case Axis.mZ:
                forwardDirectionVector = -transform.parent.forward;
                break;
        }
        switch (upDirection)
        {
            case Axis.X:
                upDirectionVector = transform.parent.right;
                break;
            case Axis.Y:
                upDirectionVector = transform.parent.up;
                break;
            case Axis.Z:
                upDirectionVector = transform.parent.forward;
                break;
            case Axis.mX:
                upDirectionVector = -transform.parent.right;
                break;
            case Axis.mY:
                upDirectionVector = -transform.parent.up;
                break;
            case Axis.mZ:
                upDirectionVector = -transform.parent.forward;
                break;
        }

        //Rotar el objeto para que la cara frontal mire hacia el control
        transform.rotation = Quaternion.LookRotation(forwardDirectionVector, upDirectionVector);
        transform.localPosition = Vector3.zero;

        //Eje profundidad
        switch (localZAxis)
        {
            case Axis.X:
                localZAxisVector = Vector3.right;
                break;
            case Axis.Y:
                localZAxisVector = Vector3.up;
                break;
            case Axis.Z:
                localZAxisVector = Vector3.forward;
                break;
            case Axis.mX:
                localZAxisVector = -Vector3.right;
                break;
            case Axis.mY:
                localZAxisVector = -Vector3.up;
                break;
            case Axis.mZ:
                localZAxisVector = -Vector3.forward;
                break;
        }
        //Mover el objeto a un offset deseado como posición inicial
        transform.Translate(localZAxisVector * movementLimits.x, Space.Self);
    }

    public void Drop()
    {
        transform.SetParent(null);
        //Equipo para ubicar en el rack
        if (useGravity)
        {
            rg.useGravity = true;
            rg.freezeRotation = false;
        }
        //Conector
        else
        {
            rg.isKinematic = true;
            //Si no ha sido tomado por el snap
            if (!blocked)
            {
                ResetTransform();
            }
        }
        grabbing = false;
    }

    /// <summary>
    /// Movimiento en dirección forward
    /// </summary>
    /// <param name="sentido"> -1: Acercar 1:Alejar</param>
    public void MoveForward(int sentido)
    {
        movementValue = movementSpeed * sentido;
        if (forwardMovement)
        {
            if ((transform.localPosition.z + movementValue) >= movementLimits.x && (transform.localPosition.z + movementValue) <= movementLimits.y)
                transform.Translate(movementValue * localZAxisVector, Space.Self);
        }
    }

    #endregion

    /// <summary>
    /// Resatura la posicion inicial cuando se ubicó incorrectamente
    /// </summary>
    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.localEulerAngles = initialRotation;
    }

    public void DeactivateCableSimulation()
    {
        // if (TryGetComponent<CableActivation>(out CableActivation cable))
        //     cable.DeactivateCableComponent();

        Destroy(this);
    }

    /// <summary>
    /// Elimina scripts y componentes innecesarios luego del snap
    /// </summary>
    public void Deactivate(bool detenerSimulacionCable)
    {
        if (detenerSimulacionCable) Drop();
        if (!keepInteractable) this.tag = "Untagged";
        if (useGravity)
        {
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this);
        }

    }
}
