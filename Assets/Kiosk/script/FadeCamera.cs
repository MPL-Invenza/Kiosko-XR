using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeCamera : MonoBehaviour
{
    public Animator[] imagenAnimator;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        imagenAnimator[0].Play("ShowCamera");
        imagenAnimator[1].Play("ShowCamera");
    }

}
