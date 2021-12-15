using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Ennemy
{
    public override void Attack()
    {
        if (Insphere())
        {
            //animation d'attaque
            
            if (Time.time > timeStamp + cooldown)
            {
                playerLife.TakeDamage(5);
                timeStamp = Time.time;
            }
        }

    }
}