using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    MeshRenderer keyMesh;
    Color keyColor;

    public Color Getcolor()
    {
        keyColor = GetComponent<MeshRenderer>().material.color;
        return keyColor;
    }
}
