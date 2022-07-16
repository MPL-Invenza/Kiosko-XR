using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/**
* Name: visibilidad
* 
* Description: clase que permite activar o desactivar un objeto determinado en la escena
* Params:  N/A
* 
* Return: N/A
**/

public class visibilidad : MonoBehaviour{
    public GameObject Cajainfo1;
    public GameObject Cajainfo2;
    bool state;
  
      public void vis_info1()
      {
          state = !state; 
          Cajainfo1.gameObject.SetActive (state);
      }

      public void vis_info2()
      {
          state = !state; 
          Cajainfo2.gameObject.SetActive (state);
      }
    }
