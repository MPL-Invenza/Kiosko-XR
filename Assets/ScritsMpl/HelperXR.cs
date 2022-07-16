using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
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
public class HelperXR : MonoBehaviour
{        
    /*
    * 
    * Nombre: getIpServer
    * 
    * Descripcion: obtiene la direccion ip del dispositivo de OS android
    * 
    * Return: retorna una direccion ip en formato string
    * 
    * */
    public string GetIpServer()
    {

        return PXR_Plugin.System.UPxr_StateGetDeviceInfo(SystemInfoEnum.DEVICE_IP);
    }

    /** 
    * Nombre: silentInstallAppTest
    * 
    * Descripcion: instala un app a travez del sistema operativo y de forma silenciosa
    * 
    * Params: string apkPath, string pkgname
    * */
    public void SilentInstallAppTest(string apkPath, string pkgname)
    {

    }

   /*
    * 
    * Nombre: getSerialNumber
    * 
    * Descripcion: Captura a travez del sistema operativo Android el numerio de serie del dispositivo
    * 
    * Return: retorna la capacidad del dispositivo en un string en formado GB
    * 
    * 
    * */
    public string GetSerialNumber()
    {
        //return PXR_Plugin.System.UPxr_StateGetDeviceInfo(SystemInfoEnum.EQUIPMENT_SN);
        return PXR_Plugin.System.UPxr_GetDeviceSN();

    }

    public string GetStorage()
    {
        return PXR_Plugin.System.UPxr_StateGetDeviceInfo(SystemInfoEnum.INTERNAL_STORAGE_SPACE_OF_THE_DEVICE);
    }

    public string GetTotalStorage()
    {
        return PXR_Plugin.System.UPxr_StateGetDeviceInfo(SystemInfoEnum.INTERNAL_STORAGE_SPACE_OF_THE_DEVICE);
    }

    Action<int> Callback=a =>Debug.Log("ActionCallback");
    //public delegate bool CallBack(int hwnd, int lParam);
    //CallBack myCallBack = new CallBack(HelperXR);
    public Boolean silentUninstall(String packagename) {
        PXR_Plugin.System.UPxr_ControlAPPManger(PackageControlEnum.PACKAGE_SILENCE_UNINSTALL, packagename, Callback);
        Debug.Log("UnInstall: " + Callback);
        return true;
    }

    public Boolean silentInstall(String packagename)
    {
        PXR_Plugin.System.UPxr_ControlAPPManger(PackageControlEnum.PACKAGE_SILENCE_INSTALL, packagename, (int res) => { Debug.Log("Esto devuelve res:"+res); });
        Debug.Log("Install*********: "+Callback);
        return true;
    }


    public string GetBattery()
    {
        return PXR_Plugin.System.UPxr_StateGetDeviceInfo(SystemInfoEnum.ELECTRIC_QUANTITY);
    }

}
