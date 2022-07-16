using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveObject : MonoBehaviour
{

    public GameObject setobject;

    public void activeSET()
    {
        setobject.SetActive(true);
    }

    public void InactiveSet()
    {
        setobject.SetActive(false);
    }
}
