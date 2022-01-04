using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fosse : MonoBehaviour
{
    public static event Action dieOnFosse;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            NavMeshAgent agent = collision.gameObject.GetComponent<NavMeshAgent>();
            agent.isStopped = true;
            agent.enabled = false;
            collision.gameObject.GetComponent<LifeManager>().currentHP=0;
            StartCoroutine(timer(0.02f));
            StopCoroutine(timer(0.2f));
            dieOnFosse?.Invoke();
            Destroy(collision.gameObject);
        }

    }

    IEnumerator timer(float time)
    {
        yield return new WaitForSeconds(time);
    }


}
