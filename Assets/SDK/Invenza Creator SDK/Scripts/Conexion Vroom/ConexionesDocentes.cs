using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * NAME: ConexionesDocentes
 * 
 * Descripcion: obtiene informacion desde Vroom, muestra en forma de lista las posibles conexiones que se pueden generar desde el Vroom
 * 
 * 
 * RETURN: Conexion a la plataforma Vroom * 
 */
public class ConexionesDocentes : MonoBehaviour
{
    public Dropdown dropdownDocentes;

    private List<Dropdown.OptionData> downlist;

    public List<Docente> docentesactuales = new List<Docente>();

    public GameObject manager;

    public DownloadUtils loader;

    public GameObject createelement;

    private create_element element;

    public static bool connected = false;

    private void Awake()
    {

        loader = manager.GetComponent<DownloadUtils>();
        element = createelement.GetComponent<create_element>();

        //dropdownDocentes.ClearOptions();
    }


    /**
     * 
     * Nombre: PopulateDropdown
     * 
     * Param: Dropdown, List<Docente>
     * 
     * 
     * Descripcion: toma la informacion de la lista de tipo Docente y la ubica en la lista grafica tipo Dropdown
     * 
     **/
    public void PopulateDropdown(Dropdown dropdown, List<Docente> optionsArray)
    {        
        List<string> options = new List<string>();
        int count = 0;
        foreach (var option in optionsArray)
        {
            options.Add(optionsArray[count].teacherName); // Or whatever you want for a label
            count++;
        }
        //dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    /**
    * 
    * Nombre: changeip
    * 
    * Param: index
    * 
    * Descripcion: se ejecuta cada que cambia una opcion dentro de la lista grafica, conecta con la ip que esta incrustada en cada opcion
    * 
    **/
    public void changeip(int index)
    {
        loader.url = docentesactuales[index - 1].ipAddress;
        loader.teachername = docentesactuales[index - 1].id_user;
        //Debug.Log("debo cambiar de ip");
        for (int i = 0; i < element.objectlist.Count; i++)
        {
            Destroy(element.objectlist[i]);
        }
        connected = true;
        manager.GetComponent<LoadAppData>().SaveConnection(docentesactuales[index - 1]);
        manager.GetComponent<WebSocketConnection>().sendinfo();
        //element.selectElement();
    }
}


/**
 * 
 * NOMBRE: Docente
 * 
 * Descripcion: objeto que almacena la informacion proveniente de Vroom 
 **/
[Serializable]
public class Docente
{
    public string teacherName;

    public string fps_v;

    public string height_v;

    public string width_v;

    public string ipAddress;

    public string id_user;

    public Boolean Equals(Docente docente)
    {
        return this.teacherName == docente.teacherName & this.ipAddress == docente.ipAddress;
    }
}