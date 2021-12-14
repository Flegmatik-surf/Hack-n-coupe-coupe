using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Basic Sword Strike attack of the warrior and the shuriken attack of the samourai
public class BasicAttackController : MonoBehaviour
{
    [SerializeField] private float cooldown;
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
            Destroy(gameObject);
        }
        if(gameObject.tag=="EnnemyAttack" && body.gameObject.tag=="Player")
        {
            body.gameObject.GetComponent<LifeManager>().TakeDamage(5);
            Destroy(gameObject);
        }
    }

    //The LifeTimer coroutine, destroying the object if it remains too long
    private IEnumerator LifeTimerCoroutine()
    {
        yield return new WaitForSeconds(cooldown);
        Destroy(gameObject);
    }
}
