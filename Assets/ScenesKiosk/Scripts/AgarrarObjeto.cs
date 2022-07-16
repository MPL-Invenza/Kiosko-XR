using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Axis{
//     X, Y, Z, mX, mY, mZ
// }
public class AgarrarObjeto : MonoBehaviour
{

    //
    //Flecha azul
    [SerializeField]
    [Tooltip("Ejes locales respecto al control")]
    private Axis localZ;

    private Vector3 localZV;

    [SerializeField]
    [Tooltip("Direccion flecha azul cuando se agarra")]
    private Axis direccionForward = Axis.Y;
    [SerializeField]
    [Tooltip("Direccion flecha verde cuando se agarra")]
    private Axis direccionUp = Axis.Z;

    private Vector3 direccionForwardV;
    private Vector3 direccionUpV;

    [Header("Velocidades")]
    [SerializeField]
    [Space]
    private float velocidadMovimiento;

    [Header("Limite")]
    [SerializeField]
    [Space]
    private Vector2 limitesForward;

    public bool usarGravedad = true;
    [HideInInspector]
    public Vector3 posicionInicial;
    private Vector3 rotacionInicial;


    [HideInInspector]
    public bool acercar;

    [HideInInspector]
    public bool bloqueo;

    [HideInInspector]
    public bool agarrando;

    private Rigidbody rg;
    private float movProf = 0;
    public byte snapGroup;
    [HideInInspector]
    public bool onTrigger;

    public bool keepInteractable = false;
    public bool detenerSimulacionCable = true;
    public bool movimientoProfundidad = true;

    void Start()
    {
        //Almacenar transform inicial
        posicionInicial = transform.position;
        rotacionInicial = transform.eulerAngles;
        rg = GetComponent<Rigidbody>();
#if !UNITY_EDITOR
        velocidadMovimiento = velocidadMovimiento / 10;
#endif
    }

    private void Update()
    {
        if (agarrando)
        {
            //Anular velocidad para evitar colisiones elásticas que perturben el movimiento del objeto
            rg.velocity = new Vector3(0, 0, 0);
        }
    }
    #region Agarre

    public void Agarrar(Transform control)
    {
        //Ajustar físicas para facilitar el transporte del objeto
        rg.useGravity = false;
        rg.freezeRotation = true;
        rg.isKinematic = false;
        agarrando = true;

        //Orientación Agarre
        switch (direccionForward)
        {
            case Axis.X:
                direccionForwardV = control.right;
                break;
            case Axis.Y:
                direccionForwardV = control.up;
                break;
            case Axis.Z:
                direccionForwardV = control.forward;
                break;
            case Axis.mX:
                direccionForwardV = -control.right;
                break;
            case Axis.mY:
                direccionForwardV = -control.up;
                break;
            case Axis.mZ:
                direccionForwardV = -control.forward;
                break;
        }
        switch (direccionUp)
        {
            case Axis.X:
                direccionUpV = control.right;
                break;
            case Axis.Y:
                direccionUpV = control.up;
                break;
            case Axis.Z:
                direccionUpV = control.forward;
                break;
            case Axis.mX:
                direccionUpV = -control.right;
                break;
            case Axis.mY:
                direccionUpV = -control.up;
                break;
            case Axis.mZ:
                direccionUpV = -control.forward;
                break;
        }

        //Rotar el objeto para que la cara frontal mire hacia el control
        transform.rotation = Quaternion.LookRotation(direccionForwardV, direccionUpV);
        transform.SetParent(control);
        transform.position = new Vector3(control.position.x, control.position.y, control.position.z);

        //Eje profundidad
        switch (localZ)
        {
            case Axis.X:
                localZV = Vector3.right;
                break;
            case Axis.Y:
                localZV = Vector3.up;
                break;
            case Axis.Z:
                localZV = Vector3.forward;
                break;
            case Axis.mX:
                localZV = -Vector3.right;
                break;
            case Axis.mY:
                localZV = -Vector3.up;
                break;
            case Axis.mZ:
                localZV = -Vector3.forward;
                break;
        }
        //Mover el objeto a un offset deseado como posición inicial
        transform.Translate(localZV * limitesForward.x, Space.Self);
    }

    public void Soltar()
    {
        transform.SetParent(null);
        //Equipo para ubicar en el rack
        if (usarGravedad)
        {
            rg.useGravity = true;
            rg.freezeRotation = false;
        }
        //Conector
        else
        {
            rg.isKinematic = true;
            //Si no ha sido tomado por el snap
            if (!bloqueo)
            {
                RestaurarPosicion();
                // if (TryGetComponent<ActivacionCable>(out ActivacionCable ac))
                // {
                //     ac.DesactivarCable();
                //     ac.OcultarCable();
                // }
            }
            else //Si es tomado por el snap
                Invoke("DesactivarSimulacionCable", 2f);
        }
        agarrando = false;
    }

    /// <summary>
    /// Movimiento en dirección forward
    /// </summary>
    /// <param name="sentido"> -1: Acercar 1:Alejar</param>
    public void MovimientoProfundidad(int sentido)
    {
        movProf = velocidadMovimiento * sentido;
        if (movimientoProfundidad)
        {
            if ((transform.localPosition.z + movProf) >= limitesForward.x && (transform.localPosition.z + movProf) <= limitesForward.y)
                transform.Translate(movProf * localZV, Space.Self);
        }
    }

    #endregion

    /// <summary>
    /// Resatura la posicion inicial cuando se ubicó incorrectamente
    /// </summary>
    public void RestaurarPosicion()
    {
        transform.position = posicionInicial;
        transform.localEulerAngles = rotacionInicial;
    }

    public void DesactivarSimulacionCable()
    {
        // if(detenerSimulacionCable) GetComponent<ActivacionCable>().DesactivarCable();
        // Destroy(this.GetComponent<AgarrarObjeto>());
    }

    /// <summary>
    /// Elimina scripts y componentes innecesarios luego del snap
    /// </summary>
    public void Desactivar()
    {
        Soltar();
        if (!keepInteractable) this.tag = "Untagged";
        if (usarGravedad)
        {
            Destroy(this.GetComponent<Rigidbody>());
            Destroy(this.GetComponent<AgarrarObjeto>());
        }

    }
}
