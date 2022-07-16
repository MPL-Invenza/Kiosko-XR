using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

/**
 * Nombre : InfoPanel
 * Descripcion: Clase que recopila la informacion proveniente de vroom para mostrarla en la interfaz grafica para el usuario
 **/
public class InfoPanel : MonoBehaviour
{
    public create_element create_Element_sc;
    public Element item;
    public BaseModel objetctmodel;
    public Text nombre, nombrecorto, duracion, objetivos;
    public static GameObject activeitem;
    GameObject nowActive;
    private bool done = false;
    public RawImage imagen;
    public Button botonactivacion;
    public LoadAppData manager;
    DownloadUtils downloadUtils = new DownloadUtils();

    public GameObject downloadButton;
    //public GameObject updateButton;

    public AsiraRestful asira;

    public void callscene()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {

        if (activeitem != nowActive)
        {
            UpdateItemData();
            EvaluateExperienceState();
        }

    }

    public void UpdateItemData()
    {
        Debug.Log(activeitem);
        nowActive = activeitem;
        botonactivacion.onClick.AddListener(delegate ()
        {
            LauncherUtils.DoSomething(activeitem.GetComponent<HomeItem>().model2);
            Debug.Log("debo abrir el app");
        });
        Debug.Log("boton Lanzar listener added");
        item = activeitem.GetComponent<HomeItem>().elementdescription;
        objetctmodel = activeitem.GetComponent<HomeItem>().model;
        nombre.text = item.id_contets__name;
        nombrecorto.text = item.id_contets__short_title;
        duracion.text = item.id_contets__duration;
        objetivos.text = item.id_contets__description;
        imagen.texture = activeitem.GetComponent<HomeItem>().image.texture;
        Debug.Log("Final update InfoPanel");
    }

    public void EvaluateExperienceState()
    {
        if (asira.CheckIfExperienceIsDownloaded(int.Parse(item.id), out bool needToUpdate))
        {
            downloadButton.SetActive(false);
            //updateButton.SetActive(needToUpdate);
        }
        else
        {
            downloadButton.SetActive(true);
        }
    }
    public void UninstallAndDestroy()
    {
        //create_Element_sc.userMessages.text = item.id_contets__name;
        manager.deleteApp(item.id_contets__name);
        
        //Destroy(activeitem);
        //nombre.text = "";
        //nombrecorto.text = "";
        //duracion.text = "";
        //objetivos.text = "";
        //imagen.texture = null;
    }

    public void LanzarExp() {
        Debug.Log(item.id_contets__name);
        manager.lanzar(item.id_contets__name);
    }

    public void UninstallAndDestroy(string contentName)
    {
        manager.deleteApp(contentName);
    }

    public void DeleteFolders()
    {
        Debug.Log(item.path_folder_content);
        //string fileOrDirectory = item.id_contets__url_internal_file;
        //#if UNITY_ANDROID
        //        string fileOrDirectory = downloadUtils.GetAndroidInternalFilesDir() + "/" + item.path_folder_content;
        //#elif UNITY_EDITOR
        //        string fileOrDirectory = Application.streamingAssetsPath + "/" + item.path_folder_content;
        //#endif
        //Debug.Log(fileOrDirectory);
        //        fileOrDirectory.Count<char>().
        create_Element_sc.userMessages.text = item.path_folder_content;
        if (!item.path_folder_content.Equals(""))
        {
            //File.Delete(objetctmodel.imageUrl);

            Directory.Delete(item.path_folder_content, true);
        }
        EvaluateExperienceState();
    }

    public void DeleteFolders(string folderPath)
    {
        //Debug.Log(folderPath);
        create_Element_sc.userMessages.text = folderPath;
        if (!folderPath.Equals(""))
        {
            //File.Delete(objetctmodel.imageUrl);

            Directory.Delete(folderPath, true);
        }
    }

    public void BorradoRecursivo(string fileOrDirectory)
    {
        if (Directory.Exists(fileOrDirectory))
        {
            foreach (var item in Directory.GetFiles(fileOrDirectory))
            {
                BorradoRecursivo(item);
            }
        }
        if (!Directory.EnumerateFiles(fileOrDirectory).Any())
        {
            Directory.Delete(fileOrDirectory);
        }
        File.Delete(fileOrDirectory);
    }

}
