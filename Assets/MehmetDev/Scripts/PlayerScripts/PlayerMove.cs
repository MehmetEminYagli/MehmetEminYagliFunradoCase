using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public FloatingJoystick floatingJoystick;
    public Rigidbody rb;
    public Animator animator;

    private bool isWalking = false;

   public PlayerFrontRaycast PlayerFrontRaycast;


    private void Awake()
    {
        PlayerFrontRaycast.GetComponent<PlayerFrontRaycast>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = Vector3.back * floatingJoystick.Vertical + Vector3.left * floatingJoystick.Horizontal;

        if (direction.magnitude > 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(rotation);
            rb.angularVelocity = Vector3.zero;
        }
        rb.velocity = direction.normalized * speed;

        PlayerFrontRaycast.CheckForObjectsInFront(direction);
    }


   
 

}
