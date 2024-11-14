using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private float detectRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCoolTime;
    [SerializeField] private float lastAttackTime;

    public event Action onMove;
    public event Action onAttack;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget <= detectRange)
        {
            onMove?.Invoke();
            agent.SetDestination(target.position);
            if (distanceToTarget <= attackRange)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        
        if (Time.time - lastAttackTime >= attackCoolTime)
        {
            if (target != null && Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                onAttack?.Invoke();
                lastAttackTime = Time.time;

                DealDamageToPlayer();
            }
        }
    }

    private void DealDamageToPlayer()
    {
        if (target != null)
        {
            PlayerCondition playerCondition = target.GetComponent<PlayerCondition>();
            if (playerCondition != null)
            {
                playerCondition.TakePhysicalDamage(5);
            }
        }
    }
}
