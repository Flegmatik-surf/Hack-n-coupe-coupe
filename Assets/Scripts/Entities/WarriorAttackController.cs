using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WarriorAttackController : MonoBehaviour
{
    private IEnumerator coroutine;
    [SerializeField] private int damage;

    //Every 0.5 sec, the attack is initiated :
    public IEnumerator LaunchAttack()
    {
        CheckForEnnemies();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    //We're going to check for enemies :
    private void CheckForEnnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,5f);
        foreach(Collider c in colliders)
        {
            if(c.gameObject.tag=="Ennemy")
            {
                StartCoroutine(PushBackEnnemies(c));
                c.gameObject.GetComponent<Ennemy>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator PushBackEnnemies(Collider c){
        c.GetComponent<NavMeshAgent>().enabled=false;
        c.GetComponent<Rigidbody>().AddForce((c.transform.position-transform.position)*2000);
        yield return null;
        c.GetComponent<NavMeshAgent>().enabled=true;
    }
}
