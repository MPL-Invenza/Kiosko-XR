using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO.Compression;

public class ComprimirDireccion : EditorWindow
{

    string path;

    string zipPath;

    [MenuItem("Invenza Creator SDK/Comprimir Carpeta")]
    /**
     * Name: CrearVentana
     * Description: Crea la ventana para capturar la posicion de el GameObject
     * Params: NO
     * Return: NO
     *
     * */
    public static void CrearVentana()
    {
        GetWindow(typeof(ComprimirDireccion));
    }

    /**
  * Name: OnGUI
  * Description: Crea la ventana junto con las opciones internas
  * Params: NO
  * Return: la ventana dentro del editor con la opción de comprimir la carpeta deseada
  *
  * */
    private void OnGUI()
    {
        GUILayout.Label("Direccion de la carpeta", EditorStyles.boldLabel);

        if (GUILayout.Button("Buscar"))
        {
            path = EditorUtility.OpenFolderPanel("Seleccione la carpeta a comprimir", "", "");
        }

        GUILayout.TextField(path, GUILayout.MaxWidth(500.0f));


        if (GUILayout.Button("Comprimir"))
        {


            zipPath = EditorUtility.SaveFilePanel("Seleccione la carpeta donde va a alojar el comprimido", "", "result", "zip");

            ComprimirCarpeta(zipPath);
        }
    }

    /**
* Name: ComprimirCarpeta
* Description: Comprime la direccion dada y retorna un objeto del tipo .zip
*
* Params: zipPath. Un objeto tipo string que da la direccion a comprimir
*
* Return: una carpeta comprimida en formato zip
*
* */
    public void ComprimirCarpeta(string zipPath)
    {
        //System.IO.Compression.ZipFile.CreateFromDirectory(path, zipPath, System.IO.Compression.CompressionLevel.Fastest, true);
    }




}
