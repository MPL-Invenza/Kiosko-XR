using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dipstick : MonoBehaviour
{
    public Vector3 positionInHand;
    private Vector3 lastPosition;
    private bool inHand;
    private Transform originalParent;
    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveDipstickToHand(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = positionInHand;
        GetComponent<XRHandleObject>().enabled = false;
        inHand = true;
    }

    public void DropDipstick()
    {
        if (inHand)
        {
            transform.SetParent(originalParent);
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = new Vector3(0, 0, 0.28f);
            GetComponent<XRHandleObject>().enabled = true;
            inHand = false;
        }
    }
}
