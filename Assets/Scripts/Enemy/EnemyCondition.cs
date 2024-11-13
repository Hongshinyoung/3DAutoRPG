using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class EnemyCondition : MonoBehaviour, IDamagable
{
    public EnemyHPBar hpBar;
    public EnemyController controller;
    private Condition health
    {
        get { return hpBar.health; }
    }

    public event Action onDamaged;

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if(health.currentValue <= 0)
        {
            Die();
        }
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onDamaged?.Invoke();
    }

    public void Die()
    {
        Debug.Log("몬스터 사망");
        Destroy(gameObject);
    }
}
