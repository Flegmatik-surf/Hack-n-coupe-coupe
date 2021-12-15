using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Trap planted by the archer
public class TrapController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="Ennemy")
        {
            /*
            other.gameObject.GetComponent<Bowman>().is_immobilized=true;
            other.gameObject.GetComponent<Soldier>().is_immobilized=true;
            other.gameObject.GetComponent<Guru>().is_immobilized=true;*/
        }
    }
    
}
