using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorKeyColorGenerator : MonoBehaviour
{
    [SerializeField] private GameObject key1;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject hingeJointDoor1;
    [SerializeField] private GameObject hingeJointDoor2;
    [SerializeField] private GameObject hingeJointDoor3;
    MeshRenderer keymesh1;
    MeshRenderer keymesh2;
    MeshRenderer doorMesh;
    MeshRenderer hingeJointDoorMesh1;
    MeshRenderer hingeJointDoorMesh2;
    MeshRenderer hingeJointDoorMesh3;
    public Material[] colorMaterial;
    void Start()
    {
        keymesh1 = key1.GetComponent<MeshRenderer>();
        keymesh2 = key2.GetComponent<MeshRenderer>();
        doorMesh = door.GetComponent<MeshRenderer>();
        hingeJointDoorMesh1 = hingeJointDoor1.GetComponent<MeshRenderer>();
        hingeJointDoorMesh2 = hingeJointDoor2.GetComponent<MeshRenderer>();
        hingeJointDoorMesh3 = hingeJointDoor3.GetComponent<MeshRenderer>();

        int randomColor1 = Random.Range(0, colorMaterial.Length);
        int randomColor2;
        do
        {
            randomColor2 = Random.Range(0, colorMaterial.Length);
        } while (randomColor2 == randomColor1);

        keymesh1.material.color = colorMaterial[randomColor1].color;
        keymesh2.material.color = colorMaterial[randomColor2].color;

        doorMesh.material.color = colorMaterial[randomColor1].color;
        hingeJointDoorMesh1.material.color = colorMaterial[randomColor2].color;
        hingeJointDoorMesh2.material.color = colorMaterial[randomColor2].color;
        hingeJointDoorMesh3.material.color = colorMaterial[randomColor2].color;
    }

}
