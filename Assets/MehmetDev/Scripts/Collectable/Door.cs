using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    MeshRenderer doorMesh;
    Color doorColor;

    public Color GetDoorColor()
    {
        doorMesh = GetComponent<MeshRenderer>();
        doorColor = doorMesh.material.color;
        return doorColor;
    }
}
