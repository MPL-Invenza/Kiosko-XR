using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioPosicion : MonoBehaviour
{

    /// <summary>
    /// Oculta el spawn point seleccionado
    /// </summary>
    /// <param name="nPosicion">Posición del spawn point en la jerarquía</param>
    public void Ocultar(byte nPosicion)
    {
        //nPosicion=0 (microscopio)
        //nPosicion=1 (Rack)
        //nPosicion=2 (Conectores)
        for (byte i = 0; i < transform.childCount; i++)
        {
            if (i==nPosicion)
                transform.GetChild(i).GetComponent<BoxCollider>().enabled=false;
            else
                transform.GetChild(i).GetComponent<BoxCollider>().enabled=true;
        }

        // if (GetComponent<CollidersManager>())
        // {
        //     DesactivarColliders(nPosicion);
        // }
    }

    // private void DesactivarColliders(byte nPosicion)
    // {
    //     if (nPosicion==1)
    //     {
    //         GetComponent<CollidersManager>().Desactivar(2);
    //         GetComponent<CollidersManager>().Activar(1);
    //     }
    //     else if(nPosicion==2)
    //     {
    //         GetComponent<CollidersManager>().Desactivar(1);
    //         GetComponent<CollidersManager>().Activar(2);
    //     }
    // }
}
