using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsiraAnimationManager : MonoBehaviour
{
    public Transform rigPiernas;
    public Animator rigAnimator;
    private bool girarCabeza=true;
    // Start is called before the first frame update
    void Start()
    {
        rigPiernas.Translate(0,-22.2f,0);
        rigAnimator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (girarCabeza)
        {
            StartCoroutine("MirarUsuario");
            girarCabeza = false;
        }
    }

    public IEnumerator MirarUsuario()
    {
        yield return new WaitForSecondsRealtime(20);
        //print("Hola");
        rigAnimator.Play("GiroCabezaAct");
        yield return new WaitForSecondsRealtime(5);
        rigAnimator.Play("GiroCabezaDes");
        girarCabeza = true;
    }
}
