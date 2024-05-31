using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;
    int wayPointIndex;
    Vector3 target;

    public Animator animator;
   
    private bool isWalking = false;
    private bool isWaiting = false;
    private bool isAttacking = false;
    private float waitTime = 2f;
    private float attackAnimationDuration = 1.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (!isWaiting && Vector3.Distance(transform.position, target) < 1f)
        {
            StartCoroutine(WaitAndMoveToNextWaypoint());
        }
    }

    private void FixedUpdate()
    {
        if (agent.velocity.magnitude > 0f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        animator.SetBool("isWalking", isWalking);

        if (isAttacking)
        {
            animator.SetBool("isAttack", isAttacking);
        }
        else
        {
            animator.SetBool("isAttack", false);
        }
    }

    public void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttack", isAttacking);
            StartCoroutine(EndAttackAfterAnimation());
        }
    }

    private IEnumerator EndAttackAfterAnimation()
    {
        yield return new WaitForSeconds(attackAnimationDuration);
        isAttacking = false;
        animator.SetBool("isAttack", false);

    }

    private IEnumerator WaitAndMoveToNextWaypoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        IterateWayPointIndex();
        UpdateDestination();
        isWaiting = false;
    }

    private void UpdateDestination()
    {
        if(waypoints.Length != 0)
        {
            target = waypoints[wayPointIndex].position;
            agent.SetDestination(target);
        }
        else
        {
            Debug.Log("waypoint atanmadı");
        }
        
    }

    private void IterateWayPointIndex()
    {
        wayPointIndex++;
        if (wayPointIndex == waypoints.Length)
        {
            wayPointIndex = 0;
        }
    }
}
