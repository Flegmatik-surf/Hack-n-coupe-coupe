using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowman : Ennemy
{
    [SerializeField] GameObject arrow;
    private ArrowFactory factory;
    private Vector3 offset;
    Vector3 target;
    
    private void Awake()
    {
        factory = arrow.GetComponent<ArrowFactory>();
        offset = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);
        player = GameObject.FindGameObjectWithTag("Player");
         target = (player.transform.position - transform.position).normalized;
    }

    public override void Attack()
    {
        if (Insphere())
        {
            StartCoroutine(AttackAnimation());
            //animation d'attaque
            navMeshAgent.enabled = false;
            if (Time.time > timeStamp + cooldown)
            {
                audioSource.PlayOneShot(audioSource.clip);
                var inst = factory.GetNewInstance();
                //inst.transform.Rotate(0,0, - (180 / Mathf.PI)*Mathf.Acos(target.z));
                //inst.transform.Rotate(0, 65,90);
                inst.transform.position = transform.forward + transform.position+ new Vector3(0,1,0);
                //Vector3 vector = new Vector3(transform.forward.z, 0, transform.forward.x).normalized;
                //inst.transform.localEulerAngles = 90 * vector;
                timeStamp = Time.time;
            }
            navMeshAgent.enabled = true;
        }
    }


}
