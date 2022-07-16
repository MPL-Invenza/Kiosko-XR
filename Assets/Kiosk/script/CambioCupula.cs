using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCupula : MonoBehaviour
{
    public MeshRenderer cupula;
    public bool ocultarCupula;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CambiarCupula()
    {
        if (!ocultarCupula)
            {
                if(!cupula.enabled) cupula.enabled = true;

                cupula.material = this.GetComponent<MeshRenderer>().material;
            }
        else
            cupula.enabled = false;
    }
}
