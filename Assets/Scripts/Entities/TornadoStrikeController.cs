using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoStrikeController : MonoBehaviour
{
    private IEnumerator coroutine;

    //Every 0.5 sec, the attack is initiated :
    public IEnumerator LaunchAttack(int damage)
    {
        CheckForEnnemies(damage);
        yield return new WaitForSeconds(0.5f);
        coroutine=LaunchAttack(damage);
        StartCoroutine(coroutine);
    }

    //We're going to check for enemies :
    private void CheckForEnnemies(int damage)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,5f);
        foreach(Collider c in colliders)
        {
            if(c.gameObject.tag=="Ennemy")
            {
                c.gameObject.GetComponent<Ennemy>().TakeDamage(damage);
            }
        }
    }
}
