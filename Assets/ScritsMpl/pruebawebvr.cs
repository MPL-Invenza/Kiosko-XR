using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pruebawebvr : MonoBehaviour
{
    public GameObject manager;

    //public AndroidHelper Helper;
    public HelperXR Helper;

    public void Start()
    {
        //Helper = manager.GetComponent<AndroidHelper>();
        Helper = manager.GetComponent<HelperXR>();
    }

    public void ClickB()
    {        
        //Helper.lanzarvideo();
        Debug.Log("debo lanzar el video");
    }
}
