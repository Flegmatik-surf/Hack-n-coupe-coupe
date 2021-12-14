using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashStrikeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider body)
    {
        if(body.gameObject.tag=="Ennemy")
        {
            body.gameObject.GetComponent<Ennemy>().TakeDamage(5);
        }
    }
}
