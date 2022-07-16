
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Compression;
using System.IO;
using UnityEngine.UI;
/**
 * 
 * Nombre: create_element
 * 
 * Descripcion: Clase que realiza la consulta a Vroom sobre los contenidos a mostrar y crea los elementos en interfaz para que sean interactuables
 * 
 **/

public class create_element : MonoBehaviour
{

    public GameObject CardView;

    //public GameObject panelMsg;
    float sliderValue = 0.0f;
    Boolean isfinish = false;
    int varControl = 0;
    DownloadUtils downloadUtils = new DownloadUtils();
    //AndroidHelper androidHelper = new AndroidHelper();
    HelperXR HelperXR = new HelperXR();
    public string filename;
    public string filename_apk;
    public List<GameObject> objectlist = new List<GameObject>();
    public bool installed;
    public GameObject manager;
    public Text Status;
    public Text userMessages;
    public DebugFile file;
    string jsonfile;
    public Transform carousel;
    int count = 0;
    public LoadAppData installedApps;
    public GameObject Lanzar;
    public GameObject desinstalar;

    Experiencia expData = new Experiencia();
    void zipstuff()
    {
        Debug.Log("no sirvo pa nada");
    }

    void Start()
    {
        downloadUtils = manager.GetComponent<DownloadUtils>();
        installedApps = manager.GetComponent<LoadAppData>();
        string routeFile = downloadUtils.GetAndroidInternalFilesDir() + "/6.png";
        //Debug.Log("EL DATAPATH ES:  " + Application.streamingAssetsPath + "//////////////////////////////");
        //selectElement();
        file = manager.GetComponent<DebugFile>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isfinish && varControl == 1)
        {

            varControl = 0;
        }

    }

    void CreateElementDynamic()
    {
        GameObject cardViewTemp;
        BaseModel model;

        for (int i = 1; i < 8; i++)
        {
            model = new BaseModel();
            Debug.Log("Before mod" + i);
            if (i % 2 == 0)
            {
                model.mid = -1;
                model.dataType = DataType.VIDEO;
                model.title = "Invasion";
                model.imageUrl = "http://192.168.0.27/vroom/images/" + (i + 5) + ".png";
                model.url = "/sdcard/pre_resource/video/Invasion.mp4";
                model.videoType = 0;
                Debug.Log("Entro en Video" + i);
            }
            else
            {

                model.mid = -1;
                model.dataType = DataType.APP;
                model.title = "FileManager";
                model.imageUrl = "http://192.168.0.27/vroom/images/" + (i + 5) + ".png";
                model.packageName = "com.pvr.filemanager";
                Debug.Log("Entro en App" + i);

            }
            cardViewTemp = Instantiate(CardView, transform);
            cardViewTemp.GetComponent<HomeItem>().index = i;
            cardViewTemp.GetComponent<HomeItem>().model2 = model;

        }

    }

    /**
     * 
     * Nombre: CreateElementDynamicType
     * 
     * 
     * Params: String type, int id, String title, String urlImage, String urlOPackage, Element[] element, string filename
     * 
     * Descripcion: crea los elementos traidos dentro del objeto Element utilizando la informacio y la codificacion de Pico
     * 
     * 
     * */
    public void CreateElementDynamicType(String type, int id, String title, String urlImage, String urlOPackage, Element[] element, string filename)
    {
        GameObject cardViewTemp;
        BaseModel model = new BaseModel();
        bool exist = false;
        switch (type)
        {
            case "i360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en imagen 360");
                break;
            case "v360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en video 360");
                break;
            case "VIDEO":
                model.mid = -1;
                model.dataType = DataType.VIDEO;
                model.title = title;
                model.imageUrl = urlImage;
                Debug.Log("Entro en video");
                break;
            case "VR":
                model.mid = -1;
                model.dataType = DataType.APP;
                model.title = title;
                model.imageUrl = urlImage;
                model.packageName = urlOPackage;
                Debug.Log("Entro en vr");
                installedApps.Addplayer(title, urlOPackage, urlImage, type, element[count]);
                file.WriteToFile("estos son los datos que creo:\n" + model.dataType.ToString() + "\n" + model.title.ToString() + "\n" + model.imageUrl.ToString() + "\n" + model.packageName.ToString());
                Debug.Log("estos son los datos que creo:\n" + model.dataType.ToString() + "\n" + model.title.ToString() + "\n" + model.imageUrl.ToString() + "\n" + model.packageName.ToString());
                break;
            default:
                Console.WriteLine("Default case");
                break;
        }

        //Original
        //Debug.Log("Voy a verificar si existe tarjeta");
        //Debug.Log(objectlist.Count);
        //for (int i = 0; i < objectlist.Count; i++)
        //{
        //    Debug.Log("model2=" + objectlist[i].GetComponent<HomeItem>().model2);
        //    Debug.Log("model=" + model);
        //    if (objectlist[i].GetComponent<HomeItem>().model2 == model)
        //    {
        //        exist = true;
        //        break;
        //    }
        //}
        //if (!exist)
        //{
        //    cardViewTemp = Instantiate(CardView, carousel);
        //    cardViewTemp.GetComponent<HomeItem>().index = id;
        //    cardViewTemp.GetComponent<HomeItem>().model2 = model;
        //    cardViewTemp.transform.name = cardViewTemp.transform.name + " " + title;
        //    cardViewTemp.GetComponent<HomeItem>().elementdescription = element[count];
        //    objectlist.Add(cardViewTemp);
        //    cardViewTemp.GetComponent<HomeItem>().initData();
        //    count++;
        //}
        //else
        //{
        //    file.WriteToFile(model.title + " ya existia, no se crea tarjeta");
        //}

        //Modified
        for (int i = 0; i < objectlist.Count; i++)
        {
            if (objectlist[i].GetComponentInChildren<HomeItem>().model2 == model)
            {
                exist = true;
                break;
            }
        }
        if (!exist)
        {
            cardViewTemp = Instantiate(CardView, carousel);
            cardViewTemp.GetComponentInChildren<HomeItem>().index = id;
            cardViewTemp.GetComponentInChildren<HomeItem>().model2 = model;
            cardViewTemp.transform.name = cardViewTemp.transform.name + " " + title;
            cardViewTemp.GetComponentInChildren<HomeItem>().elementdescription = element[count];
            objectlist.Add(cardViewTemp);
            cardViewTemp.GetComponentInChildren<HomeItem>().initData();
            count++;
        }
        else
        {
            file.WriteToFile(model.title + " ya existia, no se crea tarjeta");
        }
    }

    public void CreateElementDynamicType(String type, int id, String title, String urlImage, String urlOPackage, Element element, string filename)
    {
        GameObject cardViewTemp;
        BaseModel model = new BaseModel();
        bool exist = false;
        switch (type)
        {
            case "i360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en imagen 360");
                break;
            case "v360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en video 360");
                break;
            case "VIDEO":
                model.mid = -1;
                model.dataType = DataType.VIDEO;
                model.title = title;
                model.imageUrl = urlImage;
                Debug.Log("Entro en video");
                break;
            case "VR":
                model.mid = -1;
                model.dataType = DataType.APP;
                model.title = title;
                model.imageUrl = urlImage;
                model.packageName = urlOPackage;
                Debug.Log("Entro en vr");
                installedApps.Addplayer(title, urlOPackage, urlImage, type, element);
                file.WriteToFile("estos son los datos que creo:\n" + model.dataType.ToString() + "\n" + model.title.ToString() + "\n" + model.imageUrl.ToString() + "\n" + model.packageName.ToString());
                Debug.Log("estos son los datos que creo:\n" + model.dataType.ToString() + "\n" + model.title.ToString() + "\n" + model.imageUrl.ToString() + "\n" + model.packageName.ToString());
                break;
            default:
                Console.WriteLine("Default case");
                break;
        }

        //Modified
        for (int i = 0; i < objectlist.Count; i++)
        {
            if (objectlist[i].GetComponentInChildren<HomeItem>().model2 == model)
            {
                exist = true;
                break;
            }
        }

        for (int i = 0; i < objectlist.Count; i++)
        {
            if (objectlist[i].GetComponentInChildren<HomeItem>().index == id)
            {

                objectlist[i].GetComponentInChildren<HomeItem>().model2 = model;
                objectlist[i].transform.name = objectlist[i].transform.name + " " + title;
                objectlist[i].GetComponentInChildren<HomeItem>().elementdescription = element;
                objectlist[i].GetComponentInChildren<HomeItem>().initData();
                objectlist[i].GetComponentInChildren<HomeItem>().activateinfo();
                exist = true;
                break;
            }
        }
        if (!exist)
        {
            cardViewTemp = Instantiate(CardView, carousel);
            cardViewTemp.GetComponentInChildren<HomeItem>().index = id;
            cardViewTemp.GetComponentInChildren<HomeItem>().model2 = model;
            cardViewTemp.transform.name = cardViewTemp.transform.name + " " + title;
            cardViewTemp.GetComponentInChildren<HomeItem>().elementdescription = element;
            objectlist.Add(cardViewTemp);
            cardViewTemp.GetComponentInChildren<HomeItem>().initData();
        }
        else
        {
            file.WriteToFile(model.title + " ya existia, no se crea tarjeta");
        }
    }





    public void CreateElementDynamicTypeNoArray(String type, int id, String title, String urlImage, String urlOPackage, Element element, string filename)
    {
        GameObject cardViewTemp;
        BaseModel model = new BaseModel();

        switch (type)
        {
            case "i360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en imagen 360");
                break;
            case "v360":
                model.mid = -1;
                model.dataType = DataType.URL;
                model.title = title;
                model.imageUrl = urlImage;
                model.url = urlOPackage;
                model.videoType = 0;
                Debug.Log("Entro en video 360");
                break;
            case "VIDEO":
                model.mid = -1;
                model.dataType = DataType.VIDEO;
                model.title = title;
                model.imageUrl = urlImage;
                Debug.Log("Entro en video");
                break;
            case "VR":
                model.mid = -1;
                model.dataType = DataType.APP;
                model.title = title;
                model.imageUrl = urlImage;
                model.packageName = urlOPackage;
                Debug.Log("Entro en vr");
                break;
            default:
                Console.WriteLine("Default case");
                break;
        }
        //Original
        //cardViewTemp = Instantiate(CardView, carousel);
        //cardViewTemp.GetComponent<HomeItem>().index = id;
        //cardViewTemp.GetComponent<HomeItem>().model2 = model;
        //cardViewTemp.transform.name = cardViewTemp.transform.name + " " + title;
        //cardViewTemp.GetComponent<HomeItem>().elementdescription = element;
        //objectlist.Add(cardViewTemp);
        //cardViewTemp.GetComponent<HomeItem>().initData();

        //Modified
        cardViewTemp = Instantiate(CardView, carousel);
        cardViewTemp.GetComponentInChildren<HomeItem>().index = id;
        cardViewTemp.GetComponentInChildren<HomeItem>().model2 = model;
        cardViewTemp.transform.name = cardViewTemp.transform.name + " " + title;
        cardViewTemp.GetComponentInChildren<HomeItem>().elementdescription = element;
        objectlist.Add(cardViewTemp);
        cardViewTemp.GetComponentInChildren<HomeItem>().initData();
    }





    /**
     * 
     * Nombre: selectElement
     * 
     * Descripcion: realiza la consulta sobre los elementos que deben ser mostrados en el HDM, utilizando el metodo Get_data del objeto DownloadUtils
     * 
     * 
     * */
    public void selectElement()
    {
        Debug.Log("Select element1");
        StartCoroutine(manager.GetComponent<DownloadUtils>().Get_data((Element[] elements) =>
       {
           Debug.Log("Select element2");
           if (!manager.GetComponent<DownloadUtils>().url.Equals("0.0.0.0"))
           {
               Debug.Log("SelectElement downLoad true");
               StartCoroutine(downLoadElements(elements));
               //userMessages.text = "Conectando con servidor IP: " + manager.GetComponent<DownloadUtils>().url + "Sincroniza contenidos";
               //userMessages.text = manager.GetComponent<DownloadUtils>().url;
           }
           else
           {
               Debug.Log("SelectElement downLoad false");
               //panelMsg.gameObject.SetActive(true);
               userMessages.text = "Se debe establecer primero una conexión con Vroom";


           }

       }));
    }


    /**
     * 
     * Nombre: downLoadElements
     * 
     * Descripcion: Corrutina que tomando los contenidos del arreglo de tipo elements, descarga todos los archivos correspondientes a cada una de las experiencias.
     * 
     * Params: Element[] elements
     * 
     * Return: Archivos descargados dentro del dispositivo
     * 
     * */
    IEnumerator downLoadElements(Element[] elements)
    {
        Boolean conti = false;
        int count = 1;
        foreach (Element element in elements)
        {
            filename = splitURL(element.id_contets__img);

            filename_apk = splitURL(element.id_contets__url_file);
            Debug.Log("este es el nombre del archivo: ______" + filename);


#if UNITY_EDITOR
            switch (element.id_contets__type_contents)
            {
                case "VR":
                    Debug.Log("ENTRO A VR EDITOR");
                    if (!downloadUtils.verifyFilesExist(Application.streamingAssetsPath + "/" + filename))
                    //if (!downloadUtils.verifyFilesExist(Application.dataPath + "/StreamingAssets" + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText2("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, Application.streamingAssetsPath + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found VR image");
                                StartCoroutine(downloadUtils.GetText2("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_file/*"http://192.168.10.18:8880/downloads/ExperienciaRack_.zip"*/, Application.streamingAssetsPath + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_file);
                                    if (res2)
                                    {
                                        string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                                        Debug.Log(foldername);
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".apk"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            Status.text = "";

                                            //if (installapp(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername + "/" + element.id_contets__url_file))
                                            {
                                                //CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                                CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                                            }
                                            count++;
                                            conti = true;
                                            Debug.Log("Found VR");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found VR");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    else
                    {
                        Debug.Log("descargada pero no se sabe su estatus");
                        string folderpath = filename_apk.Substring(0, filename_apk.Length - 4);
                        Debug.Log("este es folderpath: " + folderpath);
                        string[] subFolders = Directory.GetDirectories(Application.streamingAssetsPath + "/" + folderpath);
                        if (subFolders != null)
                        {
                            foreach (string subfolder in subFolders)
                            {
                                //file.WriteToFile("nombre: "+fileName+"\n");
                                Debug.Log("esta es una carpeta dentro de el origen: " + subfolder);
                                string[] fileEntriesS = Directory.GetFiles(subfolder);
                                foreach (string fileNameM in fileEntriesS)
                                {
                                    string fileext = Path.GetExtension(fileNameM);
                                    Debug.Log("el formato es: " + fileext);
                                    if (fileext.Equals(".json"))
                                    {
                                        jsonfile = subfolder;
                                        Debug.Log("este debe ser el archivo json: " + jsonfile);
                                        break;
                                    }
                                }
                            }
                        }
                        ReadJson(jsonfile, element);
                        file.WriteToFile("esta es la ruta que debo leer: " + jsonfile);
                        if (!AppUtils.Instance.IsInstall(element.id_contets__url_internal_file))
                        {
                            Debug.Log("no esta instalada");
                            string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                            file.WriteToFile("foldername= " + foldername);
                            Debug.Log("esta es la ruta a instalar el apk" + Application.streamingAssetsPath + "/" + foldername + "/" + element.id_contets__url_file);
                            //if (installapp(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername + "/" + element.id_contets__url_file))
                            {
                                //file.WriteToFile("este deberia ser el paquete "+element.id_contets__url_file.ToString());
                                //string folderpath= filename.Substring(0, filename.Length - 4);                                
                                file.WriteToFile("este deberia ser el paquete despues de leer el json " + element.id_contets__url_file.ToString());
                                //CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);                                
                                CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                                conti = true;
                                Debug.Log("Into install");
                            }
                            conti = true;
                        }
                        else
                        {
                            /*Debug.Log("en teoria estoy instalada");
                             string FOLPATH= filename_apk.Substring(0, filename_apk.Length - 4);
                             Debug.Log("este es FOLPATH: "+FOLPATH);

                             string[] fileEntries = Directory.GetFiles(downloadUtils.GetAndroidInternalFilesDir() + "/" + FOLPATH);  
                             foreach (string fileName in fileEntries)
                             {
                             //file.WriteToFile("nombre: "+fileName+"\n");
                              Debug.Log("el archivo se llama: "+fileName);
                             }*/
                            //string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                            //string folderpath= filename.Substring(0, filename.Length - 4);

                            ReadJson(jsonfile, element);
                            //CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                            conti = true;
                        }
                    }
                    break;
                case "i360":
                    Debug.Log("ENTRO A I360");
                    if (!downloadUtils.verifyFilesExist(Application.streamingAssetsPath + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, Application.streamingAssetsPath + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_internal_file, Application.streamingAssetsPath + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".html"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found i360");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo html");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found i360");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                case "v360":
                    Debug.Log("ENTRO A V360");
                    if (!downloadUtils.verifyFilesExist(Application.streamingAssetsPath + "/" + filename))
                    //if (!downloadUtils.verifyFilesExist(Application.dataPath + "/StreamingAssets" + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, Application.streamingAssetsPath + "/" + filename, (Boolean res) =>
                        //StartCoroutine(downloadUtils.GetText(element.id_contets__img, Application.dataPath + "/StreamingAssets" + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_internal_file, Application.streamingAssetsPath + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".html"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found v360");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found v360");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                case "VIDEO":
                    Debug.Log("ENTRO A VIDEO");
                    if (!downloadUtils.verifyFilesExist(Application.streamingAssetsPath + "/" + filename))
                    //if (!downloadUtils.verifyFilesExist(Application.dataPath + "/StreamingAssets" + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, Application.streamingAssetsPath + "/" + filename, (Boolean res) =>
                        //StartCoroutine(downloadUtils.GetText(element.id_contets__img, Application.dataPath + "/StreamingAssets" + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_file, Application.streamingAssetsPath + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".mp4"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, Application.streamingAssetsPath + "/" + filename, element.id_contets__url_internal_file, elements, Application.streamingAssetsPath + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found Video");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found video");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                default:
                    break;
            }
#elif UNITY_ANDROID
            switch (element.id_contets__type_contents)
            {
                case "VR":
                    Debug.Log("ENTRO EN VR ANDROID");
                    if (!downloadUtils.verifyFilesExist(downloadUtils.GetAndroidInternalFilesDir() + "/" + filename))
                    {
                        conti = false;
                        Debug.Log("comienzo GetText");
                        StartCoroutine(downloadUtils.GetText2("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText2("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_file/*"http://192.168.10.18:8880/downloads/ExperienciaRack_.zip"*/, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".apk"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            Debug.Log("Voy a instalar");
                                            Status.text = "";

                                            if (installapp(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername + "/" + element.id_contets__url_file))
                                            {
                                                CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                            }
                                            count++;
                                            conti = true;
                                            Debug.Log("Found VR");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found VR");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    else
                    {
                        Debug.Log("descargada pero no se sabe su estatus");
                        string folderpath= filename_apk.Substring(0, filename_apk.Length - 4);
                        Debug.Log("este es folderpath: "+folderpath);
                        string[] subFolders=Directory.GetDirectories(downloadUtils.GetAndroidInternalFilesDir() + "/" + folderpath);
                        if (subFolders!=null)
                        {
                            foreach (string subfolder in subFolders)
                            {
                                //file.WriteToFile("nombre: "+fileName+"\n");
                                Debug.Log("esta es una carpeta dentro de el origen: "+subfolder);
                                string[] fileEntriesS = Directory.GetFiles(subfolder);
                                foreach (string fileNameM in fileEntriesS)
                                {
                                string fileext=Path.GetExtension(fileNameM);
                                Debug.Log("el formato es: "+fileext);                                
                                    if (fileext.Equals(".json"))
                                    {
                                        jsonfile= subfolder;
                                        Debug.Log("este debe ser el archivo json: "+jsonfile);
                                        break;
                                    }                                    
                                }                                 
                            }
                        } 
                        ReadJson(jsonfile,element);
                        file.WriteToFile("esta es la ruta que debo leer: "+jsonfile);
                        if (!AppUtils.Instance.IsInstall(element.id_contets__url_internal_file))
                        {
                            Debug.Log("no esta instalada");
                            string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                            file.WriteToFile("foldername= "+foldername);
                            Debug.Log("esta es la ruta a instalar el apk"+downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername + "/" + element.id_contets__url_file);
                            if (installapp(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername + "/" + element.id_contets__url_file))
                            {   
                                //file.WriteToFile("este deberia ser el paquete "+element.id_contets__url_file.ToString());
                                //string folderpath= filename.Substring(0, filename.Length - 4);                                
                                file.WriteToFile("este deberia ser el paquete despues de leer el json "+element.id_contets__url_file.ToString());
                                CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                conti = true;
                                Debug.Log("Into install");
                            }
                            conti = true;
                        }
                        else
                        {
                            /*Debug.Log("en teoria estoy instalada");
                             string FOLPATH= filename_apk.Substring(0, filename_apk.Length - 4);
                             Debug.Log("este es FOLPATH: "+FOLPATH);

                             string[] fileEntries = Directory.GetFiles(downloadUtils.GetAndroidInternalFilesDir() + "/" + FOLPATH);  
                             foreach (string fileName in fileEntries)
                             {
                             //file.WriteToFile("nombre: "+fileName+"\n");
                              Debug.Log("el archivo se llama: "+fileName);
                             }*/
                            //string foldername = filename_apk.Substring(0, filename_apk.Length - 4);
                            //string folderpath= filename.Substring(0, filename.Length - 4);
                            
                            Debug.Log("en teoria estoy instalada");
                            ReadJson(jsonfile,element);
                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                            conti = true;
                        }
                    }
                    break;
                case "i360":
                    if (!downloadUtils.verifyFilesExist(downloadUtils.GetAndroidInternalFilesDir() + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_internal_file, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".html"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found VR");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found VR");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                case "v360":
                    if (!downloadUtils.verifyFilesExist(downloadUtils.GetAndroidInternalFilesDir() + "/" + filename))
                    //if (!downloadUtils.verifyFilesExist(Application.dataPath + "/StreamingAssets" + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, (Boolean res) =>
                        //StartCoroutine(downloadUtils.GetText(element.id_contets__img, Application.dataPath + "/StreamingAssets" + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_internal_file, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".html"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found VR");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found VR");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                case "VIDEO":
                    if (!downloadUtils.verifyFilesExist(downloadUtils.GetAndroidInternalFilesDir() + "/" + filename))
                    //if (!downloadUtils.verifyFilesExist(Application.dataPath + "/StreamingAssets" + "/" + filename))
                    {
                        conti = false;
                        StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, (Boolean res) =>
                        //StartCoroutine(downloadUtils.GetText(element.id_contets__img, Application.dataPath + "/StreamingAssets" + "/" + filename, (Boolean res) =>
                        {
                            if (res)
                            {
                                Debug.Log("Found image");
                                StartCoroutine(downloadUtils.GetText("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + "/static" + element.id_contets__url_internal_file, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename_apk, (Boolean res2) =>
                                {
                                    Debug.Log(manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img);
                                    if (res2)
                                    {
                                        if (UnzipAsync(filename_apk, element).id_contets__url_file.Contains(".mp4"))
                                        {
                                            Debug.Log("termine de descomprimir");
                                            CreateElementDynamicType(element.id_contets__type_contents, count, element.id_contets__name, downloadUtils.GetAndroidInternalFilesDir() + "/" + filename, element.id_contets__url_internal_file, elements, downloadUtils.GetAndroidInternalFilesDir() + "/" + element.id_contets__url_file);
                                            count++;
                                            conti = true;
                                            Debug.Log("Found VR");
                                        }
                                        else
                                        {
                                            Debug.LogError("no hay un archivo instalable");
                                            conti = true;
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("No Found VR");
                                        conti = true;
                                    }
                                }));
                            }
                            else
                            {
                                Debug.Log("No Found image");
                                conti = true;
                            }
                        }));
                    }
                    break;
                default:
                    break;
            }
#endif
            yield return new WaitUntil(() => conti);
        }
        userMessages.text = "Contenidos descargados. Ya puedes lanzar la experiencia que desees.";
        installedApps.SaveGameData();
    }


    /**
     * 
     * Nombre: splitURL
     * 
     * Descripcion: toma la direccion proveniente de el objeto Elements, y la convierte en un string que se utiliza como nombre del archivo
     * 
     * Params: string url
     * 
     * Return: un string con el nombre del archivo en formato "/filename.extension
     **/
    public string splitURL(string url)
    {
        string[] splitUrl = url.Split(new Char[] { '/' });
        string imageName = splitUrl[splitUrl.Length - 1];

        //imageName = imageName.Remove(0,1);

        return imageName;
    }

    public string splitURLfolder(string entry)
    {
        string[] splitUrl = entry.Split(new Char[] { '/' });
        string imageName = splitUrl[splitUrl.Length - 2];

        //imageName = imageName.Remove(0,1);

        return imageName;
    }

    /**
    * 
    * Nombre: installapp
    * 
    * Descripcion: Instala la aplicacion que sollicite
    * 
    * */
    public Boolean installapp(string filename)
    {
        Debug.Log("no he instalado nada");
        Status.text = "Instalando";
        file.WriteToFile("debo instalar el apk:    " + filename);
        Debug.Log(filename);
        //if (installed = androidHelper.silentInstallApp(filename, "co.com.mpl.KioskPicoXr"))
        Debug.Log("almacenamiento"+HelperXR.GetStorage());
        //filename = "/mnt/sdcard/RACK_DATALOG.apk";
        if (installed = HelperXR.silentInstall(filename))
        {
            Status.text = "Instalación exitosa";
            Lanzar.SetActive(true);
            desinstalar.SetActive(true);
        }
        else
        {
            Status.text = "Instalación fallida";
        }
        return installed;
    }

    private Element UnzipAsync(string filepath, Element element)
    {
        Status.text = "Descomprimiendo";

#if UNITY_EDITOR
        if (Path.GetExtension(filepath).Equals(".zip"))
        {
            string foldername = filepath.Substring(0, filepath.Length - 4);
            Directory.CreateDirectory(Application.streamingAssetsPath + "/" + foldername);

            if (!Directory.Exists(Application.streamingAssetsPath + "/" + foldername))
            {
                ZipFile.ExtractToDirectory(Application.streamingAssetsPath + "/" + filepath, Application.streamingAssetsPath + "/" + foldername);
            }
            else
            {
                try
                {
                    Directory.Delete(Application.streamingAssetsPath + "/" + foldername, true);
                    ZipFile.ExtractToDirectory(Application.streamingAssetsPath + "/" + filepath, Application.streamingAssetsPath + "/" + foldername);
                }
                catch (IOException ex)
                {
                    Debug.Log("Error: " + ex);
                }
            }
            #region coment

            //ZipFile.ExtractToDirectory(Application.streamingAssetsPath + "/" + filepath, Application.streamingAssetsPath + "/" + foldername);
            /*using (ZipArchive archive = ZipFile.OpenRead(Application.streamingAssetsPath + "/" + filepath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));
                    if (!entry.FullName.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
                    {
                        //string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));
                        entry.ExtractToFile(destinationPath, true);
                    }
                }
            }*/
            #endregion

            /////re asignacion de variables
            using (ZipArchive archive = ZipFile.OpenRead(Application.streamingAssetsPath + "/" + filepath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //entry.ExtractToFile(entry.Name, true);
                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));

                        string json = System.IO.File.ReadAllText(destinationPath);

                        //Experiencia expData = new Experiencia();

                        expData = JsonUtility.FromJson<Experiencia>(json);


                        element.id_contets__url_file = expData.URL_FILE;
                        element.id_contets__url_internal_file = expData.NAME_FILE_ZIP;
                        element.id_contets__name = expData.SHORT_TITLE;
                        element.path_folder_content = foldername;
                        Debug.Log(destinationPath);
                    }
                    else
                    {
                        //element.id_contets__url_file = null;
                        //element.id_contets__url_internal_file = null;
                        Debug.Log("no es json");
                    }
                }

            }
            System.IO.File.Delete(Application.streamingAssetsPath + "/" + filepath);
            return element;
        }
#elif UNITY_ANDROID
        if (Path.GetExtension(filepath).Equals(".zip"))
        {
            string foldername = filepath.Substring(0, filepath.Length - 4);

            //Directory.CreateDirectory(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername);
            //ZipFile.ExtractToDirectory(Application.streamingAssetsPath + "/" + filepath, Application.streamingAssetsPath + "/" + foldername);
            if (!Directory.Exists(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername))
            {
                file.WriteToFile("no existia la carpeta");
                ZipFile.ExtractToDirectory(downloadUtils.GetAndroidInternalFilesDir() + "/" + filepath, downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername);
                file.WriteToFile("extraido en la direccion: " + downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername);
            }
            else
            {
                try
                {
                    file.WriteToFile("ya existia la carpeta");
                    Directory.Delete(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername, true);
                    ZipFile.ExtractToDirectory(downloadUtils.GetAndroidInternalFilesDir() + "/" + filepath, downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername);
                    file.WriteToFile("extraido en la direccion: " + downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername);

                }
                catch (IOException ex)
                {
                    Debug.Log("Error: " + ex);
                    file.WriteToFile("error: " + ex.ToString());
                }
            }
            using (ZipArchive archive = ZipFile.OpenRead(downloadUtils.GetAndroidInternalFilesDir() + "/" + filepath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //entry.ExtractToFile(entry.Name, true);
                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        // Gets the full path to ensure that relative segments are removed.
                        string destinationPath = Path.GetFullPath(Path.Combine(downloadUtils.GetAndroidInternalFilesDir() + "/" + foldername, entry.FullName));

                        string json = System.IO.File.ReadAllText(destinationPath);

                        Experiencia expData = new Experiencia();

                        expData = JsonUtility.FromJson<Experiencia>(json);


                        element.id_contets__url_file = expData.URL_FILE;
                        element.id_contets__url_internal_file = expData.NAME_FILE_ZIP;
                        Debug.Log("el paquete es: "+ element.id_contets__url_internal_file);
                        element.id_contets__name = expData.SHORT_TITLE;
                        element.path_folder_content = foldername;

                        Debug.Log(destinationPath);
                    }
                    else
                    {
                        //element.id_contets__url_file = null;
                        //element.id_contets__url_internal_file = null;
                        Debug.Log("no es json");
                    }
                }

            }
            System.IO.File.Delete(downloadUtils.GetAndroidInternalFilesDir() + "/" + filepath);
            return element;
        }
#endif
        return element;
    }

    public bool UnzipAsyncAsira(string filepath, string folderpath, ContentVroomDB contentVroomDBOriginal, out ContentVroomDB contentVroomDBOut)
    {
        Status.text = "Descomprimiendoo";
        ContentVroomDB vroomDB = contentVroomDBOriginal;
        //#if UNITY_EDITOR
        if (Path.GetExtension(filepath).Equals(".zip"))
        {
            Directory.CreateDirectory(folderpath);

            if (Directory.Exists(folderpath))
            {
                ZipFile.ExtractToDirectory(filepath, folderpath);
            }
            else
            {
                try
                {
                    Directory.Delete(folderpath, true);
                    ZipFile.ExtractToDirectory(filepath, folderpath);
                }
                catch (IOException ex)
                {
                    Debug.Log("Error: " + ex);
                }
            }
            ///re asignacion de variables
            using (ZipArchive archive = ZipFile.OpenRead(filepath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    //entry.ExtractToFile(entry.Name, true);
                    if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.Log("Descomprimiendo1");
                        Debug.Log("folderpath"+folderpath);
                        Debug.Log("entry"+entry.FullName);
                        string destinationPath = Path.GetFullPath(Path.Combine(folderpath, entry.FullName));
                        Debug.Log("destinationPath" + destinationPath);
                        string json = System.IO.File.ReadAllText(destinationPath);
                        Debug.Log("json"+json);
                        Experiencia expData = new Experiencia();
                        Debug.Log("Descomprimiendo1");
                        expData = JsonUtility.FromJson<Experiencia>(json);
                        Debug.Log("Descomprimiendo2");
                        //vroomDB.url = expData.URL_FILE;
                        vroomDB.URL_INTERNAL_FILE = expData.NAME_FILE_ZIP;
                        Debug.Log("Descomprimiendo3");
                        vroomDB.SHORT_TITLE = expData.SHORT_TITLE;
                        Debug.Log("Descomprimiendo4");
                        vroomDB.APK_FILE_PATH = expData.URL_FILE;
                        Debug.Log("Descomprimiendo5");
                        //contentVroomDBOut = vroomDB;
                        //sQLit.ReplaceData(vroomDB);
                    }
                    else
                    {
                        Debug.Log("no es json");
                    }
                    Debug.Log("Descomprimiendo6");
                }
                Debug.Log("Descomprimiendo7");
            }
            Debug.Log("Descomprimiendo8");
            System.IO.File.Delete(filepath);
        }
        contentVroomDBOut = vroomDB;
        return true;
    }

    private void ReadJson(string folderpath, Element element)
    {

        string filepath = folderpath + "/manifest.json";

        if (File.Exists(filepath))
        {
            string json = System.IO.File.ReadAllText(filepath);
            Experiencia expData = new Experiencia();
            expData = JsonUtility.FromJson<Experiencia>(json);
            element.id_contets__url_file = expData.URL_FILE;
            element.id_contets__url_internal_file = expData.NAME_FILE_ZIP;
            element.id_device = expData.URL_INTERNAL_FILE;
            Debug.Log("el paquete es: " + element.id_contets__url_internal_file);
            element.id_contets__name = expData.SHORT_TITLE;
            Debug.Log(filepath);
        }
    }
}
