using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.PXR;
using UnityEngine;

public class SystemService: MonoBehaviour
{
//    private void Awake()
//    {
//        PXR_System.InitSystemService(this.name);
//        PXR_System.BindSystemService();
//    }
    // Initializing and bind Service, the objectname refers to name of the object which is used to receive callback.

    private void Awake()
    {
        Invoke("Init", 1f);
    }
    // Initializing and bind Service, the objectname refers to name of the object which is used to receive callback.
    void Init()
    {
        PXR_System.InitSystemService(this.name);
        PXR_System.BindSystemService();
    }
    // Unbind the Service
    private void OnDestory()
    {
        PXR_Plugin.System.UPxr_UnBindSystemService();
        //PXR_Plugin.System.UnBindSystemService();
    }
    // Add 4 callback methods to allow corresponding callback can be received.
    private void BoolCallback(string value)
    {
        if (PXR_Plugin.System.BoolCallback != null) PXR_Plugin.System.BoolCallback(bool.Parse(value));
        PXR_Plugin.System.BoolCallback = null;
    }
    private void IntCallback(string value)
    {
        if (PXR_Plugin.System.IntCallback != null) PXR_Plugin.System.IntCallback(int.Parse(value));
        PXR_Plugin.System.IntCallback = null;
    }
    private void LongCallback(string value)
    {
        if (PXR_Plugin.System.LongCallback != null) PXR_Plugin.System.LongCallback(int.Parse(value));
        PXR_Plugin.System.LongCallback = null;
    }
    private void StringCallback(string value)
    {
        if (PXR_Plugin.System.StringCallback != null) PXR_Plugin.System.StringCallback(value);
        PXR_Plugin.System.StringCallback = null;
    }
    public void toBServiceBind(string s) { Debug.Log("Bind success."); }
}
