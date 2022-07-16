using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//using Microsoft.Unity.VisualStudio.Editor;
using SQLite4Unity3d;
using System.IO.Compression;
using UnityEngine.Events;
//using System.Threading;

[Serializable]
public class StatusChecker
{
    public string STATUS;
}

[Serializable]
public class UserInfo
{
    public string USER_NAME;
    public string ID_USER;
    public string SITE_ID;
}
[Serializable]
public class ContentVroom
{
    public string NAME_FILE_ZIP;
    public string URL_INTERNAL_FILE;
    public string SHORT_TITLE;
    public string NAME_EXPERIENCES;
    public string DESC_OBJETIVES;
    public string DESC_DURATION;
    public string IMAGE_CONTENT;
    public string TYPE;
    public string VERSION;
    public string ID_ELEMENT;
}

[Serializable]
public class VroomReader
{
    public UserInfo USER;
    public List<ContentVroom> LIST_CONTENT;
}

[Serializable]
public class VroomDataContent
{
    public string DATA_CONTENT;
}

public class AsiraRestful : MonoBehaviour
{
    public string eid = "";
    public string pw = "";
    public string IPPCVroom = "";
    public string urlAsira = "http://api.asira.co/loginVroom";
    public ShowExperienceData sExpData;
    private SQLiteTest sQLit;
    public create_element create_Element_sc;
    public Text eidPanel;
    public Text pwPanel;
    //public Text IPPanel;
    public Canvas Panel_element;
    public GameObject manager;
    public GameObject CerrarSesion;
    //public LoadAppData manager;

    public PackageList AppsInstalled = new PackageList();

    private DownloadUtils downloadUtils;
    private AlmacenamientodeDatos txtdatos;
    private Element[] elements;
    private HelperXR andHelp;

    public InfoPanel infoPanel;
    public GameObject CanvasLogOut;

    public UnityEvent onSuccessfulConnection;

    // Start is called before the first frame update
    void Start()
    {
        sQLit = new SQLiteTest("TestVroom.db");
        downloadUtils = FindObjectOfType<DownloadUtils>();
        txtdatos = new AlmacenamientodeDatos();
        andHelp = manager.GetComponent<HelperXR>();
    }

    // Update is called once per frame
    void Update()
    {
        // print(eid);
        // print(pw);
    }

    public void CoroutineCaller(string method)
    {

        StartCoroutine(method);
    }

    public void LogOu()
    {
        ////sQLit.CreateVroomContentDB();
        ///

        unistallAllApps();

        txtdatos.logout();

        //Panel_element.SetActive(true);
        Panel_element.enabled = true;


    }

    //IEnumerator
    public void unistallAllApps() 

    {
        //bool[] existsInAsira = new bool[sQLit.GetTableRowCount()];
        //ArrayList listIdContent = new ArrayList();

        //Verify if the contents saved in Asira are the same ones saved in the local database

        for (int j = 0; j < sQLit.GetTableRowCount(); j++)
        {
            
            bool isInstalled = AppUtils.Instance.IsInstall(sQLit.GetContent(j).URL_INTERNAL_FILE);
            if (isInstalled)
            {
                //create_Element_sc.userMessages.text = sQLit.GetContent(j).URL_INTERNAL_FILE;
                if (andHelp.silentUninstall(sQLit.GetContent(j).URL_INTERNAL_FILE))
                {
                    //Directory.Delete(sQLit.GetContent(j).FOLDER_PATH);
                    infoPanel.DeleteFolders(sQLit.GetContent(j).FOLDER_PATH);

                    //create_Element_sc.userMessages.text = "a";
                    //sQLit.DeleteContentInTable(j);
                    //create_Element_sc.userMessages.text = "b";
                    //create_Element_sc.userMessages.text = "true";

                }
            }
            else {

                create_Element_sc.userMessages.text = isInstalled.ToString();
            }

        }
        sQLit.CreateVroomContentDB();
        CanvasLogOut.SetActive(false);
    }

    //public void DeletFolder()

    //{
    //    for (int j = 0; j < sQLit.GetTableRowCount(); j++)
    //    {

    //        bool isInstalled = AppUtils.Instance.IsInstall(sQLit.GetContent(j).URL_INTERNAL_FILE);
    //        if (isInstalled)
    //        {
    //            //create_Element_sc.userMessages.text = sQLit.GetContent(j).URL_INTERNAL_FILE;
    //            if (andHelp.silentUninstall(sQLit.GetContent(j).URL_INTERNAL_FILE))
    //            {
    //                Directory.Delete(sQLit.GetContent(j).FOLDER_PATH);
    //                sQLit.DeleteContentInTable(j);
    //                //create_Element_sc.userMessages.text = "true";
    //            }
    //        }
    //        else
    //        {

    //            create_Element_sc.userMessages.text = isInstalled.ToString();
    //        }

    //    }

    //}

    //public static void ExecProcess(string name, string args)
    //{
    //    Process p = new Process();
    //    p.StartInfo.FileName = name;
    //    p.StartInfo.Arguments = args;
    //    p.StartInfo.RedirectStandardError = true;
    //    p.StartInfo.RedirectStandardOutput = true;
    //    p.StartInfo.CreateNoWindow = true;
    //    p.StartInfo.UseShellExecute = false;
    //    p.Start();

    //    string log = p.StandardOutput.ReadToEnd();
    //    string errorLog = p.StandardError.ReadToEnd();

    //    p.WaitForExit();
    //    p.Close();
    //}

    //public string IPVroomPc() { 
    //return IPPCVroom = IPPanel.text;
    //}

        public IEnumerator GetDataVroom()
    {

        eid = eidPanel.text;
        pw = pwPanel.text;
        //IPPCVroom = IPPanel.text;


        // print(5);
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        // Assuming the perl script manages high scores for different games
        form.AddField("eid", eid);

        // The name of the player submitting the scores
        form.AddField("pw", pw);

        create_Element_sc.userMessages.text = "Conectando con Asira";
        Debug.Log("eid"+eid);
        Debug.Log("pw" + pw);
        // Debug.Log("error");
        using (UnityWebRequest www = UnityWebRequest.Post(urlAsira, form))
        {

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error downloading: " + www.error);
            }
            else
            {
                // show the response
                //Debug.Log(www.downloadHandler.text);

                //Reads the status from the Json
                StatusChecker statusC = JsonUtility.FromJson<StatusChecker>(www.downloadHandler.text);

                bool STATUS = Convert.ToBoolean(statusC.STATUS);
                if (STATUS)
                {
                    Debug.Log("1.AsiraR");
                    //Check if the table has data
                    Debug.Log(sQLit.TableExists());
                    if (!sQLit.TableExists())
                    {
                        Debug.Log("2.AsiraR");
                        sQLit.CreateVroomContentDB();
                        Debug.Log("3.AsiraR");
                    }
                    Debug.Log("4.AsiraR");
                    create_Element_sc.userMessages.text = "Conectado con Asira";
                    CerrarSesion.SetActive(true);
                    Debug.Log("5.AsiraR");
                    txtdatos.savecredential(eid, pw);
                    onSuccessfulConnection.Invoke();
                    //Save the information in an object of type vroom
                    VroomReader vroom = JsonUtility.FromJson<VroomReader>(www.downloadHandler.text);

                    ///////////----------------------

                    //Read every content information
                    for (int i = 0; i < vroom.LIST_CONTENT.Count; i++)
                    {

                        //vroom.LIST_CONTENT.Count

                        // create_Element_sc.userMessages.text =vroom.LIST_CONTENT[i].NAME_EXPERIENCES;

                        //Path of the image
                        string filePath;
#if UNITY_EDITOR
                        filePath = Application.streamingAssetsPath + "/" + vroom.LIST_CONTENT[i].NAME_EXPERIENCES + ".jpg";
#elif UNITY_ANDROID
                        filePath = downloadUtils.GetAndroidInternalFilesDir() + "/" + vroom.LIST_CONTENT[i].NAME_EXPERIENCES + ".jpg";
#endif

                        //filePath = downloadUtils.GetAndroidInternalFilesDir() + "/" + vroom.LIST_CONTENT[i].NAME_EXPERIENCES + ".jpg";
                        File.WriteAllBytes(filePath, Convert.FromBase64String(vroom.LIST_CONTENT[i].IMAGE_CONTENT));//i
                        //Verify if the image already exists
                        //if (File.Exists(filePath))
                        //{
                        //    create_Element_sc.userMessages.text = "Existe";
                        //}
                        //else
                        //{
                        //    create_Element_sc.userMessages.text = "No existe";
                        //}


                        //Search for this experience in the database
                        if (sQLit.SearchForIDElement(vroom.LIST_CONTENT[i].ID_ELEMENT, out ContentVroomDB content))//i
                        {
                            ContentVroomDB contenLocalData = sQLit.getElementById(vroom.LIST_CONTENT[i].ID_ELEMENT); //i
                            Debug.Log("Exists in Database");
                            //  content.UpdateValues(vroom.LIST_CONTENT[i]);
                            contenLocalData.IMAGE_FILE_PATH = filePath;
                            //create_Element_sc.userMessages.text = content.IMAGE_FILE_PATH;
                            sQLit.ReplaceData(contenLocalData);
                            //create_Element_sc.userMessages.text = sQLit.GetContent(0).IMAGE_FILE_PATH;//i

                        }
                        else
                        {
                            //Create a contentVroomDB in order to save the path of the image
                            ContentVroomDB contentVroomDB = new ContentVroomDB(vroom.LIST_CONTENT[i]);//i
                            contentVroomDB.IMAGE_FILE_PATH = filePath;

                            sQLit.InsertData(contentVroomDB);
                        }

                    }
                    //CompareCloudAndLocalContent(vroom.LIST_CONTENT);

                    for (int i = 0; i < sQLit.GetTableRowCount(); i++)
                    {
                        if (CheckIfExperienceIsDownloaded(i, out bool needToUpdate))//bandera
                        {
                            if (needToUpdate)
                            {
                         
                            }
                        }
                    }
                    #region Elements creation

                    for (int i = 0; i < vroom.LIST_CONTENT.Count; i++)
                    {
                        //vroom.LIST_CONTENT.Count
                        //string idElement = vroom.LIST_CONTENT[i].ID_ELEMENT; 
                        string idElement = vroom.LIST_CONTENT[i].ID_ELEMENT;//i
                        Element element = WriteElement(sQLit.getElementById(idElement));
                        create_Element_sc.CreateElementDynamicType(sQLit.getElementById(idElement).TYPE, sQLit.getElementById(idElement).Id - 1, sQLit.getElementById(idElement).NAME_EXPERIENCES, sQLit.getElementById(idElement).IMAGE_FILE_PATH, sQLit.getElementById(idElement).URL_INTERNAL_FILE, element, sQLit.getElementById(idElement).NAME_FILE_ZIP);
                    }
                    //Panel_element.SetActive(false);
                    Panel_element.enabled = false;
                    //Panel_element.renderMode = 0;
                    #endregion

                }

            }
        }

    }

    public void DownloadContentCoroutine(string contentNumber)
    {

        StartCoroutine("DownloadContent", InfoPanel.activeitem.GetComponent<HomeItem>().elementdescription.id_content_asira);
        //create_Element_sc.userMessages.text = InfoPanel.activeitem.GetComponent<HomeItem>().elementdescription.id_content_asira;
    }

    public void UpdateScreenData(int id)
    {
        sExpData.UpdateData(sQLit.GetContent(id).IMAGE_FILE_PATH, sQLit.GetContent(id).NAME_EXPERIENCES, sQLit.GetContent(id).SHORT_TITLE, sQLit.GetContent(id).DESC_DURATION, sQLit.GetContent(id).DESC_OBJETIVES);
    }

    public IEnumerator DownloadContent(string idElement)
    {
        // Create a form object for sending high score data to the server
        WWWForm form = new WWWForm();

        // The user id
        form.AddField("eid", eid);

        // The password of the user
        form.AddField("pw", pw);

        // 
        form.AddField("siteId", "");

        // The type of the experience
        form.AddField("type", "VR");

        //var ds = new SQLiteTest("TestVroom.db");
        //var vroomDB = ds.GetContentData();

        //Debug.Log("eid : " + eid);
        //Debug.Log("pwd : " + pw);

        // The name of the resource to download
        form.AddField("nameResource", sQLit.getElementById(idElement).NAME_FILE_ZIP);
        Debug.Log("eid : " + eid);
        Debug.Log("pwd : " + pw);
        Debug.Log("nameResource : " + sQLit.getElementById(idElement).NAME_FILE_ZIP);

        //form.AddField("nameResource", "TestAPKv360.zip");
        //Debug.Log("a: " + form);
        //Debug.Log("b: " + form.data);
        using (UnityWebRequest www = UnityWebRequest.Post("http://api.asira.co/getDataContent", form))//"http://35.175.16.166/getDataContent""http://api.asira.co/getDataContent"
        {

            yield return www.SendWebRequest();
            
            //Debug.Log("b: "+UnityWebRequest.Result.Success);
            if (www.result != UnityWebRequest.Result.Success)
            {
                print("Error downloading: " + www.error);
                //Debug.Log("form:" + form);
                //Debug.Log("error:" + www.error);
                //Debug.Log("1: " + www.result);
                //Debug.Log("2: " + UnityWebRequest.Result.Success);
                //create_Element_sc.userMessages.text = "1";
            }
            else
            {
                //create_Element_sc.userMessages.text = "2";
                // show the highscores
                Debug.Log(www.downloadHandler.text);

                StatusChecker statusC = JsonUtility.FromJson<StatusChecker>(www.downloadHandler.text);
                bool STATUS = Convert.ToBoolean(statusC.STATUS);
                if (STATUS)
                {

                    print(1);
                    VroomDataContent contentUrl = JsonUtility.FromJson<VroomDataContent>(www.downloadHandler.text);
                    string filepath;
#if UNITY_EDITOR
                    filepath = Application.streamingAssetsPath + "/" + sQLit.getElementById(idElement).NAME_FILE_ZIP;
#elif UNITY_ANDROID
                        filepath = downloadUtils.GetAndroidInternalFilesDir() + "/" + sQLit.getElementById(idElement).NAME_FILE_ZIP;
#endif
                    // string filepath = Application.streamingAssetsPath + "/" + sQLit.GetContent(contentNumber).NAME_FILE_ZIP;
                    string folderpath = filepath.Substring(0, filepath.Length - 4);

                    StartCoroutine(downloadUtils.GetText2Asira(contentUrl.DATA_CONTENT, filepath, (Boolean res) =>
                    {
                        if (res)
                        {
                            Debug.Log("Calling Unzip");
                            Debug.Log("AsiraRestful");
                            if (create_Element_sc.UnzipAsyncAsira(filepath, folderpath, sQLit.getElementById(idElement), out ContentVroomDB contentVroom))
                            {
                                Debug.Log("AsiraRestful1");
                                contentVroom.FOLDER_PATH = folderpath;
                                contentVroom.VERSION_LOCAL = sQLit.getElementById(idElement).VERSION_SERVER;
                                sQLit.ReplaceData(contentVroom);
                           
                                Element element = WriteElement(sQLit.getElementById(idElement));
                                Debug.Log("AsiraRestful2");
#if UNITY_EDITOR
                                Debug.Log("AsiraRestful3");
                                create_Element_sc.CreateElementDynamicType(sQLit.getElementById(idElement).TYPE, sQLit.getElementById(idElement).Id - 1, sQLit.getElementById(idElement).NAME_EXPERIENCES, sQLit.getElementById(idElement).IMAGE_FILE_PATH, sQLit.getElementById(idElement).URL_INTERNAL_FILE, element, sQLit.getElementById(idElement).NAME_FILE_ZIP);
                                infoPanel.EvaluateExperienceState();
                                infoPanel.UpdateItemData();
                                Debug.Log("AsiraRestful4");
#elif UNITY_ANDROID
                                if (create_Element_sc.installapp(sQLit.getElementById(idElement).FOLDER_PATH + "/" + sQLit.getElementById(idElement).APK_FILE_PATH))
                                {
                                    Debug.Log("AsiraRestful5");
                                    create_Element_sc.CreateElementDynamicType(sQLit.getElementById(idElement).TYPE, sQLit.getElementById(idElement).Id - 1, sQLit.getElementById(idElement).NAME_EXPERIENCES, sQLit.getElementById(idElement).IMAGE_FILE_PATH, sQLit.getElementById(idElement).URL_INTERNAL_FILE, element, sQLit.getElementById(idElement).NAME_FILE_ZIP);
                                    infoPanel.EvaluateExperienceState();
                                    Debug.Log("AsiraRestful6");
                                }
#endif

                            }

                        }
                    }));

                    //StartCoroutine(downloadUtils.GetText2("http://" + manager.GetComponent<DownloadUtils>().url + ":8080" + element.id_contets__img, Application.streamingAssetsPath + "/" + filename, (Boolean res) =>
                }

            }
        }


    }

    //public void WriteElement(int elementNumber)
    //{
    //    int i = elementNumber;
    //    elements[i].id = (sQLit.GetContent(i).Id - 1).ToString();
    //    elements[i].id_content_asira = sQLit.GetContent(i).ID_ELEMENT;
    //    elements[i].id_contets__short_title = sQLit.GetContent(i).SHORT_TITLE;
    //    elements[i].id_contets__name = sQLit.GetContent(i).NAME_EXPERIENCES;
    //    elements[i].id_contets__img = sQLit.GetContent(i).IMAGE_CONTENT;
    //    elements[i].id_contets__duration = sQLit.GetContent(i).DESC_DURATION;
    //    elements[i].id_contets__type_contents = sQLit.GetContent(i).TYPE;
    //    elements[i].id_contets__description = sQLit.GetContent(i).DESC_OBJETIVES;
    //    elements[i].version = sQLit.GetContent(i).VERSION_SERVER;
    //    elements[i].id_contets__url_file = sQLit.GetContent(i).APK_FILE_PATH;
    //    elements[i].id_contets__url_internal_file = sQLit.GetContent(i).URL_INTERNAL_FILE;



    //}

    public Element WriteElement(ContentVroomDB vroomDB)
    {
        Element element = new Element();

        element.id = (vroomDB.Id - 1).ToString();
        element.id_content_asira = vroomDB.ID_ELEMENT;
        element.id_contets__short_title = vroomDB.SHORT_TITLE;
        element.id_contets__name = vroomDB.NAME_EXPERIENCES;
        element.id_contets__img = vroomDB.IMAGE_CONTENT;
        element.id_contets__duration = vroomDB.DESC_DURATION;
        element.id_contets__type_contents = vroomDB.TYPE;
        element.id_contets__description = vroomDB.DESC_OBJETIVES;
        element.version = vroomDB.VERSION_SERVER;
        element.id_contets__url_file = vroomDB.APK_FILE_PATH;
        element.id_contets__url_internal_file = vroomDB.URL_INTERNAL_FILE;
        element.path_folder_content = vroomDB.FOLDER_PATH;

        return element;
    }

    //public ContentVroomDB WriteContentVroomDB(Element element)
    //{
    //    ContentVroomDB contentVroomDB = new ContentVroomDB();

    //    element.id = (vroomDB.Id - 1).ToString();
    //    element.id_content_asira = vroomDB.ID_ELEMENT;
    //    element.id_contets__short_title = vroomDB.SHORT_TITLE;
    //    element.id_contets__name = vroomDB.NAME_EXPERIENCES;
    //    element.id_contets__img = vroomDB.IMAGE_CONTENT;
    //    element.id_contets__duration = vroomDB.DESC_DURATION;
    //    element.id_contets__type_contents = vroomDB.TYPE;
    //    element.id_contets__description = vroomDB.DESC_OBJETIVES;
    //    element.version = vroomDB.VERSION_SERVER;
    //    element.id_contets__url_file = vroomDB.APK_FILE_PATH;
    //    element.id_contets__url_internal_file = vroomDB.URL_INTERNAL_FILE;
    //    element.path_folder_content = vroomDB.FOLDER_PATH;

    //    return element;
    //}

    public void UpdateExperienceData(List<ContentVroom> cloudContent)
    {

    }
    #region Procesos

    #region GextText2Asira commented
    //public IEnumerator GetText2Asira(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    //{
    //    Debug.Log("routeSource: " + routeSource);
    //    //Debug.Log("routeFile: " + routeFile);
    //    float filezise = 0;
    //    //StartCoroutine(GetFileSize(routeSource, (size) =>
    //    //{
    //    //    Debug.Log("File Size: " + size);
    //    //    getsize = size;
    //    //    getsize = getsize / 1000;
    //    //}
    //    //)
    //    //);
    //    using (UnityWebRequest www = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET))
    //    {
    //        var dh = new DownloadHandlerFile(routeFile, true);
    //        dh.removeFileOnAbort = true;
    //        www.downloadHandler = dh;
    //        var asyncOperation = www.SendWebRequest();

    //        while (!dh.isDone)
    //        {
    //            //if (!downloadtext.gameObject.activeSelf)
    //            //{
    //            //    downloadtext.gameObject.SetActive(true);
    //            //}
    //            //filezise = (file_size + Mathf.FloorToInt(www.downloadedBytes)) / 1000;
    //            //downloadtext.text = filezise.ToString() + "KB" + "/" + getsize.ToString() + " KB";
    //            yield return null;
    //        }
    //        #region textodescarga       

    //        //if (!Status.gameObject.activeSelf)
    //        //{
    //        //    Status.gameObject.SetActive(true);
    //        //    Status.text = "Descargando";
    //        //    Debug.Log("descargando");
    //        //}

    //        #endregion
    //        //yield return www.SendWebRequest();
    //        if (www.isNetworkError || www.isHttpError)
    //        {
    //            Debug.Log(www.error);
    //        }
    //        else
    //        {
    //            //byte[] results = www.downloadHandler.data;
    //            if (www.isDone)
    //            {
    //                //downloadtext.gameObject.SetActive(false);
    //                //www.Dispose();
    //                resultCallBack(true);
    //                Debug.Log("Download saved to: " + routeFile);
    //            }
    //            else
    //            {
    //                Debug.Log("no finalizo la descarga");
    //                resultCallBack(false);
    //            }
    //        }
    //    }

    //}
    #endregion

    //public bool UnzipAsyncAsira(string filepath, string folderpath, ContentVroomDB contentVroomDBOriginal, out ContentVroomDB contentVroomDBOut)
    //{
    //    //Status.text = "Descomprimiendo";

    //    ContentVroomDB vroomDB = contentVroomDBOriginal;
    //    //#if UNITY_EDITOR
    //    if (Path.GetExtension(filepath).Equals(".zip"))
    //    {
    //        Directory.CreateDirectory(folderpath);

    //        if (Directory.Exists(folderpath))
    //        {
    //            ZipFile.ExtractToDirectory(filepath, folderpath);
    //        }
    //        else
    //        {
    //            try
    //            {
    //                Directory.Delete(folderpath, true);
    //                ZipFile.ExtractToDirectory(filepath, folderpath);
    //            }
    //            catch (IOException ex)
    //            {
    //                Debug.Log("Error: " + ex);
    //            }
    //        }
    //        #region coment

    //        //ZipFile.ExtractToDirectory(Application.streamingAssetsPath + "/" + filepath, Application.streamingAssetsPath + "/" + foldername);
    //        /*using (ZipArchive archive = ZipFile.OpenRead(Application.streamingAssetsPath + "/" + filepath))
    //        {
    //            foreach (ZipArchiveEntry entry in archive.Entries)
    //            {
    //                string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));
    //                if (!entry.FullName.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    //string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));
    //                    entry.ExtractToFile(destinationPath, true);
    //                }
    //            }
    //        }*/
    //        #endregion

    //        ///re asignacion de variables
    //        using (ZipArchive archive = ZipFile.OpenRead(filepath))
    //        {
    //            foreach (ZipArchiveEntry entry in archive.Entries)
    //            {
    //                //entry.ExtractToFile(entry.Name, true);
    //                if (entry.FullName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
    //                {
    //                    // Gets the full path to ensure that relative segments are removed.
    //                    //string destinationPath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath + "/" + foldername, entry.FullName));
    //                    string destinationPath = Path.GetFullPath(Path.Combine(folderpath, entry.FullName));

    //                    string json = System.IO.File.ReadAllText(destinationPath);

    //                    Experiencia expData = new Experiencia();

    //                    expData = JsonUtility.FromJson<Experiencia>(json);


    //                    //vroomDB.url = expData.URL_FILE;
    //                    vroomDB.URL_INTERNAL_FILE = expData.NAME_FILE_ZIP;
    //                    vroomDB.SHORT_TITLE = expData.SHORT_TITLE;
    //                    vroomDB.APK_FILE_PATH = expData.URL_FILE;

    //                    //contentVroomDBOut = vroomDB;
    //                    //sQLit.ReplaceData(vroomDB);
    //                }
    //                else
    //                {
    //                    Debug.Log("no es json");
    //                }
    //            }

    //        }
    //        System.IO.File.Delete(filepath);
    //    }    
    //    //#endif
    //    #endregion
    //    contentVroomDBOut = vroomDB;
    //    return true;
    //}

    /// <summary>
    /// Check if the experience is downloaded and updated
    /// </summary>
    /// <param name="rowIndex"></param>
    /// <param name="needToUpdate"></param>
    /// <returns></returns>
    public bool CheckIfExperienceIsDownloaded(int rowIndex, out bool needToUpdate)
    {
        Debug.Log("AsiraResfulCheck1");
        if (CompareCloudAndLocalVersion(rowIndex))
        {
            Debug.Log("AsiraResfulCheck2");
            needToUpdate = false;
        }
        else
        {
            Debug.Log("AsiraResfulCheck3");
            needToUpdate = true;
        }
        Debug.Log("AsiraResfulCheck4");
        string filePath = sQLit.GetContent(rowIndex).FOLDER_PATH + "/" + sQLit.GetContent(rowIndex).APK_FILE_PATH;
        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            Debug.Log("Archivo existe");
            return true;
        }
        else
        {
            Debug.Log("archivo no existe");
            return false;
        }




    }

    /// <summary>
    /// Compare if the cloud version of the experience and the local version are the same
    /// </summary>
    /// <param name="rowIndex"> number of the row of the local database</param>
    /// <returns></returns>
    public bool CompareCloudAndLocalVersion(int rowIndex)
    {
        if (sQLit.GetContent(rowIndex).VERSION_LOCAL != null)
        {
            //If the local version is equal to the server version
            if (sQLit.GetContent(rowIndex).VERSION_LOCAL.Equals(sQLit.GetContent(rowIndex).VERSION_SERVER, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// Compare if the cloud content and the local content are the same, if not it deletes the local content
    /// from the database and the device
    /// </summary>
    /// <param name="cloudContent"></param>
    public void CompareCloudAndLocalContent(List<ContentVroom> cloudContent)
    {
        bool[] existsInAsira = new bool[sQLit.GetTableRowCount()];

        ArrayList listIdContent = new ArrayList();

        //Verify if the contents saved in Asira are the same ones saved in the local database
        for (int i = 0; i < cloudContent.Count; i++)
        {
            for (int j = 0; j < sQLit.GetTableRowCount(); j++)
            {
                if (cloudContent[i].ID_ELEMENT.Equals(sQLit.GetContent(j).ID_ELEMENT, StringComparison.OrdinalIgnoreCase))
                {
                    listIdContent.Add(sQLit.GetContent(j).Id);
                    existsInAsira[j] = true;
                    break;
                }
                else
                {
                    if (!existsInAsira[j])
                    {
                        existsInAsira[j] = false;
                    }
                }
            }
        }

        foreach (int i in listIdContent)
        {
            //sQLit.DeleteContentInTable(i);
        }

        for (int i = 0; i < existsInAsira.Length; i++)
        {
            if (!existsInAsira[i])
            {
                //sQLit.DeleteContentInTable(i);
            }
        }
    }

    #endregion
}
