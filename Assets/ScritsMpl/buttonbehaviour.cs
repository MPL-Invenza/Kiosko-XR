using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * Name: buttonbehaviour
 * Description: da el comportamiento al boton para observar el contenido
 * Params: NO
 * Return: NO 
 * 
 * **/
public class buttonbehaviour : MonoBehaviour
{
    public GameObject panel;
    public bool done = false;

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && !done)
        {
            GetComponent<Button>().onClick.AddListener(delegate ()
            {
                LauncherUtils.DoSomething(panel.GetComponent<InfoPanel>().objetctmodel);
            });
            done = true;
        }
    }
}
