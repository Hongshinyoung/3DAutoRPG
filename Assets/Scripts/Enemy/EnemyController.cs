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
                Debug.Log("여기에 오긴하냐");
                Attack();
            }
        }
    }

    private void Attack()
    {
        onAttack?.Invoke();
    }
    
}
