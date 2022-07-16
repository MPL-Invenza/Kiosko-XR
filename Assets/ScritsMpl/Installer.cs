using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * 
 * NAME: Installer
 *
 * Description: da informacion en el Log sobre la instalacion de las app en el HDM
 * 
 * 
 **/

public class Installer : MonoBehaviour
{
    public GameObject button;

    public Memory_test memtest;

    private void Start()
    {
        memtest = button.GetComponent<Memory_test>();
    }

    /**
 * 
 * NAME: InstallCallback
 *
 * Description: da informacion en el Log sobre la instalacion de las app en el HDM
 * 
 * Params: String s
 * 
 **/
    public void InstallCallback(string s)
    {
        //This function will be invoked after the installation completed.
        if (memtest.installed)
        {
            Debug.Log(s);
        }
        else
        {
            Debug.Log(s);
        }
        //s equals "success" if installation success.
        //s contains failed reason if the installation failed.
    }

    public void UninstallCallback(string s)
    {
        //This function will be invoked after the uninstallation completed.
        //if ()
        //{
        //    return s = "1";
        //}
       // else
       // {
        //    return s = "-1";
       // }
        //s equals "1" if uninstallation success.
        //s equals "-1" if uninstallation failed.
    }
}
