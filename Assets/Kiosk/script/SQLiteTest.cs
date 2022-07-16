using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.IO;

public class ContentVroomDB
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string ID_ELEMENT { get; set; }
    public string NAME_FILE_ZIP { get; set; }
    public string URL_INTERNAL_FILE { get; set; }
    public string SHORT_TITLE { get; set; }
    public string NAME_EXPERIENCES { get; set; }
    public string DESC_OBJETIVES { get; set; }
    public string DESC_DURATION { get; set; }
    public string IMAGE_CONTENT { get; set; }
    public string TYPE { get; set; }
    public string VERSION_LOCAL { get; set; }
    public string VERSION_SERVER { get; set; }
    public string IMAGE_FILE_PATH { get; set; }
    public string FOLDER_PATH { get; set; }
    public string APK_FILE_PATH { get; set; }

    public ContentVroomDB() { }

    public ContentVroomDB(ContentVroom contentVroom)
    {
        this.NAME_FILE_ZIP = contentVroom.NAME_FILE_ZIP;
        this.URL_INTERNAL_FILE = contentVroom.URL_INTERNAL_FILE;
        this.SHORT_TITLE = contentVroom.SHORT_TITLE;
        this.NAME_EXPERIENCES = contentVroom.NAME_EXPERIENCES;
        this.DESC_OBJETIVES = contentVroom.DESC_OBJETIVES;
        this.DESC_DURATION = contentVroom.DESC_DURATION;
        this.IMAGE_CONTENT = contentVroom.IMAGE_CONTENT;
        this.TYPE = contentVroom.TYPE;
        this.VERSION_SERVER = contentVroom.VERSION;
        this.ID_ELEMENT = contentVroom.ID_ELEMENT;
    }

    public void UpdateValues(ContentVroom contentVroom)
    {
        this.NAME_FILE_ZIP = contentVroom.NAME_FILE_ZIP;
        //this.URL_INTERNAL_FILE = contentVroom.URL_INTERNAL_FILE;
        this.SHORT_TITLE = contentVroom.SHORT_TITLE;
        this.NAME_EXPERIENCES = contentVroom.NAME_EXPERIENCES;
        this.DESC_OBJETIVES = contentVroom.DESC_OBJETIVES;
        this.DESC_DURATION = contentVroom.DESC_DURATION;
        this.IMAGE_CONTENT = contentVroom.IMAGE_CONTENT;
        this.TYPE = contentVroom.TYPE;
        this.VERSION_SERVER = contentVroom.VERSION;
        this.ID_ELEMENT = contentVroom.ID_ELEMENT;
    }

}

public class SQLiteTest
{
    private SQLiteConnection _connection;
    private DownloadUtils downloadUtils = new DownloadUtils();


    public SQLiteTest(string DatabaseName)
    {
#if UNITY_EDITOR
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#elif UNITY_ANDROID
        // check if file exists in Application.persistentDataPath
        //var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        //var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        string dbPath = downloadUtils.GetAndroidInternalFilesDir()+"/"+DatabaseName;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);
    }

    public void CreateVroomContentDB()///////////////
    {
        _connection.DropTable<ContentVroomDB>();
        _connection.CreateTable<ContentVroomDB>();

    }

    public void InsertDataList(IEnumerable<ContentVroomDB> data)
    {
        _connection.InsertAll(data);
    }

    public void InsertData(ContentVroomDB data)
    {

        _connection.Insert(data);

    }

    public IEnumerable<ContentVroomDB> GetContentData()
    {
        return _connection.Table<ContentVroomDB>();
    }

    public void ReplaceData(ContentVroomDB data)
    {
        _connection.InsertOrReplace(data);
    }

    public ContentVroomDB GetContent(int rowIndex)
    {
        return _connection.Table<ContentVroomDB>().ElementAt(rowIndex);
    }

    public int GetTableRowCount()
    {
        return _connection.Table<ContentVroomDB>().Count();
    }

    public bool TableExists()
    {
        Debug.Log("1.sqlitest");
        if (_connection.GetTableInfo("ContentVroomDB").Count == 0)
        {
            Debug.Log("_connection null");
            return false;
        }
        else
        {
            Debug.Log("2.sqlitest");
            return true;
        }
        //if (_connection.Table<ContentVroomDB>() == null)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}

    }

    public int DeleteContentInTable(int contentId)
    {
        int newId = contentId;
        return _connection.Delete(newId);
    }

//    public 
//    {
//}

    //public bool SearchForContent(string NAME_EXPERIENCE)
    //{
    //    if (_connection.Table<ContentVroomDB>().Where(x => x.NAME_EXPERIENCES == NAME_EXPERIENCE).FirstOrDefault() == null)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}

    public bool SearchForIDElement(string ID_ELEMENT, out ContentVroomDB content)
    {
        content = _connection.Table<ContentVroomDB>().Where(x => x.ID_ELEMENT == ID_ELEMENT).FirstOrDefault();
        if (content == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public ContentVroomDB getElementById(string ID_ELEMENT)
    {
        return _connection.Table<ContentVroomDB>().Where(x => x.ID_ELEMENT == ID_ELEMENT).FirstOrDefault();
            
    }

    //public Person GetJohnny()
    //{
    //    return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
    //}

}
