using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Name: ToggleRenderer
* 
* Description: clase que permite encender o apagar el componente Renderer de algun objeto en la escena
* Params:  N/A
* 
* Return: N/A
**/
public class ToggleRenderer : MonoBehaviour
{

    public void ToggleVisibility()
    {

    	Renderer rend = gameObject.GetComponent<Renderer>();

    	if(rend.enabled)
    		rend.enabled=false;
    	else
    		rend.enabled=true;
    }
}
