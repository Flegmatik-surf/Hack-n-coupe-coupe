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

    public override void Attack()
    {
        if (Insphere())
        {
            
            //animation d'attaque
            StartCoroutine(AttackAnimation());
            navMeshAgent.enabled = false;
            if (Time.time > timeStamp + cooldown)
            {
                StartCoroutine(AttackAnimation()); //a revoir
                audioSource.PlayOneShot(audioSource.clip);
                playerLife.TakeDamage(5);
                timeStamp = Time.time;
            }
            
        }
        navMeshAgent.enabled = true;
    }

    //the function handling the attack animation :
    private IEnumerator AttackAnimation(){
        animator.SetBool("attacking",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("attacking",false);
    }
}