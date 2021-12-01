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

    //It only deals with itself
    private void OnCollisionEnter(Collision body)
    {
        Destroy(gameObject);
    }

    private IEnumerator LifeTimerCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
