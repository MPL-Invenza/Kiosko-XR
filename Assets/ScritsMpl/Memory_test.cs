using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * Nombre: Memory_test
 * 
 * Descripcion:  clase que Captura la informacion del dispositivo utilizando los objeto de tipo Message para enviarla por medio de websocket a Vroom
 * 
 * 
 **/

public class Memory_test : MonoBehaviour
{

    public GameObject manager;

    //public AndroidHelper helper = new AndroidHelper();
    public HelperXR helper = new HelperXR();

    public static Message deviceinfo;

    public bool installed;

    float memoria;
    float bateria;
    float memoria_total;

    public List<string> screenshots = new List<string>();

    private WebSocketConnection socket;
    int count;

    private void Start()
    {
        //helper = manager.GetComponent<AndroidHelper>();
        helper = manager.GetComponent<HelperXR>();

        manager.GetComponent<ScreenCapture>().InvokeRepeating("TimedScreen", 1f, 1f);
    }


    /**
     * 
     * Nombre: Update
     * 
     * Descripcion: Cada cuadro, el metodo captura la informacion del dispositivo para enviarlo por medio de una conexion por websocket
     * 
     * */
    private void Update()
    {
        deviceinfo = new Message();

#if UNITY_EDITOR
        deviceinfo.id = SystemInfo.deviceUniqueIdentifier;
        deviceinfo.name = SystemInfo.deviceName;

        memoria = 30f;
        deviceinfo.memory = memoria.ToString("F2");

        memoria_total = 30f * 100 / 50f;

        deviceinfo.memory_p = memoria_total.ToString();

        bateria = SystemInfo.batteryLevel * 100;

        deviceinfo.drums = "100";
#elif UNITY_ANDROID
        Debug.Log("MemoryTest");
        deviceinfo.id = helper.GetSerialNumber();
        deviceinfo.name = helper.GetSerialNumber();

        //memoria = helper.GetStorage();
        memoria = 30f;
        deviceinfo.memory = memoria.ToString("F2");

        //memoria_total = helper.GetStorage() * 100 / helper.GetTotalStorage();
        memoria_total = 30f * 100 / 50f;

        deviceinfo.memory_p = memoria_total.ToString();

        Debug.Log(SystemInfo.batteryLevel * 100);
        bateria = SystemInfo.batteryLevel * 100;

        deviceinfo.drums = bateria.ToString();
#endif       
        Debug.Log("MemoryTest"+helper.GetIpServer());
    }


    /**
     * 
     * Nombre: takescreenshot
     * 
     * Descripcion: toma un screenshot haciendo uso de la clase ScreenCapture
     * 
     * */
    public void takescreenshot()
    {
        screenshots.Add(manager.GetComponent<ScreenCapture>().GetScreenshot());
    }


}