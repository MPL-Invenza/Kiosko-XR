using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandSimulator : MonoBehaviour
{
    private XRController xRController;
    public bool showController = false;
    public GameObject HandModelPrefab;
    public GameObject spawnedController;

    private GameObject spawnedHandModel;
    private Animator handAnimator;


    private void Start()
    {
        xRController = GetComponent<XRController>();
        TryInitialize();
    }

    private void TryInitialize()
    {
        if (xRController.inputDevice!=null && spawnedHandModel==null)
        {
            spawnedHandModel = Instantiate(HandModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!xRController)
        {
            xRController = GetComponent<XRController>();
        }
        //is Device connected?
        //if (!targetDevice.isValid)
        if (!xRController.inputDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (spawnedHandModel.activeInHierarchy)
            {
                UpdateHandAnimation();
            }


        }
    }

    private void UpdateHandAnimation()
    {
        //Trigger
        if (xRController.inputDevice.IsPressed(InputHelpers.Button.Trigger,out bool triggerPressed))
        {
            if (triggerPressed)
                handAnimator.SetFloat("Trigger", 1.0f);
            else
                handAnimator.SetFloat("Trigger", 0);
        }
        else
            handAnimator.SetFloat("Trigger", 0);

        //Touchpad
        if (xRController.inputDevice.IsPressed(InputHelpers.Button.Primary2DAxisClick,out bool touchPadPressed))
        {
            if (touchPadPressed)
                handAnimator.SetFloat("TouchPad", 1.0f);
            else
                handAnimator.SetFloat("TouchPad", 0);
        }
        else
            handAnimator.SetFloat("TouchPad", 0);
    }

    public void ChangeModel()
    {
        //Device Render
        if (spawnedHandModel.activeInHierarchy)
        {
            spawnedController.SetActive(true);
            spawnedHandModel.SetActive(false);
        }
        else
        {
            spawnedController.SetActive(false);
            spawnedHandModel.SetActive(true);
        }
    }

    public void ShowHand(bool show)
    {
        spawnedHandModel.SetActive(show);
    }

}
