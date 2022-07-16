using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class RadialMenu : MonoBehaviour
{
    private Vector2 touchpadPosition;
    public GameObject point;
    [SerializeField]
    private XRController xRController;
    [SerializeField]
    private RectTransform canvas;
    private Vector2 canvasSize;
    [HideInInspector]
    public CanvasGroup canvasGroup;
    private bool onClickInvoked;

    public Transform headReference;
    public Transform handController;
    private float angleRadialMenu;
    private float distanceRadialMenu;

    public GameObject xRRig;

    private float xPos, yPos;

    //events
    public UnityEvent onRadialMenuShowed;
    public UnityEvent onRadialMenuHidden;
    private bool showRadialMenuEvent;
    private bool hideRadialMenuEvent;

    // Start is called before the first frame update
    void Start()
    {
        canvasSize = canvas.sizeDelta;
        canvasGroup = canvas.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

        if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out touchpadPosition))
        {
            xPos = touchpadPosition.x * ((canvasSize.x / 2));
            yPos = touchpadPosition.y * ((canvasSize.y / 2));
            point.transform.localPosition = new Vector2(xPos, yPos);
        }

        distanceRadialMenu = Vector3.Distance(headReference.localPosition, handController.localPosition);
        if (distanceRadialMenu < 0.4)
        {
            angleRadialMenu = Vector3.Angle(headReference.up, handController.forward);
            if (angleRadialMenu < 50)
            {
                ShowRadialMenu();
            }
        }
        else
        {
            HideRadialMenu();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Button>())
        {
            other.GetComponent<Button>().interactable = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Button>())
        {
            if (xRController.inputDevice.IsPressed(InputHelpers.Button.Trigger, out bool triggerPressed))
            {
                if (triggerPressed && !onClickInvoked)
                {
                    other.GetComponent<Button>().onClick.Invoke();
                    onClickInvoked = true;
                }

                if (!triggerPressed)
                {
                    onClickInvoked = false;
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Button>())
        {
            other.GetComponent<Button>().interactable = false;
            onClickInvoked = false;
        }
    }

    private void ShowRadialMenu()
    {
        if (!showRadialMenuEvent)
        {
            canvasGroup.alpha = 1 - ((angleRadialMenu - 10) / 50);
            //Enable radial menu interactions
            GetComponent<BoxCollider>().isTrigger = true;
            onRadialMenuShowed.Invoke();
            showRadialMenuEvent = true;
            hideRadialMenuEvent = false;
        }
    }

    private void HideRadialMenu()
    {
        if (!hideRadialMenuEvent)
        {
            canvasGroup.alpha = 0;
            //Disable radial menu interactions
            GetComponent<BoxCollider>().isTrigger = false;
            onRadialMenuShowed.Invoke();
            hideRadialMenuEvent = true;
            showRadialMenuEvent = false;
        }
    }
}
