using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class HotspotAnimations : MonoBehaviour
{
    private bool show;
    private bool active = false;
    public SetActiveojs panel;
    public UnityEvent onPanelOpened;
    private bool eventInvoked;
    public bool invokeEventOnce = true;

    public void ShowUIElements(bool show)
    {
        this.show = show;
        foreach (Animator item in transform.GetComponentsInChildren<Animator>())
        {
            if (show)
            {
                if (!item.GetBool("Open"))
                {
                    item.SetBool("Open", true);
                    item.SetBool("On", false);
                    Invoke("PanelOpened", 1.5f);
                    if (item.GetComponentInChildren<BoxCollider>())
                    {
                        item.GetComponentInChildren<BoxCollider>().enabled = true;
                    }

                    //active = true;
                }
            }
            else
            {
                print("Hola");
                item.SetBool("On", true);
                item.SetBool("Open", false);
                //panel.Hideobjs(show);
                if (item.GetComponentInChildren<BoxCollider>())
                {
                    item.GetComponentInChildren<BoxCollider>().enabled = false;
                }
                active = false;
            }

        }
    }

    public void PanelOpened()
    {
        panel.Hideobjs(false);
        if (!eventInvoked)
        {
            onPanelOpened.Invoke();
            if (invokeEventOnce)
                eventInvoked = true;
        }

        panel.GetComponent<Button>().onClick.Invoke();
    }
}
