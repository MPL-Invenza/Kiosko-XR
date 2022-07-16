using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConsola : MonoBehaviour
{
    public MeshRenderer consola;
    private byte prueba = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            prueba++;
            if (prueba > 2) prueba = 0;
            CambiarColorConsola(prueba);
        }
    }

    /// <summary>
    /// Cambiar el color de emisión de la consola
    /// </summary>
    /// <param name="estado">0: normal, 1:conectado, 2:error</param>
    public void CambiarColorConsola(int estado)
    {
        switch (estado)
        {
            case 0://Normal
                consola.material.SetColor("_EmissionColor", Color.white);
                break;
            case 1://Conectado
                consola.material.SetColor("_EmissionColor", Color.green);
                break;
            case 2://Error
                consola.material.SetColor("_EmissionColor", Color.red * 2f);
                break;
        }
    }
}
