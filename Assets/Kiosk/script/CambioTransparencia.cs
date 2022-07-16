using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioTransparencia : MonoBehaviour
{
    public Material[] material;
    private Color[] color;
    [SerializeField]
    private float[] transparencia;
    public bool transparente;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color[material.Length];
        for (byte i = 0; i < material.Length; i++)
        {
            color[i] = material[i].GetColor("_BaseColor");
        }

    }

    public void CambiarTransparencia()
    {
        if (!transparente)
        {
            //Hacer transparente
            for (byte i = 0; i < material.Length; i++)
            {
                material[i].SetColor("_BaseColor", new Color(color[i].r, color[i].g, color[i].b, transparencia[i]));
            }
        }
        else
        {
            //Hacer sólido
            for (byte i = 0; i < material.Length; i++)
            {
                material[i].SetColor("_BaseColor", new Color(color[i].r, color[i].g, color[i].b, 1));
            }
        }

        transparente = !transparente;
    }
}
