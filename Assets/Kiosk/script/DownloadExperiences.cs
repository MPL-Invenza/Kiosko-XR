using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DownloadExperiences : MonoBehaviour
{
    public InfoPanel InfoPanel;
    public AsiraRestful asiraRestful;
    // Start is called before the first frame update
    void Start()
    {
        asiraRestful = FindObjectOfType<AsiraRestful>();
        //childCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Download()
    {
        asiraRestful.DownloadContentCoroutine(InfoPanel.activeitem.GetComponent<HomeItem>().elementdescription.id_content_asira);
    }
}
