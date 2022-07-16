using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class webglvideo : MonoBehaviour
{
    public VideoPlayer videop;
    public string videoname;
    public bool ambient;

    // Start is called before the first frame update
    void Start()
    {
        videop.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoname);
    }

    private void Update()
    {
        if (ambient)
        {
            videop.Play();
        }
    }
}
