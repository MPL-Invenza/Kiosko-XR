using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Nombre: Element
 *
 * Descripcion: Clase que recibe la informacion sobre los contenidos que puede ver cada dispositivo dentro del Vroom
 *
 **/

[Serializable]
public class Element
{/*Object to serializable, dl: download, pkg:package*/
    public string id;
    public string id_content_asira;
    public string id_contets__short_title;
    public string id_contets__name;
    public string id_contets__img;
    public string id_contets__duration;
    public string id_contets__type_contents;
    public string id_contets__size_contents;
    public string id_contets__description;
    public string version;
    public string user_id;
    public string id_device;
    public string id_contets__id_Category__name;
    public string id_contets__url_file;
    public string id_contets__url_internal_file;
    public string path_folder_content;
}
