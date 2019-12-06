using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(TextMesh))]
public class GetFontTexture : MonoBehaviour
{
    public string targetTextureProperty = "_BaseMap";

    private void OnValidate()
    {
        GetComponent<Renderer>().sharedMaterial.SetTexture(targetTextureProperty, GetComponent<TextMesh>().font.material.mainTexture);
    }
}
