using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private Transform wayPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private float walkToRunTime = 2f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange;
    public float moveSpeed;

    private float curTime = 0;
    private bool isRunning;
    public bool isAttacking;
    [SerializeField]private Transform targetEnemy;

    private void Start()
    {
        agent.speed = moveSpeed;
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        FindClosestEnemy();
        WalkToWayPoint();
    }

    private void WalkToWayPoint()
    {
        if (targetEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, targetEnemy.position);

            if (distanceToEnemy > attackRange)
            {
                agent.SetDestination(targetEnemy.position);
            }
            else
            {
                Attack();
            }
        }
        else
        {
            float distanceToTarget = Vector3.Distance(wayPoint.position, transform.position);
            
            if (distanceToTarget > detectRange)
            {
                agent.SetDestination(wayPoint.position);
            }
            else
            {
                Attack();
            }

            if (curTime > walkToRunTime && !isRunning)
            {
                Run();
                curTime = 0;
            }
        }
    
        CharacterManager.Instance.Player.animationController.Walk();
    }


    private void FindClosestEnemy()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, detectRange, enemyLayer);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        targetEnemy = closestEnemy;
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
        
        CharacterManager.Instance.Player.animationController.Run();
    }


    private void Attack()
    {
        isAttacking = true;
        CharacterManager.Instance.Player.animationController.Attack();
        isAttacking = false;
    }
}
