using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{

    private Touch initTouch = new Touch();
    public Camera Camera;
    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 1f;
    public float dir = -1;
    void Start()
    {
        origRot = Camera.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initTouch = touch;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = initTouch.position.y - touch.position.y;
                float deltaY = initTouch.position.x - touch.position.x;
                rotX -= deltaX * Time.deltaTime * rotSpeed * dir;
                rotY += deltaY * Time.deltaTime * rotSpeed * dir;
                Mathf.Clamp(rotX, -45f, 45f);
                Camera.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
            }
        }
    }

}