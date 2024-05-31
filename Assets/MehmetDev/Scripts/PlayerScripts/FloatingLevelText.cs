using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingLevelText : MonoBehaviour
{

    Transform mainCamera;
    Transform player;
    public Transform worldSpaceCanvas;
    public Vector3 offset;
    void Start()
    {
        mainCamera = Camera.main.transform;
        player = transform.parent;
        worldSpaceCanvas = worldSpaceCanvas.transform;

        transform.SetParent(worldSpaceCanvas);
    }

    void Update()
    {
        if (player != null)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
            transform.position = player.position + offset;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
