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

    //This method deals with the attack, to ensure that it doesn't just "remain there"
    //It's its life-timer
    private void Update()
    { 
        if(Time.time > cooldown)
        {
            Destroy(this);
        }
    }

    //It only deals with itself
    private void OnCollisionEnter(Collision body)
    {
        Destroy(gameObject);
    }
}
