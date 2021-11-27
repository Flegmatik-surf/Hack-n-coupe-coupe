using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : Ennemy
{
    [SerializeField] GameObject arrow;
    [SerializeField] ArrowFactory factory;
    public override void Attack()
    {
        if (Insphere())
        {
            //animation d'attaque

            if (Time.time > timeStamp + cooldown)
            {
                var inst = factory.GetNewInstance();
                inst.transform.position = transform.position;
                timeStamp = Time.time;
            }

        }

    }

   


}
