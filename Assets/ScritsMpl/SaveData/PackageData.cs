using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Name: PackageData
/// Description: Contiene las definiciones para guardar los datos sobre las aplicaciones instaladas en las gafas.
/// </summary>
[Serializable]
public class PackageData
{
    public string appName;
    public string appPackage;
    public string imgRoute;
    public string type;
    public Element element;
}
/// <summary>
/// Name: PackageList
/// Descripcion: Contiene una lista de objetos tipo PackageData
/// </summary>
[Serializable]
public class PackageList
{
    public List<PackageData> appList = new List<PackageData>();
}
