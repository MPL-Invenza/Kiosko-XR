using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    public Text userID;
    public Text password;
    public AsiraRestful asira;

    public void GetText()
    {
        asira.eid = userID.text;
        asira.pw = password.text;
        print(userID.text);
        asira.CoroutineCaller("GetDataVroom");
    }
}
