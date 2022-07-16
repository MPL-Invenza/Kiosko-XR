using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using BestHTTP;
using ImageLoaderPlugin;

public class HomeItem : MonoBehaviour
{

    public BaseModel model;
    public int index;
    public RawImage image;
    public Text titleText;
    //private create_element create_; 
    public BaseModel model2;
    public Element elementdescription;
    public GameObject infocanvas;
    public Text textomodel;
    // Use this for initialization
    public Sprite poster;
    public int child;

    void Start()
    {
        ImageUtils.onTextureCallback += OnTextureCallback;
        /*GetComponent<Button>().onClick.AddListener(delegate ()
        {
            LauncherUtils.DoSomething(this.model);
        });*/

        GetComponent<Button>().onClick.AddListener(delegate ()
        {
            activateinfo();
        });
    }

    public void activateinfo()
    {
        Debug.Log("activeinfo");
        infocanvas = GameObject.Find("CanvasIzquirdo2");
        //Debug.Log(infocanvas.name);        
        //textomodel = GameObject.Find("textodebug").GetComponent<Text>();

        infocanvas.transform.GetChild(3).gameObject.SetActive(true);
        //textomodel.text = elementdescription.name.ToString();

        /*if (GameObject.FindGameObjectWithTag("Active Item"))
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Active Item");

            temp.tag = null;
        }
        gameObject.tag = "Active Item";*/
        InfoPanel.activeitem = this.gameObject;
    }

    public void initData()
    {
        OnItemDataChangeCallback(model2);
    }

    private void OnGUI()
    {
        initData();
    }



    private void OnItemDataChangeCallback(BaseModel model)
    {
        if (model == null)
        {
            return;
        }
        if (this.model != null && model.Equals(this.model))
        {
            return;
        }
        this.model = model;
        SetUI();
    }

    /*
	 * 设置UI 
	 */
    private void SetUI()
    {
        LogUtils.Log(this.model.title);
        titleText.text = this.model.title;
        LoadImage();
    }

    /*
	* 加载图片
	*/
    public void LoadImage()
    {
        string imageUrl = this.model.imageUrl;
        if (string.IsNullOrEmpty(imageUrl))
        {
            LogUtils.LogError(this.gameObject.name + " image url is null!");
            return;
        }
        if (imageUrl.StartsWith("poster"))
        {
            LogUtils.Log(this.model.title + " load image from <poster>");
            image.texture = Resources.Load(imageUrl) as Texture;
        }
        else if (imageUrl.Contains("sdcard"))
        {
            Debug.Log("image path sdcard: " + imageUrl);
            LogUtils.Log(this.model.title + " load image from <sdcard>");
            new LocalImageLoader(imageUrl, true, OnLocalTextureCallback).StartLoad();

        }
        else
        {
            //string imageLocalPath = LauncherUtils.GetImageCachePath() + GetImageUrl().GetHashCode();
            string imageLocalPath = model.imageUrl;
            if (!File.Exists(imageLocalPath))
            {
                LogUtils.Log(this.model.title + " load image from <network>");
                ImageUtils.Instance.LoadTexture(GetImageUrl());
            }
            else
            {
                Debug.Log("image path cache: " + imageLocalPath);
                LogUtils.Log(this.model.title + " load image from <cache>");
                new LocalImageLoader(imageLocalPath, true, OnLocalTextureCallback).StartLoad();
            }
        }
    }

    /*
	* 网络图片下载回调
	*/
    private void OnTextureCallback(HTTPRequest req, HTTPResponse resp)
    {
        if (req == null || this.model == null)
        {
            return;
        }
        if (!req.Uri.ToString().Equals(GetImageUrl()))
        {
            return;
        }
        Texture2D texture = resp.DataAsTexture2D;
        if (texture.width <= 8 || texture.height <= 8)
        {
            LogUtils.LogError(this.model.title + " image width or height less than 8!");
            return;
        }
        image.texture = texture;
    }

    /*
	* 本地图片加载回调
	*/
    private void OnLocalTextureCallback(LocalImageLoader localImageLoader, LocalImageLoaderResponse response)
    {
        image.texture = response.DataAsTexture2D;
    }

    void OnDestroy()
    {
        ImageUtils.onTextureCallback -= OnTextureCallback;
    }

    /*
	 * 获取图片url
	*/
    private string GetImageUrl()
    {
        if (index == 0)
            return this.model.imageUrl + "?x-oss-process=image/resize,w_434,h_210";
        return this.model.imageUrl + "?x-oss-process=image/resize,w_210,h_210";
    }

}
