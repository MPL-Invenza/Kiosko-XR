using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;
    public bool enableLocomotion { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (leftTeleportRay)
        {
            leftTeleportRay.gameObject.SetActive(CheckIfActivated(leftTeleportRay) && enableLocomotion);
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        //controller.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool isActivated);
        controller.inputDevice.IsPressed(teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }

}
