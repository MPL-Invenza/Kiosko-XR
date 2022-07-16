using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using Unity.XR.PXR;


/**
 * 
 * Nombre: DownloadUtils
 * 
 * 
 * Descripcion: clase que realiza las conexiones y solicitudes de descarga de los elementos que el dispositivo debe poder ver (apks, videos, imagenes, textos, entre otros)
 * 
 * 
 * **/

public class DownloadUtils : MonoBehaviour
{
    public Image image;
    public AudioSource audioSource;
    //AndroidHelper androidHelper = new AndroidHelper();
    HelperXR HelperXR = new HelperXR();
    SystemService SystemService = new SystemService();

    public string IP = "";
    public Text IPPanel;
    //public string url= "192.168.0.9";
    public string url;
    public string teachername;
    public string urlsocket;

    public string url_dir;

    public FormData data = new FormData();

    public GameObject conDocentes;

    public DataParent parent;
    public Text downloadtext, Status, usertext;

    public float file_size;
    public long getsize;

    public GameObject AsiraResful;

    private void Update()
    {
        //Debug.Log(url);
        //Debug.Log(url_dir);
    }



    /**
     * 
     * Nombre: GetRequest
     * 
     * Descripcion: corrutina que realiza un webrequests de unity a una direccion solicitada
     * 
     * Params: string endpoint, Action<UnityWebRequest> callback
     * 
     * Return: retorna un resultado de la consulta, este puede tener un objeto o no
     * 
     * */


    IEnumerator GetRequest(string endpoint, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(endpoint))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();

            callback(request);
        }
    }
    /**
     * 
     * Nombre: DownloadImage
     * 
     * Descripcion: metodo que descarga una imagen de una direccion solicitada
     * 
     * Params: string url
     * 
     * Return: retorna una imagen a una Texture2D de unity
     * 
     * */

    public void DownloadImage(string url)
    {
        StartCoroutine(ImageRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                // Get the texture out using a helper downloadhandler
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                // Save it into the Image UI's sprite
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }));
    }

    /**
     * 
     * Nombre: ImageRequest
     * 
     * Descripcion: corrutina que realiza un webrequest sobre imagen de una direccion solicitada
     * 
     * Params: string url, Action<UnityWebRequest> callback
     * 
     * Return: retorna un resultado sobre la consulta, este puede estar lleno o no
     * 
     * */
    IEnumerator ImageRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequestTexture.GetTexture(url))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }

    /**
    * 
    * Nombre: DownloadSound
    * 
    * Descripcion: metodo que descarga un sonido sobre una direccion solicitada
    * 
    * Params: string url
    * 
    * Return: retorna un sonido descargado
    * 
    * */
    public void DownloadSound(string url)
    {
        StartCoroutine(SoundRequest(url, (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else
            {
                // Get the sound out using a helper downloadhandler
                AudioClip clip = DownloadHandlerAudioClip.GetContent(req);
                // Load the clip into our audio source and play
                audioSource.Stop();
                audioSource.clip = clip;
                audioSource.Play();
            }
        }));
    }


    /**
    * 
    * Nombre: SoundRequest
    * 
    * Descripcion: corrutina que realiza un webrequest de unity sobre un sonido
    * 
    * Params: string url, Action<UnityWebRequest> callback
    * 
    * Return: retorna un resultado sobre la consulta, este puede estar poblado o no
    * 
    * */
    IEnumerator SoundRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS))
        {
            yield return req.SendWebRequest();
            callback(req);
        }
    }



    /**
    * 
    * Nombre: ByteArrayToFile
    * 
    * Descripcion: metodo que almacena un arreglo de bytes como un archivo, utilizado por los metodos de la clase create_element
    * 
    * Params: string fileName, byte[] byteArray
    * 
    * Return: un booleano con la respuesta si el archivo pudo ser descargado o no
    * 
    * */
    public bool ByteArrayToFile(string fileName, byte[] byteArray)
    {
        try
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(byteArray, 0, byteArray.Length);
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.Log("Exception caught in process: " + ex);
            return false;
        }
    }

    /**
    * 
    * Nombre: GetText
    * 
    * Descripcion: corrutina que realiza un webrequest sobre un elemento obtenido por el HDM dentro del json durante la conexion con Vroom
    * 
    * Params: String routeSource, string routeFile, Action<Boolean> resultCallBack
    * 
    * Return: retorna una respuesta sobre si el archivo existe dentro de la direccion consultada o no
    * 
    * */
    public IEnumerator GetText2(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        Debug.Log("routeSource: " + routeSource);
        //Debug.Log("routeFile: " + routeFile);
        float filezise = 0;
        StartCoroutine(GetFileSize(routeSource, (size) =>
        {
            Debug.Log("File Size: " + size);
            getsize = size;
            getsize = getsize / 1000;
        }
        )
        );
        using (UnityWebRequest www = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET))
        {
            var dh = new DownloadHandlerFile(routeFile, true);
            dh.removeFileOnAbort = true;
            www.downloadHandler = dh;

            var asyncOperation = www.SendWebRequest();

            while (!dh.isDone)
            {
                if (!downloadtext.gameObject.activeSelf)
                {
                    downloadtext.gameObject.SetActive(true);
                }
                filezise = (file_size + Mathf.FloorToInt(www.downloadedBytes)) / 1000;
                downloadtext.text = filezise.ToString() + "KB" + "/" + getsize.ToString() + " KB";
                yield return null;
            }
            #region textodescarga       

            if (!Status.gameObject.activeSelf)
            {
                Status.gameObject.SetActive(true);
                Status.text = "Descargando";
                Debug.Log("descargando");
            }

            #endregion
            //yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //byte[] results = www.downloadHandler.data;
                if (www.isDone)
                {
                    downloadtext.gameObject.SetActive(false);
                    //www.Dispose();
                    resultCallBack(true);
                    Debug.Log("Download saved to: " + routeFile);
                }
                else
                {
                    Debug.Log("no finalizo la descarga");
                    resultCallBack(false);
                }
            }
        }
    }


    public IEnumerator GetText2Asira(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        Debug.Log("routeSource: " + routeSource);
        //Debug.Log("routeFile: " + routeFile);
        float filezise = 0;
        StartCoroutine(GetFileSize(routeSource, (size) =>
        {
            Debug.Log("File Size: " + size);
            getsize = size;
            getsize = getsize / 1000;
        }
        )
        );
        using (UnityWebRequest www = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET))
        {
            var dh = new DownloadHandlerFile(routeFile, true);
            dh.removeFileOnAbort = true;
            www.downloadHandler = dh;
            var asyncOperation = www.SendWebRequest();
            #region textodescarga       

            //if (!Status.gameObject.activeSelf)
            {
                Status.gameObject.SetActive(true);
                Status.text = "Descargando";
                Debug.Log("descargando");
            }

            #endregion
            while (!dh.isDone)
            {
                if (!downloadtext.gameObject.activeSelf)
                {
                    downloadtext.gameObject.SetActive(true);
                }
                filezise = (file_size + Mathf.FloorToInt(www.downloadedBytes)) / 1000;
                //downloadtext.text = filezise.ToString() + "KB" + "/" + dh.data.Length.ToString() + " KB";
                downloadtext.text = filezise.ToString() + "KB" + "/" + getsize.ToString() + " KB";
                yield return null;
            }

            //yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //byte[] results = www.downloadHandler.data;
                if (www.isDone)
                {
                    downloadtext.gameObject.SetActive(false);
                    //www.Dispose();
                    resultCallBack(true);
                    Debug.Log("Download saved to: " + routeFile);
                }
                else
                {
                    Debug.Log("no finalizo la descarga");
                    resultCallBack(false);
                }
            }
        }

    }



    public IEnumerator GetText_2(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        Debug.Log("routeSource: " + routeSource);
        //Debug.Log("routeFile: " + routeFile);
        UnityWebRequest www = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET);
        float filezise = 0;

        StartCoroutine(GetFileSize(routeSource, (size) =>
        {
            Debug.Log("File Size: " + size);
            getsize = size;
            getsize = getsize / 1000;
        }
        )
        );

        using (var dh = new DownloadHandlerFile(routeFile, true))
        {
            dh.removeFileOnAbort = true;
            www.downloadHandler = dh;

            var asyncOperation = www.SendWebRequest();

            while (!dh.isDone)
            {
                if (!downloadtext.gameObject.activeSelf)
                {
                    downloadtext.gameObject.SetActive(true);
                }
                filezise = (file_size + Mathf.FloorToInt(www.downloadedBytes)) / 1000;
                downloadtext.text = filezise.ToString() + "KB" + "/" + getsize.ToString() + " KB";
                yield return null;
            }
        }


        #region textodescarga


        if (!Status.gameObject.activeSelf)
        {
            Status.gameObject.SetActive(true);
            usertext.text = "Sincronizando contenidos";
            Status.text = "Descargando";
            Debug.Log("descargando");
        }

        #endregion
        //yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            //byte[] results = www.downloadHandler.data;
            if (www.isDone)
            {
                downloadtext.gameObject.SetActive(false);
                www.Dispose();
                resultCallBack(true);
                Debug.Log("Download saved to: " + routeFile);
            }
            else
            {
                Debug.Log("no finalizo la descarga");
                resultCallBack(false);
            }
        }
    }

    public IEnumerator DownloadFile(string routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        var uwr = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET);
        //string path = Path.Combine(Application.persistentDataPath, "unity3d.html");
        uwr.downloadHandler = new DownloadHandlerFile(routeFile);


        yield return uwr.SendWebRequest();
        while (!uwr.isDone)
        {
            Debug.Log("debo mostrar el progreso");
            Debug.Log(uwr.downloadProgress);
            yield return null;
        }
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError(uwr.error);
            resultCallBack(false);
        }
        else
        {
            Debug.Log("File successfully downloaded and saved to " + routeFile);
            resultCallBack(true);
        }
    }

    public IEnumerator downfile(string routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        using (UnityWebRequest uwr = new UnityWebRequest(routeSource, UnityWebRequest.kHttpVerbGET))
        {
            uwr.SendWebRequest();

            int bundleSize = 0;

            using (UnityWebRequest request = UnityWebRequest.Head(routeSource))
            {
                yield return request.SendWebRequest();
                bundleSize = Int32.Parse(request.GetResponseHeader("Content-Length"));
            }
            while (!uwr.isDone)
            {
                Debug.Log((bundleSize != 0) ? (uwr.downloadedBytes / (float)bundleSize) : uwr.downloadProgress);
                yield return new WaitForEndOfFrame();
            }
            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
                resultCallBack(false);
            }
            else
            {
                Debug.Log("File successfully downloaded and saved to " + routeFile);
                resultCallBack(true);
            }
        }
    }


    /**
    * 
    * Nombre: GetAndroidInternalFilesDir
    * 
    * Descripcion: metodo que consulta dentro de la memoria del dispositivo android cual es la ruta para descargar los archivos traidos desde Vroom
    *   
    * 
    * Return: direccion dentro del dispositivo donde se pueden alojar los archivos
    * 
    * */
    public string GetAndroidInternalFilesDir()
    {
        string[] potentialDirectories = new string[]
        {
        "/mnt/sdcard",
        "/sdcard",
        "/storage/sdcard0",
        "/storage/sdcard1"
        };

        //string dir = "";

        if (Application.platform == RuntimePlatform.Android)
        {
            for (int i = 0; i < potentialDirectories.Length; i++)
            {
                if (Directory.Exists(potentialDirectories[i]))
                {
                    return potentialDirectories[i];
                    //dir = potentialDirectories[i];
                }
            }
        }
        return "";
        //return dir;
    }

    public IEnumerator GetText_3(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        string url = routeSource;

        //string vidSavePath = Path.Combine(Application.persistentDataPath, "Videos");
        //vidSavePath = Path.Combine(vidSavePath, "MyVideo.webm");

        //Create Directory if it does not exist
        /*if (!Directory.Exists(Path.GetDirectoryName(vidSavePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(vidSavePath));
        }*/

        var uwr = new UnityWebRequest(url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var dh = new DownloadHandlerFile(routeFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log(uwr.error);
            resultCallBack(false);
        }
        else
        {
            resultCallBack(true);
            Debug.Log("termine");
        }
    }




    public IEnumerator GetText(String routeSource, string routeFile, Action<Boolean> resultCallBack)
    {
        resultCallBack(true);
        yield return null;
    }


    /**
    * 
    * Nombre: verifyFilesExist
    * 
    * Descripcion: metodo que verifica si el archivo ya existe dentro del dispositivo
    * 
    * Params: string file
    * 
    * Return: retorna una respuesta sobre si el archivo existe dentro de la direccion consultada o no
    * 
    * */
    public Boolean verifyFilesExist(string file)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(file)) return true;
        }
        return false;
    }

    /**
    * 
    * Nombre: Get_data
    * 
    * Descripcion: Corrutina que da como respuesta si el arreglo de tipo Elements viene poblado desde Vroom
    * 
    * Params: Action<Element[]> resultCallBack
    * 
    * Return: retorna una respuesta sobre si el arreglo esta poblado o no
    * 
    * */
    //public string GetIP() {
    //    IP = IPPanel.text;
    //    return IP;
    //}
    public void GetIP()
    {
        IP = IPPanel.text;
        Debug.Log(IP+"**IP");
        SystemService.toBServiceBind("");
        Debug.Log("IPNeo3: "+HelperXR.GetIpServer());
        //Debug.Log("BateriaNeo3: " + HelperXR.GetBattery().TrimEnd('%')); 
        Debug.Log("MemoriaNeo3: " + HelperXR.GetStorage().TrimEnd('B').TrimEnd('M'));
        //Debug.Log("SN1Neo3: " + HelperXR.GetSerialNumber());
        Debug.Log("Paso");
    }

    public IEnumerator Get_data(Action<Element[]> resultCallBack)
    {
        //IP = IPPanel.text;

#if UNITY_EDITOR
        //if (!androidHelper.getIpServer().Equals(""))
        if (true)
        {
            WWWForm form = new WWWForm();
            //form.AddField("action", "get_data");

            data.id_devices = SystemInfo.deviceUniqueIdentifier;
            //data.user_id = conDocentes.GetComponent<ConexionesDocentes>().loader.teachername;
            //data.user_id = "inusermwc";//********************************************************************Quemado***********
            data.user_id = teachername;
            parent.dataContainer = data;

            string stdata = JsonUtility.ToJson(data);

            //form.AddField("user_id", conDocentes.GetComponent<ConexionesDocentes>().loader.teachername);
            //Debug.Log(stdata);
            url = IP;
            Debug.Log(url + "****");
            //string url = androidHelper.getIpServer() + "/vroom/scripts/get_elements.php";
            //url_dir = url + ":8000" + "/cotents_devices/" + androidHelper.getSerialNumber();

            //url = AsiraResful.GetComponent<AsiraRestful>().IPVroomPc();
            //url = "192.168.0.9";
            url_dir = url + ":8000" + "/cotents_devices_device";
            //url_dir= "192.168.1.101:8000" + "/cotents_devices_device";
            //Debug.Log("direccion de consulta:  " + url_dir);
            UnityWebRequest w = UnityWebRequest.Put("HTTP://" + url_dir, stdata);
            yield return w.SendWebRequest();
            Debug.Log("result" + w.downloadHandler.text);
            //if (!w.downloadHandler.text.Equals(""))
            if (!w.downloadHandler.text.Equals(""))
            {
                string jsonfix = fixJson(w.downloadHandler.text);
                Debug.Log(w.downloadHandler.text);
                Element[] elements = JsonHelper.FromJson<Element>(jsonfix);
                //Element elements = JsonUtility.FromJson<Element>(w.downloadHandler.text);
                resultCallBack(elements);
                //Debug.Log("UrlPkg: " + elements[0].ivr_urlpkgdl);
                //Debug.Log("UrlPkg: " + elements[1].ivr_urlpkgdl);
            }
            else
            {
                Debug.Log("get_data downLoad false");
                Element[] element = new Element[1];
                //Element element = new Element();
                // Debug.Log("get_data downLoad: "+element.Length);
                resultCallBack(element);
            }
            //Debug.Log(PXR_System.GetSDKVersion);
        }
        else
        {
            Element[] element = new Element[1];
            //Element element = new Element();
            resultCallBack(element);
        }

#elif UNITY_ANDROID
        //if (!androidHelper.getIpServer().Equals(""))
        
        Debug.Log("UnityAndroid123");
        //Debug.Log(HelperXR.GetIpServer());
        //Debug.Log(HelperXR.GetSerialNumber()+"**else**");
        if (!HelperXR.GetIpServer().Equals(""))
            if (true)
            {
                Debug.Log("Entre");
                Debug.Log(HelperXR.GetIpServer()+"**aquí**");
                //data.id_devices = androidHelper.getSerialNumber();
                data.id_devices = HelperXR.GetSerialNumber();
                Debug.Log(HelperXR.GetSerialNumber()+"**No, aquí**");
                //data.user_id = conDocentes.GetComponent<ConexionesDocentes>().loader.teachername;
                //data.user_id = "inusermwc";
                data.user_id = teachername;
                Debug.Log("Por fín");
                parent.dataContainer = data;

                string stdata = JsonUtility.ToJson(data);
                //url = "192.168.0.9";
                url = IP;
                Debug.Log(url + "****");
                url_dir = url + ":8000" + "/cotents_devices_device";
                Debug.Log("direccion de consulta:  " + url_dir);
                UnityWebRequest w = UnityWebRequest.Put("HTTP://" + url_dir, stdata);
                yield return w.SendWebRequest();
                Debug.Log("result" + w.downloadHandler.text);
                Debug.Log("stdata:"+stdata);
                //if (!w.downloadHandler.text.Equals(""))
                if (!w.downloadHandler.text.Equals(""))
                {
                    string jsonfix = fixJson(w.downloadHandler.text);
                    Debug.Log(w.downloadHandler.text);
                    Element[] elements = JsonHelper.FromJson<Element>(jsonfix);
                    resultCallBack(elements);
                }
                else
                {
                    Debug.Log("get_data downLoad false");
                    Element[] element = new Element[1];
                    resultCallBack(element);
                }

            }
            else
            {
                Debug.Log(HelperXR.GetSerialNumber()+"**else**");
                Element[] element = new Element[1];
                //Element element = new Element();
                resultCallBack(element);  
            }
            Debug.Log("Fin***********");
#endif

    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }

    public IEnumerator GetFileSize(string url, Action<long> resut)
    {
        UnityWebRequest uwr = UnityWebRequest.Head(url);
        yield return uwr.SendWebRequest();
        string size = uwr.GetResponseHeader("Content-Length");

        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.Log("Error While Getting Length: " + uwr.error);
            if (resut != null)
                resut(-1);
        }
        else
        {
            if (resut != null)
                resut(Convert.ToInt64(size));
        }
    }
}



/**
 * 
 * Nombre: FormData
 * 
 * Descripcion: clase que se utiliza para enviar la informacion al servidor sobre el serial del dispositivo y la identificacion del docente al cual se conecta
 * 
 * */

[Serializable]
public class FormData
{
    public string id_devices;
    public string user_id;
}


/**
 * 
 * Nombre: DataParent
 * 
 * Descripcion: clase que se utiliza para contener la informacion de tipo FormData, para que cumpla con el formato que requiere el servidor
 * 
 * */
[Serializable]
public class DataParent
{
    public FormData dataContainer;
}