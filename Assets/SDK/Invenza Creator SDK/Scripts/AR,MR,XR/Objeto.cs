using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/**
* Name: Objeto
* Description: Clase que contiene los parametros basicos de los objetos dentro de las experiencias
* 
* Params:  
* 
* NAME_MODEL:  nombre del modelo dentro de cada experiencia
* PATH_IMAGE_REF: camino relativo para la imagen que ilustra el modelo dentro de las experiencas
* PATH_MODEL: camino del modelo tridimensional, este puede ser obj o gltf
* SCALE_MODEL: tamaño del modelo en un formato x,y,z
* PATH_MODEL_LABEL: camino a los labels generados para el objeto 3D en particular (valido para RA)
* HOTSPOTS: es un arreglo que muestra los hotspots generados por el usuario* 
* 
* Return: N/A
 * */
[Serializable]
public class Objeto
{
    public string NAME_MODEL;
    public string PATH_IMAGE_REF;
    public string PATH_MODEL;
    public string SCALE_MODEL;
    public string PATH_MODEL_LABEL;
    public List<SubObjeto> HOTSPOTS = new List<SubObjeto>();
}
