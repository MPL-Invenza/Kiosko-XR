using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * 
 * Nombre: Message
 * 
 * Descripcion:  Clase que contiene la informacion sobre el dispositivo para que sea visible dentro del Vroom
 * 
 * 
 **/
[Serializable]
public class Message
{
    public string id;
    public string name;
    public string memory_p;
    public string memory;
    public string drums;
    public List<string> screenshot = new List<string>();
}


/**
 * 
 * Nombre: Messageholder
 * 
 * Descripcion:  Clase que contiene un objeto de tipo Message, para tener el formato solicitado desde Vroom
 * 
 * 
 **/
[Serializable]
public class Messageholder
{
    public Message message;
}
