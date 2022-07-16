using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * 
 * Nombre: AndroidHelper
 * 
 * Descripcion: clase que realiza las tareas concernientes a metodos de lenguaje java para adquirir o realizar tareas especificas realcionadas con el HDM
 * o cuando se requiere realizar algo a travez del sistema operativo.
 * 
 * 
 * 
 * **/
public class AndroidHelper : MonoBehaviour
{

    AndroidJavaObject ajo;
    AndroidJavaObject ActivityContext;
    string numServerIp = "100";

    /**
    * 
    * Nombre: silentInstallApp
    * 
    * Descripcion: metodo que instala de forma silenciosa las app dentro del dispositivo
    * **/
    //public Boolean silentInstallApp(string apkPath, string pkgname)
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.DeviceHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    //Ensure to invoke "init" function first before using all interfaces.
    //    ajo.Call("init", ActivityContext);
    //    //object[] args = new object[] { apkPath, pkgname };
    //    ajo.Call("silentInstall", apkPath, pkgname);
    //    // deviceHelper.Call("silentInstall", apkPath, pkgname);
    //    return true;
    //}


    //public Boolean silentUninstall(String packagename)
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.DeviceHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    ajo.Call("silentUninstall", packagename);
    //    return true;
    //}

    ///**
    // * 
    // * Nombre: getIpServer
    // * 
    // * Descripcion: obtiene la direccion ip del dispositivo de OS android
    // * 
    // * Return: retorna una direccion ip en formato string
    // * 
    // * */
    //public string getIpServer()
    //{
    //    string ipServer = "";


    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.WifiHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    string ipDevice = ajo.Call<string>("getWifiIpAddress");

    //    if (!ipDevice.Equals(""))
    //    {
    //        string[] ipDeviceSplit = ipDevice.Split('.');
    //        ipServer = ipDeviceSplit[0] + "." + ipDeviceSplit[1] + "." + ipDeviceSplit[2] + "." + numServerIp;
    //    }
    //    //Debug.Log(ipDevice + " Ipdevices " + ipServer);
    //    return ipServer;

    //}

    ///** 
    //* Nombre: silentInstallAppTest
    //* 
    //* Descripcion: instala un app a travez del sistema operativo y de forma silenciosa
    //* 
    //* Params: string apkPath, string pkgname
    //* */
    //public void silentInstallAppTest(string apkPath, string pkgname)
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.DeviceHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    //Ensure to invoke "init" function first before using all interfaces.
    //    ajo.Call("init", ActivityContext);
    //    object[] args = new object[] { apkPath, pkgname };
    //    ajo.Call("silentInstall", apkPath, pkgname);
    //    // deviceHelper.Call("silentInstall", apkPath, pkgname);

    //}

    ///**
    // * 
    // * Nombre: GetStorage
    // * 
    // * Descripcion: Captura a travez del sistema operativo Android la capacidad actual en memoria disponible del dispositivo
    // * 
    // * Return: retorna la capacidad del dispositivo en un string en formado GB
    // * 
    // * 
    // * */
    //public float GetStorage()
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.StorageHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    float freestorage = ajo.Call<float>("getStorageFreeSize");

    //    //Debug.Log(freestorage);

    //    return freestorage;
    //}

    ///**
    //* 
    //* Nombre: GetTotalStorage
    //* 
    //* Descripcion: Captura a travez del sistema operativo Android la capacidad total de memoria del dispositivo
    //* 
    //* Return: retorna la capacidad del dispositivo en un string en formado GB
    //* 
    //* 
    //* */
    //public float GetTotalStorage()
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.StorageHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    float freestorage = ajo.Call<float>("getStorageTotalSize");

    //    //Debug.Log(freestorage);

    //    return freestorage;
    //}
    ///**
    //* 
    //* Nombre: getSerialNumber
    //* 
    //* Descripcion: Captura a travez del sistema operativo Android el numerio de serie del dispositivo
    //* 
    //* Return: retorna la capacidad del dispositivo en un string en formado GB
    //* 
    //* 
    //* */
    //public string getSerialNumber()
    //{
    //    ajo = new AndroidJavaObject("com.picovr.androidhelper.DeviceHelper");
    //    ActivityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("init", ActivityContext);
    //    string serialnumber = ajo.Call<string>("getSN");


    //    return serialnumber;

    //}

    //public void lanzarvideo()
    //{
    //    Debug.Log("empiezo el metodo");
    //    string configPath = "/storage/emulated/0/Download/config.txt";
    //    AndroidJavaObject ajo = new AndroidJavaObject("com.picovr.launchwebvr.LaunchWebVRClass");
    //    AndroidJavaObject context = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    //    ajo.Call("launch", context, configPath);
    //    //Debug.Log("pase por el metodo");
    //}

    public void lanzarApp(string pkgname)
    {
        //Initialize an AndroidJavaObject with the class name of the interface, 
        //e.g., DeviceHelper, StorageHelper, BlueToothHelper, WifiHelper, PowerManagerHelper.
        AndroidJavaObject deviceHelper = new AndroidJavaObject("com.picovr.androidhelper.DeviceHelper");
        //Get current activity
        AndroidJavaObject activityContext = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
        .GetStatic<AndroidJavaObject>("currentActivity");
        //Must call "init" method before call other interface.
        deviceHelper.Call("init", activityContext);
        //Call the interface.
        deviceHelper.Call("goToApp", pkgname);
    }


}
