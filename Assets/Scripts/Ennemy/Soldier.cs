using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Ennemy
{
    [SerializeField] private GameObject skeleton;
    private Animator animator;

    private void Awake() 
    {
        animator=skeleton.gameObject.GetComponent<Animator>();
        
    }

    public override void Animation() 
    {
        animator.SetFloat("speed",6);
    }

    public override void Attack()
    {
        if (Insphere())
        {
            //animation d'attaque
            StartCoroutine(AttackAnimation());
            navMeshAgent.enabled = false;
            if (Time.time > timeStamp + cooldown)
            {
                playerLife.TakeDamage(5);
                timeStamp = Time.time;
            }
            
        }
        navMeshAgent.enabled = true;
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetBool("is_attacking",true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length+animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        animator.SetBool("is_attacking",false);
    }
}