using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour, IDamagable
{
    public UICondition uiCondition;
    public PlayerController controller;
    private Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void TakePhysicalDamage(int damage)
    {
        health.Subtract(damage);
        onTakeDamage?.Invoke();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    
    private void Die()
    {
        Debug.Log("플레이어 사망");
    }

    void Update()
    {
        if (health.currentValue <= 0)
        {
            Die();
        }
        else
        {
            health.Subtract(health.passiveValue * Time.deltaTime);
        }
    }

}