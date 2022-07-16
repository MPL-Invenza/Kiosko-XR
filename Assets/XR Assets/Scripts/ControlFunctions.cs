using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class ControlFunctions : MonoBehaviour
{
    private XRController xRController;
    public GameObject GUI;
    private bool guiState=false;

    // Start is called before the first frame update
    void Start()
    {
        xRController = GetComponent<XRController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.menuButton,out bool menuButtonPressed))
        {
            if (menuButtonPressed && !guiState)
            {
                if(GUI.activeInHierarchy)
                    GUI.SetActive(false);
                else
                    GUI.SetActive(true);
                guiState = true;
            }

            if (!menuButtonPressed)
            {
                guiState = false;
            }
        }
    }


}
