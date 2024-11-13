using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private EnemyController controller;

    private readonly int isWalk = Animator.StringToHash("isWalk");
    private readonly int doAttack = Animator.StringToHash("doAttack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponentInParent<EnemyController>();
    }

    private void Start()
    {
        controller.onMove += Move;
        controller.onAttack += Attack;
    }

    private void Move()
    {
        animator.SetBool(isWalk, true);
    }

    private void Attack()
    {
        animator.SetTrigger(doAttack);
    }
}
