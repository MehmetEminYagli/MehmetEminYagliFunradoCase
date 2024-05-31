using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeJointDoor : MonoBehaviour
{
    MeshRenderer doorMesh;
    Color doorColor;
    public GameObject[] childrenDoor;
    private void Start()
    {
        for(int i =0; i< childrenDoor.Length; i++)
        {
            childrenDoor[i].GetComponent<BoxCollider>().enabled = false;
        }
    }

    public Color GetDoorColor()
    {
        doorMesh = GetComponentInChildren<MeshRenderer>();
        doorColor = doorMesh.material.color;
        return doorColor;
    }

    public void ColliderTrue()
    {
        for (int i = 0; i < childrenDoor.Length; i++)
        {
            childrenDoor[i].GetComponent<BoxCollider>().enabled = true;
        }
        GetComponent<BoxCollider>().enabled = false;
    
    }
}
