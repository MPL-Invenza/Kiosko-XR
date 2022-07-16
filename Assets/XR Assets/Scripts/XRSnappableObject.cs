using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class XRSnappableObject : MonoBehaviour
{
    public byte snapID;
    [HideInInspector]
    public byte onSnapID;
    public bool isSnappable { get; set; }
    public bool enableIsSnappable;
    public bool enableOnSnapDrop;
    public bool addDefaultRefusedSnapFunction = true;
    public bool addDefaultSnappedunction = true;
    public bool changeLayer = true;
    public UnityEvent onSnapped;
    public UnityEvent onRefusedSnap;

    private void Start()
    {
        if (addDefaultSnappedunction) onSnapped.AddListener(delegate { SnappedFunctions(); });
        if (addDefaultRefusedSnapFunction) onRefusedSnap.AddListener(delegate { RefusedSnapFunctions(); });

        if (enableIsSnappable) isSnappable = true;
        if (enableOnSnapDrop) onSnapped.AddListener(delegate { GetComponent<XRGrabObject>().Drop(); });
    }

    public void SnappedFunctions()
    {
        if (!GetComponent<SpringJoint>())
            Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<XRGrabObject>());
        //Destroy(GetComponent<XRSimpleInteractable>());
        //Destroy(GetComponent<InteractableObject>());
        if (changeLayer) gameObject.layer = 2;
        //Destroy(this);
    }

    private void RefusedSnapFunctions()
    {
        if (GetComponent<XRGrabObject>())
        {
            GetComponent<XRGrabObject>().ResetTransform();
        }
    }


}
