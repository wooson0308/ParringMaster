using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class WeaponAnimation : MonoBehaviour
{
    private Animator animator;

    private bool runningMotion;

    public bool RunningMotion
    {
        get { return runningMotion; }
        set { runningMotion = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LootMode(bool mode)
    {
        if (animator.GetBool("Loot Mode") == mode) return;
        animator.SetBool("Loot Mode", mode);
    }

    public void MoveAnim(Vector2 moveVec, float moveSpeed)
    {
        animator.SetBool("Movement", moveVec != Vector2.zero);
        animator.SetFloat("Move Anim Speed", moveSpeed);
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void OnAttack(bool value)
    {
        animator.SetBool("On Attack", value);
    }

    public void Skill()
    {
        animator.SetTrigger("Skill");
        StartCoroutine(ResetPPoint());
    }

    private IEnumerator ResetPPoint()
    {
        yield return new WaitForSeconds(0.1f);
        while (RunningMotion)
        {
            yield return null;
        }

        //animator.SetInteger("Parring Count", 0);
    }

    public void SetParringPoint(int parringPoint)
    {
        animator.SetInteger("Parring Count", parringPoint);
    }

    public void Guard()
    {
        animator.SetTrigger("Guard");
    }

    public void Cancel()
    {
        animator.SetTrigger("Cancel");
    }

    public void Stun()
    {
        animator.SetTrigger("Stun");
    }
}
