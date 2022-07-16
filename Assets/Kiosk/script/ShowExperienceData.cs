using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class ShowExperienceData : MonoBehaviour
{
    public RawImage image;
    public Text titulo;
    public Text tituloCorto;
    public Text duracion;
    public Text objetivos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateData(string imageUrl, string title, string shortTitle, string length, string objectives)
    {
        Texture2D textureImg = new Texture2D(1, 1);
        textureImg.LoadImage(File.ReadAllBytes(imageUrl));
        textureImg.Apply();
        image.texture = textureImg;
        titulo.text = title;
        tituloCorto.text = shortTitle;
        duracion.text = length;
        objetivos.text = objectives;
        //image.mainTexture
    }
}
