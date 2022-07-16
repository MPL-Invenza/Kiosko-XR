using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateElement_Local : MonoBehaviour
{
    public create_element elementCreator;


    public void CreateCards(string type, int id, string title, string urlImage, string urlOPackage, Element element, string filename)
    {
        elementCreator.CreateElementDynamicTypeNoArray(type,id,title,urlImage,urlOPackage, element, null);
    }
}
