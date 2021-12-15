using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DieZone : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            NavMeshAgent agent = collision.gameObject.GetComponent<NavMeshAgent>();
            //agent.enabled = false;
            //player die
        }
    }
}
