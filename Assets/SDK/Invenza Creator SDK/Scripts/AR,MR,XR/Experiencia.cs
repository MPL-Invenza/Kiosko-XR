using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* Name: Experiencia
* 
* Description: Clase que contiene los parametros basicos de la experiencia
* 
* Params:  
* 
* SHORT_TITLE:  Nombre de la experiencia en general
* TYPE: el tipo de experiencia que se esta generando, puede ser MR, AR, i360, v360, VR
* URL_INTERNAL_FILE: camino relativo al documento index.html para las experiencias i360, v360, y video
* NAME_FILE_ZIP: nombre del archivo en formato zip
* URL_FILE: dirección para el archivo de tipo zip
* MODEL: es un arreglo que muestra los modelos que hacen parte de las experiencias MR, AR
* 
* Return: N/A
 * */

[Serializable]
public class Experiencia
{
    public string SHORT_TITLE;
    public string TYPE;
    public string URL_INTERNAL_FILE;
    public string NAME_FILE_ZIP;
    public string URL_FILE;
    public List<Objeto> MODEL = new List<Objeto>();
}
