using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* Name: cambio_escena1
* 
* Description: clase que permite el cambio de escena a la pedida dentro de la interfaz de unity
* Params:  N/A
* 
* Return: N/A
**/

public class cambio_escena1 : MonoBehaviour
{

   public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
