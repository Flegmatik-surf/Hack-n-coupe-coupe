using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                c.gameObject.GetComponent<Ennemy>().TakeDamage(damage);
            }
        }
    }
}
