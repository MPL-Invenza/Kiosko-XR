using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.WebSockets;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using UnityEngine.UI;

/**
 * NAME: WebSocketConnection
 * 
 * PARAM: N/A
 * 
 * RETURN: Conexion con el servicio Kiosk
 **/

public class WebSocketConnection : MonoBehaviour
{
    public GameObject manager;
    public static DownloadUtils downutils;
    public Message deviceinfo = new Message();

    public Messageholder holder = new Messageholder();

    //public AndroidHelper helper = new AndroidHelper();

    public HelperXR HelperXR = new HelperXR();

    private float memoria;
    private float memoria_total;
    private float bateria;

    /**
     * Name: Receive
     * 
     * busca y realiza la conexion con el servicio de Vroom    
     **/
    public static async Task Receive(string message)
    {
        if (!downutils.url.Equals("0.0.0.0"))
        {
            using (ClientWebSocket ws = new ClientWebSocket())
            {
                try
                {
                    byte[] sendBytes = Encoding.UTF8.GetBytes(message);
                    var sendBuffer = new ArraySegment<byte>(sendBytes);
                    Debug.Log(downutils.teachername);
                    Uri serverUri = new Uri("ws://" + downutils.url + ":8000/ws/devices/" + downutils.teachername + "/");
                    downutils.urlsocket = serverUri.ToString();
                    Debug.Log(serverUri);
                    Debug.Log("Estoy en WebSocket");
                    //Uri serverUri = new Uri("ws://127.0.0.1:8000/ws/devices/demoInvenza/");
                    await ws.ConnectAsync(serverUri, CancellationToken.None);
                    await ws.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    //Debug.Log(downutils.url);
                }
                catch (Exception x)
                {
                    Debug.LogError("la conexion fallo" + x);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        downutils = manager.GetComponent<DownloadUtils>();
    }

    /**
     * Name: sendinfo
     * 
     * envia la informacion del dispositivo a Vroom    
     **/
    public void sendinfo()
    {
        //Debug.Log("hola");
#if UNITY_EDITOR
        deviceinfo.id = SystemInfo.deviceUniqueIdentifier;
        deviceinfo.name = SystemInfo.deviceName;

        memoria = 30f;
        deviceinfo.memory = memoria.ToString("F2");

        memoria_total = 30f * 100 / 50f;

        deviceinfo.memory_p = memoria_total.ToString();

        bateria = SystemInfo.batteryLevel * 100;

        deviceinfo.drums = "100";

        holder.message = deviceinfo;
#elif UNITY_ANDROID
        Debug.Log("WebSocket enviandoInfo");
        deviceinfo.id = HelperXR.GetSerialNumber();
        deviceinfo.name = HelperXR.GetSerialNumber();

        //memoria = float.Parse(HelperXR.GetStorage().TrimEnd('B').TrimEnd('M'));
        memoria = 30f;
        deviceinfo.memory = memoria.ToString("F2");
        //memoria_total = HelperXR.GetStorage() * 100 / HelperXR.GetTotalStorage();
        //memoria_total = memoria * 100 / 256000f;
        memoria_total = memoria * 100 / 60f;

        deviceinfo.memory_p = memoria_total.ToString();

        //bateria = SystemInfo.batteryLevel * 100;
        //deviceinfo.drums = bateria.ToString();

        deviceinfo.drums= HelperXR.GetBattery().TrimEnd('%');
        Debug.Log(deviceinfo);
        holder.message = deviceinfo;
#endif
        if (ConexionesDocentes.connected)
        {
            Thread t = new Thread(new ThreadStart(metodo));
            t.Start();
        }
    }

    /**
     * Name: TurntoJson
     * 
     * convierte la informacion a json
     * 
     * PARAM: un objeto de tipo Messageholder
     * 
     * RETURN: el mensaje en formato json
     * 
     **/
    public string TurntoJson(Messageholder messagetoSend)
    {
        string sendmessage = JsonUtility.ToJson(messagetoSend);
        Debug.Log(sendmessage);
        return sendmessage;
    }
    /**
     * Name: startThread
     * 
     * comienza el hilo de comunicacion entre el HMD y el Vroom
     * 
     * PARAM:
     * 
     * RETURN: N/A
     * 
     **/

    public IEnumerator startThread()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Receive(TurntoJson(holder));
    }
    /**
     * Name: metodo
     * 
     * comienza el hilo de comunicacion entre el HMD y el Vroom
     * 
     * PARAM:
     * 
     * RETURN: N/A
     * 
     **/

    void metodo()
    {
        Receive(TurntoJson(holder));
    }
}
