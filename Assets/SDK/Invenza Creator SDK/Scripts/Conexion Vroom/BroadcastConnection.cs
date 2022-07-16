using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Linq;
using UnityEngine.UI;

/**
 * 
 * Nombre: BroadcastConnection
 * 
 * Description: Realiza la conexion con el servidor para solicitar la lista de ip's 
 * disponibles para realizar conexion
 * 
 *
 * 
 **/
public class BroadcastConnection : MonoBehaviour
{
    public static bool started = false;

    public static string returnData;

    public static List<Docente> listaentrante = new List<Docente>();

    public GameObject ElementoDocente;

    //static ColorConsola consola;

    public static ConexionesDocentes conex;

    Thread t = null;

    public static Dropdown dropdownDocentes;

    public static Docente docente;

    //public Image statusConexion;
    
   // public Text userInfo;

    public string conecting = "Conectando con VRoom";

    public string disconected = "Detenida Conexión con Vroom";

    //private static DebugFile debugFile;
    public static byte status = 0;

    private void Start()
    {
        conex = ElementoDocente.GetComponent<ConexionesDocentes>();

        dropdownDocentes = ElementoDocente.GetComponent<ConexionesDocentes>().dropdownDocentes;

        //debugFile = this.gameObject.GetComponent<DebugFile>();

        //consola = this.gameObject.GetComponent<ColorConsola>();
    }
    /**
     * 
     * Nombre: connecttoBroadcast
     * 
     * 
     * Descripcion: inicializa el hilo por el cual se va a conectar la aplicacion al servidor
     * 
     * **/
    public void connecttoBroadcast()
    {

        started = !started;

        if (started)
        {
            //userInfo.text = conecting;
            //connectionty.color = Color.blue;
            t = new Thread(new ThreadStart(Threadhandler));
            //Debug.Log(started);
            t.Start();
        }
        else
        {
            //userInfo.text = disconected;
            //connectionty.color = Color.black;
            t.Abort();
            t = null;
        }

    }

    /**
     * 
     * Nombre: Threadhandler
     * 
     * Descripcion: Abre un hilo que se mantiene vivo indefinidamente para poder recibir la lista 
     * de direcciones disponibles para que el HMD se conecte
     * 
     * **/
    public static void Threadhandler()
    {
        UdpClient listener = new UdpClient(5005);
        int i = 0;
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 5005);
        Debug.Log(groupEP);
        //debugFile.WriteToFile("IP buscada: " + groupEP.ToString());
        try
        {
            while (true)
            {
                Debug.Log("entro a conectarme");
                byte[] bytes = listener.Receive(ref groupEP);
                Debug.Log(bytes);
                //debugFile.WriteToFile(IPAddress.Any.ToString());
                returnData = Encoding.UTF8.GetString(bytes);
                //debugFile.WriteToFile("datos recibidos desde la ip: " + returnData);
                Debug.Log(returnData);
                docente = JsonUtility.FromJson<Docente>(returnData);
                if (!listaentrante.Any<Docente>(x => x.teacherName == docente.teacherName))
                {
                    listaentrante.Add(docente);
                    conex.docentesactuales = listaentrante;
                    conex.PopulateDropdown(dropdownDocentes, listaentrante);
                }
                UnityEngine.Debug.Log(returnData);
                Thread.Sleep(5000);
            }
        }
        catch (SocketException e)
        {
            UnityEngine.Debug.Log(e);
            Debug.Log("algo fallo");
            //throw;
            //consola.CambiarColorConsola(status = 2);
        }
        finally
        {
            listener.Close();
        }
    }
    /**
     * 
     * Descripcion: se genera un objeto grafico que permite de forma visual identificar
     * cuando hay una ip disponible para realizar conexion
     * 
     * 
     * **/

    private void Update()
    {
        if (returnData != null)
        {
            //statusConexion.color = Color.green;
            //userInfo.text = "Conectado.  Selecciona el servidor";
        }
        else
        {
            //statusConexion.color = Color.red;
        }
    }
}
