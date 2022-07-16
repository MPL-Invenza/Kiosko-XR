using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadAppData : MonoBehaviour
{
    /// <summary>
    /// Variable tipo PackageList
    /// </summary>
    public PackageList AppsInstalled = new PackageList();
    /// <summary>
    /// booleano que comprueba la existencia de un app dentro del archivo
    /// </summary>
    bool exists = false;

    /// <summary>
    /// variable que desde el editor indica cual e el archivo que debe ser utilizado dentro del nivel dado
    /// </summary>
    public string scoreMis = "AppList";
    /// <summary>
    /// string que permite almacenar en un solo lugar el camino donde se encuentran los archivos de guardado
    /// </summary>
    private string gameDataProjectFilePath;
    /// <summary>
    /// Dirección del archivo donde se almacena la conexión
    /// </summary>
    private string Connectionpath;



    /// <summary>
    /// Variables de la clase que referencian diferentes objetos dentro de la escena
    /// </summary>
    public GameObject manager;
    //public AndroidHelper andHelp;
    public HelperXR andHelp;
    public AndroidHelper andHelp2;
    public DownloadUtils downloadUtils;
    public CreateElement_Local localelement;
    public create_element create_Element_sc;
    public Text userMessages;
    private bool uninstalled;
    private Docente docInfo;
    /// <summary>
    /// al inicializar se define el path para leer y guardar el archivo json
    /// </summary>
    private void Start()
    {
        //LoadGameData();        
        andHelp = manager.GetComponent<HelperXR>();
        andHelp2= manager.GetComponent<AndroidHelper>();
        downloadUtils = manager.GetComponent<DownloadUtils>();
        //Hacer para ambos casos 
#if UNITY_EDITOR
        gameDataProjectFilePath = Application.streamingAssetsPath + "/" + scoreMis + ".json";
        Connectionpath = Application.streamingAssetsPath + "/" + "Conection.json";
#elif UNITY_ANDROID
        
        gameDataProjectFilePath = downloadUtils.GetAndroidInternalFilesDir() + "/" + scoreMis + ".json";
        Connectionpath = downloadUtils.GetAndroidInternalFilesDir() + "/" + "Conection.json";
#endif
        Debug.Log("deberia guardar el save aqui: " + gameDataProjectFilePath);
        localelement = manager.GetComponent<CreateElement_Local>();

        LoadGameData();

    }


    /// <summary>
    /// Metodo que permite cargar la informacion del archivo json
    /// </summary>
    public void LoadGameData()
    {
        string filePath = gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            AppsInstalled = JsonUtility.FromJson<PackageList>(dataAsJson);


            for (int i = 0; i < AppsInstalled.appList.Count; i++)
            {
                localelement.CreateCards(AppsInstalled.appList[i].type, i, AppsInstalled.appList[i].appName, AppsInstalled.appList[i].imgRoute, AppsInstalled.appList[i].appPackage, AppsInstalled.appList[i].element, null);
            }
        }
        else
        {
            AppsInstalled = new PackageList();
        }
    }

    /// <summary>
    /// metodo que guarda informacion al archivo Json
    /// </summary>
    public void SaveGameData()
    {

        string dataAsJson = JsonUtility.ToJson(AppsInstalled);

        string filePath = gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
        if (userMessages.gameObject.activeSelf)
        {
            userMessages.text = "Grabado en memoria";
        }
        else
        {
            userMessages.gameObject.SetActive(true);
            userMessages.text = "Grabado en memoria";
        }

    }

    /// <summary>
    /// Metodo que permite cambiar el puntaje de un jugador que ya exista dentro de la lista
    /// </summary>
    /// <param name="appName">nombre del app</param>
    /// <param name="appPackage">string con el paquete</param>
    /// <param name="imgRoute">ruta de la imagen dentro del dispositivo</param>
    /// <param name="type">tipo de la experiencia</param>
    /// <param name="element">datos traidos desde asira</param>

    public void ChangePackage(string appName, string appPackage, string imgRoute, string type, Element element)
    {
        for (int i = 0; i < AppsInstalled.appList.Count; i++)
        {
            if (AppsInstalled.appList[i].appName.Equals(appName))
            {
                if (AppsInstalled.appList[i].appPackage != appPackage)
                {
                    AppsInstalled.appList[i].appPackage = appPackage;
                }
                if (AppsInstalled.appList[i].imgRoute != imgRoute)
                {
                    AppsInstalled.appList[i].imgRoute = imgRoute;
                }
                if (AppsInstalled.appList[i].type != type)
                {
                    AppsInstalled.appList[i].type = type;
                }
                if (AppsInstalled.appList[i].element != element)
                {
                    AppsInstalled.appList[i].element = element;
                }
            }
        }
    }
    /// <summary>
    /// Elimina un objeto del Json y desinstala el app de las gafas
    /// </summary>
    public void deleteApp(string appName)
    {
        for (int i = 0; i < AppsInstalled.appList.Count; i++)
        {
            if (AppsInstalled.appList[i].appName.Equals(appName))
            {
                //create_Element_sc.userMessages.text = AppsInstalled.appList[i].appPackage;
                if (andHelp.silentUninstall(AppsInstalled.appList[i].appPackage))
                {
                    AppsInstalled.appList.Remove(AppsInstalled.appList[i]);
                }
                break;
            }
        }
        SaveGameData();
    }

    public void lanzar(string packname) {
        andHelp2.lanzarApp(packname);
    }

    /// <summary>
    /// metodo que agrega nuevos apps al archivo si no existe, de no ser asi no agrega nada
    /// </summary>
    /// <param name="appName">puntaje del jugador</param>
    /// <param name="appPackage">nombre del jugador</param>
    /// <param name="imgRoute">puntaje del jugador</param>
    /// <param name="type">nombre del jugador</param>
    /// <param name="element">nombre del jugador</param>
    public void Addplayer(string appName, string appPackage, string imgRoute, string type, Element element)
    {
        /// <summary>
        /// variable que permite generar un nuevo jugador de ser necesario
        /// </summary>
        //Debug.Log("AppsInstaled appList count:" + AppsInstalled.appList.Count);
        PackageData addnewApp = new PackageData();
        exists = false;
        for (int i = 0; i < AppsInstalled.appList.Count; i++)
        {
            if (AppsInstalled.appList[i].appName.Equals(appName))
            {
                exists = true;
                break;
            }

        }
        Debug.Log("exists:" + exists);
        if (!exists)
        {
            addnewApp.appName = appName;
            addnewApp.appPackage = appPackage;
            addnewApp.imgRoute = imgRoute;
            addnewApp.type = type;
            addnewApp.element = element;
            Debug.Log("AppInstalled add");
            AppsInstalled.appList.Add(addnewApp);
            Debug.Log("AppInstalled added");
        }
    }


    /// <summary>
    /// guarda la información de la conexión en un Json, para su uso en las experiencias
    /// </summary>
    /// <param name="docInfo"></param>
    public void SaveConnection(Docente docInfo)
    {
        string dataAsJson = JsonUtility.ToJson(docInfo);

        string filePath = Connectionpath;
        File.WriteAllText(filePath, dataAsJson);
        if (userMessages.gameObject.activeSelf)
        {
            userMessages.text = "Grabada conexión en memoria";
        }
        else
        {
            userMessages.gameObject.SetActive(true);
            userMessages.text = "Grabada conexión en memoria";
        }
    }


}
