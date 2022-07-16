using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public enum Interaction{
    None,Grab,Handle
}

public class XRRayInteraction : MonoBehaviour
{
    private XRRayInteractor xRRay;
    private XRController xRController;
    private GameObject selectedObject;
    private bool interact;

    private Interaction currentInteraction;
    //Handle
    private Vector3 currentValue;
    private Vector3 lastValue;
    private float delta;

    //Grab
    private bool isParent;
    private bool touchDown;

    private void Start()
    {
        xRRay = GetComponent<XRRayInteractor>();
        xRController = GetComponent<XRController>();
    }

    public void GetSelectedObject()
    {
        if (xRRay.GetCurrentRaycastHit(out RaycastHit rayCast))
        {
            selectedObject = rayCast.transform.gameObject;
            interact = true;
        }
    }

    public void DeleteSelectedObject()
    {
        selectedObject = null;
        interact = false;
        CleanVariables();
    }

    private void CleanVariables()
    {
        currentValue = Vector3.zero;
        lastValue = Vector3.zero;
        delta = 0;
        currentInteraction = Interaction.None;
        isParent = false;
    }

    private void GetCurrentInteraction()
    {
        if (selectedObject!=null)
        {
            if (selectedObject.GetComponent<XRHandleObject>())
                currentInteraction = Interaction.Handle;
            else if (selectedObject.GetComponent<XRGrabObject>())
                currentInteraction = Interaction.Grab;
            else currentInteraction = Interaction.None;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentInteraction==Interaction.None)
        {
            GetCurrentInteraction();
        }
        if (interact)
        {
            Interact();
        }
    }

    private void Interact()
    {
        switch (currentInteraction)
        {
            case Interaction.Grab:
                Grab();
                break;
            case Interaction.Handle:
                Handle();
                break;
        }
    }

    private void Handle()
    {
        currentValue = transform.localEulerAngles;
        foreach (XRHandleObject handle in selectedObject.GetComponents<XRHandleObject>())
        {

            if (lastValue != Vector3.zero)
            {
                switch (handle.sourceMovementAxis)
                {
                    case Axis.X:
                        delta = lastValue.x - currentValue.x;
                        break;
                    case Axis.Y:
                        delta = lastValue.y - currentValue.y;
                        break;
                    case Axis.Z:
                        delta = lastValue.z - currentValue.z;
                        break;
                }
            }


            if (delta>1)
                delta = 1;
            else if(delta<-1)
                delta = -1;

            handle.Handle(delta);

        }

        lastValue = transform.localEulerAngles;
    }

    private void Grab()
    {
        if (!isParent)
        {
            selectedObject.transform.SetParent(transform);
            isParent = true;
            selectedObject.GetComponent<XRGrabObject>().FirstGrab();
        }
        else
        {
            if (xRController.inputDevice.IsPressed(InputHelpers.Button.Primary2DAxisClick,out bool TouchpadClick))
            {
                if (TouchpadClick)
                {
                    if (xRController.inputDevice.IsPressed(InputHelpers.Button.PrimaryAxis2DUp,out bool touchPadUp))
                    {
                        if(touchPadUp)
                            selectedObject.GetComponent<XRGrabObject>().MoveForward(1);

                    }

                    if (xRController.inputDevice.IsPressed(InputHelpers.Button.PrimaryAxis2DDown,out bool touchPadDown))
                    {
                        if(touchPadDown)
                            selectedObject.GetComponent<XRGrabObject>().MoveForward(-1);
                    }
                    touchDown = true;
                }

            }

        }

    }
}
