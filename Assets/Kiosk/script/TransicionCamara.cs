using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class TransicionCamara : MonoBehaviour
{
    public CinemachineDollyCart nave;
    private CinemachineDollyCart camaraCM;
    public Animator[] imagenAnimator;
    private bool cambioEscena;

    // Start is called before the first frame update
    void Start()
    {
        camaraCM = GetComponent<CinemachineDollyCart>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nave.m_Position >= 550f)
        {
            nave.m_Speed = 225;
        }
        if (nave.m_Position >= 800f)
        {
            nave.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            camaraCM.m_Speed = 30;
        }

        if (camaraCM.m_Position >= 160)
        {
            if (!cambioEscena)
            {
                imagenAnimator[0].Play("FadeCamera");
                imagenAnimator[1].Play("FadeCamera");
                Invoke("CambioEscena", 2);
                cambioEscena = true;
            }

        }
    }

    public void CambioEscena()
    {
        SceneManager.LoadScene("Scene");
        imagenAnimator[0].Play("FadeCamera");
        imagenAnimator[1].Play("FadeCamera");
        Invoke("CambioEscena", 2);
    }
}
