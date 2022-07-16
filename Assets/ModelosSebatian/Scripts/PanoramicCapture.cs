using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanoramicCapture : MonoBehaviour
{
    public Camera targetCamara;
    public RenderTexture CubeMapLeft;
    public RenderTexture equirecRT;
    //public int Angulo;
    public string NamePanoramic= "Panorama";
    int Pixel = 0;
    float Angulo=0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Capture();
        }
    }

    public void Capture() {
        targetCamara.GetComponent<Camera>().RenderToCubemap(CubeMapLeft);
        //targetCamara.RenderToCubemap(CubeMapLeft);
        CubeMapLeft.ConvertToEquirect(equirecRT);
        Save(equirecRT);
        
    }

    public void Save(RenderTexture rt) {

        Angulo = transform.localRotation.eulerAngles.y;
       
        Pixel = -(rt.width * (int)Angulo) / 360+ rt.width;
        
        Texture2D tex = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;


        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), Pixel, 0);
        tex.ReadPixels(new Rect(rt.width - Pixel, 0, Pixel, rt.height), 0, 0);

        //tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 1000, 0);
        //tex.ReadPixels(new Rect(rt.width-1000, 0,1000, rt.height), 0, 0);

        RenderTexture.active = null;

        

        byte[] bytes = tex.EncodeToJPG();

        string path = Application.dataPath + "/" + NamePanoramic + ".jpg";

        System.IO.File.WriteAllBytes(path, bytes);

    }
}
