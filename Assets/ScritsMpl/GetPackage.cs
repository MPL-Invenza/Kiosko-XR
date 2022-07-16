using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Nombre: GetPackage
 * 
 * Descripcion: objeto que oculta un objeto dentro de la escena
 * 
 **/

public class GetPackage : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelMsg;

    public void showHidePanleMsg()
    {
        panelMsg.gameObject.SetActive(false);
    }
}
