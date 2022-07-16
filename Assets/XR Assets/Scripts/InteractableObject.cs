using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private Color[] normalColor;
    public Color32 pointedColor = new Color32(150, 0, 0, 255);
    public bool emission = true;
    public byte materialIndex = 0;
    public bool allMaterials = true;

    private void Start()
    {
        if (allMaterials)
            normalColor = new Color[GetComponent<MeshRenderer>().materials.Length];
        else
            normalColor = new Color[1];

        if (emission)
        {
            GetComponent<MeshRenderer>().materials[materialIndex].EnableKeyword("_EMISSION");
            for (byte i = 0; i < normalColor.Length; i++)
            {
                normalColor[i] = Color.black;
            }
        }
        else
        {
            if (allMaterials)
            {
                for (byte i = 0; i < normalColor.Length; i++)
                {
                    normalColor[i] = GetComponent<MeshRenderer>().materials[i].color;
                }
            }
            else
                normalColor[0] = GetComponent<MeshRenderer>().materials[materialIndex].color;

        }

    }

    /// <summary>
    /// Change the object's color
    /// </summary>
    /// <param name="state"> 0: normal, 1: pointed</param>
    public void ChangeColor(int state)
    {
        if (emission)
        {
            switch (state)
            {
                case 0:
                    {
                        if (allMaterials)
                        {
                            for (byte i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                            {
                                GetComponent<MeshRenderer>().materials[i].SetColor("_EmissionColor", normalColor[0]);
                            }
                        }
                        else
                            GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_EmissionColor", normalColor[0]);
                    }
                    break;
                case 1:
                    {
                        if (allMaterials)
                        {
                            foreach (Material material in GetComponent<MeshRenderer>().materials)
                                material.SetColor("_EmissionColor", pointedColor);
                        }
                        else
                            GetComponent<MeshRenderer>().materials[materialIndex].SetColor("_EmissionColor", pointedColor);
                    }
                    break;
            }
        }
        else
        {
            switch (state)
            {
                case 0:
                    {
                        if (allMaterials)
                        {
                            for (byte i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
                            {
                                GetComponent<MeshRenderer>().materials[i].color = normalColor[i];
                            }
                        }
                        else
                            GetComponent<MeshRenderer>().materials[materialIndex].color = normalColor[0];
                    }
                    break;
                case 1:
                    {
                        if (allMaterials)
                        {
                            foreach (Material material in GetComponent<MeshRenderer>().materials)
                                material.color = pointedColor;
                        }
                        else
                            GetComponent<MeshRenderer>().materials[materialIndex].color = pointedColor;
                    }
                    break;
            }
        }
    }
}
