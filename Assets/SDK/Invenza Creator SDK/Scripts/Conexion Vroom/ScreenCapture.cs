using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * 
 * Name: ScreenCapture
 * 
 * Description: Captura la pantalla para la emision de informacion a Vroom
 * 
 * RETURN: imagen en formato string base 64bit para que se transmita a Vroom
 * 
 * */
public class ScreenCapture : MonoBehaviour
{
    public int resWidth;
    public int resHeight;
    public Camera maincam;
    public bool takeHiResShot = false;
    private RenderTexture rt;

    private Texture2D screenShot;

    string base64String;
    /** 
     * Name: ScreenShotName
     * 
     * Description: define el nombre para el screenshot de caso de ser necesario
     * 
     * PARAM: width, height
     * 
     * RETURN: el nombre para el screenshot en formato string
     * 
     * */
    public string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    /** 
     * Name: TimedScreen
     * 
     * Description: funcion que actua como activador para permitir la captura controlada de pantalla
     * 
     * PARAM:
     *  
     * RETURN:
     * 
     * */
    public void TimedScreen()
    {
        takeHiResShot = true;
        //Debug.Log("tomo un screenshot");
    }

    /** 
     * Name: Start
     * 
     * Description: actua antes que cualquier otro metodo de la clase
     * 
     * PARAM:
     *  
     * RETURN: genera un objeto tipo RenderTexture de tamaño definido por el desarrollador. Genera ademas una textura 2D con las mismas dimensiones
     * 
     * */
    void Start()
    {
        rt = new RenderTexture(resWidth, resHeight, 24);
        screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
    }
    /** 
     * Name: GetScreenshot
     * 
     * Description: captura la informacion de la pantalla para formatearla como base64bit
     * 
     * PARAM:
     * 
     * RETURN: informacion en base64bit para transimitir
     * 
     * */
    public string GetScreenshot()
    {
        try
        {
            maincam.targetTexture = rt;

            maincam.Render();
            RenderTexture.active = rt;
            if (BroadcastConnection.docente!=null)
            {
                resWidth = int.Parse(BroadcastConnection.docente.width_v);
                resHeight = int.Parse(BroadcastConnection.docente.height_v);
            }
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            maincam.targetTexture = null;
            RenderTexture.active = null;
            byte[] bytes = screenShot.EncodeToPNG();
            base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            bytes = null;
            return "data:image/png;base64," + base64String;
        }
        catch (Exception e)
        {
            Debug.Log(e.Data);
            return "hola";
        }

    }

    /** 
    * Name: OnDestroy
    * 
    * Description: destruye la informacion requerida para limpiar memoria
    * 
    * PARAM:
    * 
    * RETURN: 
    * */
    void OnDestroy()
    {
        Destroy(rt);
        Destroy(screenShot);
    }

}
