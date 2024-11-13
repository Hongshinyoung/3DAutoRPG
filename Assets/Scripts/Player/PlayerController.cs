using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [Header("Move")]
    [SerializeField] private Transform wayPoint;
    [SerializeField] private float walkToRunTime = 3f;
    [SerializeField]private Transform targetEnemy;
    public NavMeshAgent agent;
    public float moveSpeed;
    private bool isRunning;
    
    [Header("Detect")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float detectRange;
    
    [Header("Attack")]
    public bool isAttacking;
    private float lastAttackTime;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCoolTime = 1.5f;
    
    private float curTime = 0;

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

            if (distanceToEnemy > attackRange && !isAttacking)
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
            
            if (distanceToTarget > detectRange && !isAttacking)
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
        if (Time.time - lastAttackTime >= attackCoolTime)
        {
            if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.position) <= attackRange)
            {
                isAttacking = true;
                CharacterManager.Instance.Player.animationController.Attack();
                lastAttackTime = Time.time;
                isAttacking = false;

                DealDamageToEnemy();
            }
        }
    }

    private void DealDamageToEnemy()
    {
        if (targetEnemy != null)
        {
            EnemyCondition enemyCondition = targetEnemy.GetComponent<EnemyCondition>();
            if (enemyCondition != null)
            {
                enemyCondition.TakePhysicalDamage(10);
            }
        }
    }
}
