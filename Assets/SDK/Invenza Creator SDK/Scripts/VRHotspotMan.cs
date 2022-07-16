using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHotspotMan : MonoBehaviour
{

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;


    public void TurnPanelsOFF(int Panel)
    {

        switch (Panel)
        {
            case 1:
                Button1.transform.GetChild(0).gameObject.SetActive(true);
                Button2.transform.GetChild(0).gameObject.SetActive(false);
                Button3.transform.GetChild(0).gameObject.SetActive(false);
                Button4.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                Button1.transform.GetChild(0).gameObject.SetActive(false);
                Button2.transform.GetChild(0).gameObject.SetActive(true);
                Button3.transform.GetChild(0).gameObject.SetActive(false);
                Button4.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 3:
                Button1.transform.GetChild(0).gameObject.SetActive(false);
                Button2.transform.GetChild(0).gameObject.SetActive(false);
                Button3.transform.GetChild(0).gameObject.SetActive(true);
                Button4.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 4:
                Button1.transform.GetChild(0).gameObject.SetActive(false);
                Button2.transform.GetChild(0).gameObject.SetActive(false);
                Button3.transform.GetChild(0).gameObject.SetActive(false);
                Button4.transform.GetChild(0).gameObject.SetActive(true);
                break;

        }

    }




}
