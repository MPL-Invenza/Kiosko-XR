using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImandeObjetos : MonoBehaviour
{
    public UnityEvent upperLimitReached;

    // Start is called before the first frame update
    public float x = 0;
    public float y = 0;
    public float z = 0;

    public float rx = 0;
    public float ry = 0;
    public float rz = 0;

    public string tag = " ";

    //void Start()
    //{

    //}

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            //////other.gameObject.transform.SetParent(transform, false);
            //other.gameObject.transform.localPosition = new Vector3(x, y, z);

            //other.gameObject.transform.localRotation = Quaternion.Euler(rx, ry, rz);
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.freezeRotation = true;
            upperLimitReached.Invoke();
        }

    }
}
