using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


/**
* Name: VideoBehaviour
* 
* Description: clase que maneja el comportamiento de los videos
* Params:  N/A
* 
* Return: N/A
**/





public class VideoBehaviour : MonoBehaviour
{
    public VideoPlayer videoplayer;
    public Animator anim;

    /// <summary>
    /// metodo que permite pausar el video
    /// </summary>
    public void VideoPause()
    {
        videoplayer.Pause();
        anim.speed = 0;
    }

    /// <summary>
    /// metodo que permite iniciar el video
    /// </summary>
    public void VideoPlay()
    {
        videoplayer.Play();
        anim.speed = 1;
    }
}
