using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Basic Sword Strike attack 
/*
- Attack 1 of the warrior
- Attack 1 of the ennemy soldier
*/
public class SwordStrikeController : MonoBehaviour
{
    private float cooldown=1f;
    private IEnumerator coroutine;

    //This method deals with the attack, to ensure that it doesn't just "remain there"
    //It's its life-timer
    private void Start()
    { 
        coroutine=LifeTimerCoroutine();
        StartCoroutine(coroutine);
    }

    //when encountering an object, it will call the TakeDamage of the body in question, dependent of its tag :
    private void OnCollisionEnter(Collision body)
    {
        if(gameObject.tag=="PlayerAttack" && body.gameObject.tag=="Ennemy")
        {
            body.gameObject.GetComponent<Ennemy>().TakeDamage(5);
        }
        if(gameObject.tag=="EnnemyAttack" && body.gameObject.tag=="Player")
        {
            body.gameObject.GetComponent<LifeManager>().TakeDamage(5);
        }
        Destroy(gameObject);
    }

    private IEnumerator LifeTimerCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
