using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Trap planted by the archer
public class TrapController : MonoBehaviour
{
    [SerializeField] private float cooldown; //the cooldown is basically the attack's life
    private IEnumerator coroutine;

    //This method deals with the attack, to ensure that it doesn't just "remain there"
    //It's its life-timer
    private void Start()
    { 
        coroutine=LifeTimerCoroutine();
        StartCoroutine(coroutine);
    }

    //The LifeTimer coroutine, destroying the object if it remains too long
    private IEnumerator LifeTimerCoroutine()
    {
        yield return new WaitForSeconds(cooldown);
        Collider[] colliders = Physics.OverlapSphere(transform.position,3f);
        foreach(Collider other in colliders)
        {
            if(other.gameObject.GetComponent<Bowman>() != null)
            {
                other.gameObject.GetComponent<Bowman>().is_immobilized=false;
            }
            if(other.gameObject.GetComponent<Soldier>() != null)
            {
                other.gameObject.GetComponent<Soldier>().is_immobilized=false;
            }
            if(other.gameObject.GetComponent<Guru>() != null)
            {
               other.gameObject.GetComponent<Guru>().is_immobilized=false; 
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag=="Ennemy")
        {
            if(other.gameObject.GetComponent<Bowman>() != null)
            {
                other.gameObject.GetComponent<Bowman>().is_immobilized=true;
            }
            if(other.gameObject.GetComponent<Soldier>() != null)
            {
                other.gameObject.GetComponent<Soldier>().is_immobilized=true;
            }
            if(other.gameObject.GetComponent<Guru>() != null)
            {
               other.gameObject.GetComponent<Guru>().is_immobilized=true; 
            }
        }
    }
    
}
