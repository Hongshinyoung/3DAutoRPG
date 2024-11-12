using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private readonly int iswalk = Animator.StringToHash("isWalk");
    private readonly int isRun = Animator.StringToHash("isRun");
    private readonly int doAttack = Animator.StringToHash("doAttack");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Walk()
    {
        if(CharacterManager.Instance.Player.controller.agent.speed == CharacterManager.Instance.Player.controller.moveSpeed
           && !CharacterManager.Instance.Player.controller.isAttacking)
        animator.SetFloat(iswalk,CharacterManager.Instance.Player.controller.agent.speed);
    }

    public void Run()
    {
        if (CharacterManager.Instance.Player.controller.agent.speed > CharacterManager.Instance.Player.controller.moveSpeed
            && !CharacterManager.Instance.Player.controller.isAttacking)
            animator.SetFloat(isRun, CharacterManager.Instance.Player.controller.agent.speed);
    }

    public void Attack()
    {
        if (CharacterManager.Instance.Player.controller.agent.speed == 0)
        {
            animator.SetTrigger(doAttack);
        }
        
        Debug.Log("공격");
    }

}
