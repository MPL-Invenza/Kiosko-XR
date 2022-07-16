using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DebugFile : MonoBehaviour
{

    private string myFilePath;
    private GameObject manager;
    private DownloadUtils dUtils;

    private string date = System.DateTime.UtcNow.Millisecond.ToString();

    // Start is called before the first frame update
    void Start()
    {
        manager = this.gameObject;
        dUtils = manager.GetComponent<DownloadUtils>();
        myFilePath = dUtils.GetAndroidInternalFilesDir() + "/LogFile" + date + ".txt";
        //myFilePath = Application.streamingAssetsPath + "/LogFile" + date + ".txt";

        if (File.Exists(myFilePath))
        {
            try
            {
                File.Delete(myFilePath);
                Debug.Log("file deleted");
            }
            catch (System.Exception e)
            {
                Debug.Log("cannot delete de file");
            }
        }
    }

    public void WriteToFile(string message)
    {
        try
        {
            StreamWriter fileWriter = new StreamWriter(myFilePath, true);


            fileWriter.Write(message);
            fileWriter.WriteLine();
            fileWriter.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("Cannot write into the file" + e);
        }
    }
}
