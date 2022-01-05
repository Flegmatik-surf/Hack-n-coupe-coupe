using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the basic projectiles
public class BasicProjectileController : MonoBehaviour
{
    [SerializeField] private float cooldown; //the cooldown is basically the attack's life
    private IEnumerator coroutine;
    public float damage;

    //This method deals with the attack, to ensure that it doesn't just "remain there"
    //It's its life-timer
    private void Start()
    { 
        coroutine=LifeTimerCoroutine();
        StartCoroutine(coroutine);
    }

    //when encountering an object, it will call the TakeDamage of the body in question, dependent of its tag :
    private void OnTriggerEnter(Collider body)
    {
        if(gameObject.tag=="PlayerAttack" && body.gameObject.tag=="Ennemy")
        {
            body.gameObject.GetComponent<Ennemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if(gameObject.tag=="EnnemyAttack" && body.gameObject.tag=="Player")
        {
            body.gameObject.GetComponent<LifeManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if(body.gameObject.tag=="Wall")
        {
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
