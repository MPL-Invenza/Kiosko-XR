using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flechas : MonoBehaviour
{
    int Nflecha = 0;
    public void ActiveFlecha() { 
    transform.GetChild(Nflecha).gameObject.SetActive(true);
    }
    public void DesactiveFlecha() {
        transform.GetChild(Nflecha).gameObject.SetActive(false);
        Nflecha += 1;
    }
}
