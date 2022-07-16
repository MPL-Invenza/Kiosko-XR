using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
//using Microsoft.Unity.VisualStudio.Editor;
using SQLite4Unity3d;
using System.IO.Compression;
using UnityEngine.Events;

public class AlmacenamientodeDatos
{
    private DownloadUtils downloadUtils;
    public AlmacenamientodeDatos()
    {

    }

    public void savecredential(string ID,string Password)
    {
        PlayerPrefs.SetString("ID",ID);
        PlayerPrefs.SetString("Password", Password);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("ID"));
        Debug.Log(PlayerPrefs.GetString("Password"));
    }
    public string readCredential() 
    {
        string ID = PlayerPrefs.GetString("ID");
        string Password = PlayerPrefs.GetString("Password");
        string credential = ID+","+Password;
        return credential;
    }

    public void logout() //////////////////////
    {
        PlayerPrefs.SetString("ID","");
        PlayerPrefs.SetString("Password","");
        PlayerPrefs.Save();
    }

}
