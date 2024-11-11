using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform wayPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkToRunTime = 2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange;

    private float curTime = 0;
    private bool isRunning;
    private bool isAttacking;
    private Transform targetEnemy;

    private void Start()
    {
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        curTime += Time.deltaTime;

        if (agent.speed == moveSpeed)
        {
            Walk();
        }
    }

    private void Walk()
    {
        float distanceToTarget = Vector3.Distance(wayPoint.position, transform.position);

        if(distanceToTarget > detectRange)
        {
            MoveEnemy();
        }

        if (curTime > walkToRunTime && !isRunning)
        {
            Run();
            curTime = 0;
        }

        if (distanceToTarget > attackRange)
        {
            agent.SetDestination(wayPoint.position);
        }
        else
        {
            Attack();
        }
    }

    private void MoveEnemy()
    {
    }

    private void Run()
    {
        if(!isAttacking)
        {
            isRunning = true;
            agent.speed *= 2f;
        }
        else
        {
            agent.speed = moveSpeed;
        }
    }


    private void Attack()
    {
        isAttacking = true;
    }
}
