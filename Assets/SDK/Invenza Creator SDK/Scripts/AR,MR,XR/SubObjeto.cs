using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



/**
* Name: SubObjeto
* 
* Description: Clase que contiene los parametros de los hotspots y sus herederos
* Params:  
* 
* NAME_HOTSPOT:  Nombre del hotspot
* POSITION_HOTSPOT: posicion del hotspot en formato x,y,z
* ROTATION_HOTSPOT: rotacion del hotspot en formato x,y,z
* TYPE: tipo de el hotspot (debe ir vacio)
* PATH_ARRAY: camino para los archivos dentro del hotspot (debe ir vacio)
* TITLE_PANEL: el nombre del panel
* POSITION_PANEL: posicion del panel en formato x,y,z
* SCALE_PANEL:  escala del panel en formato x,y,z
* ROTATION_PANEL: rotacion del panel en formato x,y,z
* NUM_PANEL: numero del panel
* BUTTONS:  un arreglo que contiene los botones dentro de cada hotspot
* 
* 
* Return: N/A
 * */

[Serializable]
public class SubObjeto
{
    public string NAME_HOTSPOT;
    public string POSITION_HOTSPOT;
    public string ROTATION_HOTSPOT;
    public string IMAGE_HOTSPOT;
    public string TYPE;
    public string PATH_ARRAY;
    public string TITLE_PANEL;
    public string POSITION_PANEL;
    public string SCALE_PANEL;
    public string ROTATION_PANEL;
    public string NUM_PANEL;
    public List<Botones> BUTTONS = new List<Botones>();
}


/**
* Name: Botones
* 
* Description: Clase que contiene los parametros de los botones
* Params:  
* 
* TYPE: el tipo de boton que se esta generando, puede ser video, text o image.
* PATH_ARRAY: camino a cada uno de los archivos que se encuentran asignados a cada boton
* 
* 
* Return: N/A
 * */

[Serializable]
public class Botones
{
    public string TYPE;
    public string PATH_ARRAY;
}