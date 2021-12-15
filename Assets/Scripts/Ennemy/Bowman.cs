using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : Ennemy
{
    [SerializeField] GameObject arrow;
    private ArrowFactory factory;
    private Vector3 offset;

    private void Awake()
    {
        factory = arrow.GetComponent<ArrowFactory>();
        offset = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
    }

    public override void Attack()
    {
        if (Insphere())
        {
            //animation d'attaque
            navMeshAgent.enabled = false;
            if (Time.time > timeStamp + cooldown)
            {
                var inst = factory.GetNewInstance();
                inst.transform.position = transform.forward + transform.position;
                //Vector3 vector = new Vector3(transform.forward.z, 0, transform.forward.x).normalized;
                //inst.transform.localEulerAngles = 90 * vector;
                timeStamp = Time.time;
            }
            navMeshAgent.enabled = true;

        }

    }

   


}
