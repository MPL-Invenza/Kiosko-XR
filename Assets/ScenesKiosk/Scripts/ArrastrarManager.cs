using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Comparacion{
    Mayor,Menor
}
public class ArrastrarManager : MonoBehaviour
{
    public ArrastrarObjeto[] arrastrarScripts;
    [Tooltip("X: limite, Y:Script")]
    public Vector2[] limitesSnap;
    public float[] offsets;
    private Comparacion[] comparaciones;
    private bool desactivado;
    private byte contPosiciones=0;
    private byte posicionPasada=0;
    private bool configuracionInicial;
    private bool asignacionComparacion;
    private bool cicloCompletado;
    // Start is called before the first frame update
    void Start()
    {
        comparaciones = new Comparacion[limitesSnap.Length];
    }

    // Update is called once per frame
    void Update()
    {

        if (!configuracionInicial)
        {
            for (int i = 0; i < arrastrarScripts.Length; i++)
            {
                if (i>0)
                    arrastrarScripts[i].enabled = false;
            }
            configuracionInicial = true;
        }

        //Asignacion comparación y signo offset
        if (!cicloCompletado && !asignacionComparacion && arrastrarScripts[(int)limitesSnap[contPosiciones].y].valorEje < limitesSnap[contPosiciones].x)
        {
            comparaciones[contPosiciones] = Comparacion.Mayor;
            offsets[contPosiciones] = -offsets[contPosiciones];
            asignacionComparacion = true;
        }
        else if (!cicloCompletado && !asignacionComparacion && arrastrarScripts[(int)limitesSnap[contPosiciones].y].valorEje > limitesSnap[contPosiciones].x)
            {
                comparaciones[contPosiciones] = Comparacion.Menor;
                asignacionComparacion = true;
            }

        //Si ya se asignó la comparación
        if (asignacionComparacion || cicloCompletado)
        {
            if (comparaciones[contPosiciones]==Comparacion.Mayor)
            {
                //Si el valor del eje actual es mayor o igual al limite+offset
                //Si el script actual está activo
                if (arrastrarScripts[(int)limitesSnap[contPosiciones].y].valorEje>=(limitesSnap[contPosiciones].x+offsets[contPosiciones])
                    && arrastrarScripts[(int)limitesSnap[contPosiciones].y].enabled)
                {
                    arrastrarScripts[(int)limitesSnap[contPosiciones].y].enabled = false;
                    contPosiciones++;
                }
            }
            else
            {
                //Si el valor del eje actual es menor o igual al limite+offset
                //Si el script actual está activo
                if (arrastrarScripts[(int)limitesSnap[contPosiciones].y].valorEje<=(limitesSnap[contPosiciones].x+offsets[contPosiciones])
                    && arrastrarScripts[(int)limitesSnap[contPosiciones].y].enabled)
                {
                    arrastrarScripts[(int)limitesSnap[contPosiciones].y].enabled = false;
                    contPosiciones++;
                }
            }
        }


        if (contPosiciones==comparaciones.Length)
        {
            contPosiciones = 0;
            cicloCompletado = true;
        }

        if (posicionPasada!=contPosiciones)
        {
            arrastrarScripts[(int)limitesSnap[contPosiciones].y].enabled = true;
            posicionPasada = contPosiciones;
            asignacionComparacion = false;
        }

    }
}
