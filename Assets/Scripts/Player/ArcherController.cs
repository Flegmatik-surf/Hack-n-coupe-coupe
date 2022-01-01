using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//This script is used by the Archer class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class ArcherController : PlayerActionsController
{
    [SerializeField] private float cooldownActionOne;
    [SerializeField] private float cooldownActionTwo;
    [SerializeField] private float cooldownActionThree;
    private IEnumerator coroutine;

    public GameObject attackPosition;
    [SerializeField] GameObject arrow;
    [SerializeField] private float firingSpeed;
    [SerializeField] private GameObject trap;

    public static event Action<float> bowmanActionOneCalled;
    public static event Action<float> bowmanActionTwoCalled;
    public static event Action<float> bowmanActionThreeCalled;

    // The Action One
    //--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action One
    public override void ActionOne()
    {
        bowmanActionOneCalled?.Invoke(cooldownActionOne);
        coroutine=ActionOneCoroutine(cooldownActionOne);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the arrow shot
    private IEnumerator ActionOneCoroutine(float cooldown)
    {
        GameObject new_attack = Instantiate(arrow);
        new_attack.tag="PlayerAttack";
        new_attack.transform.position=attackPosition.transform.position;
        new_attack.transform.rotation=attackPosition.transform.rotation;
        new_attack.transform.Rotate(new Vector3(90,0,0));
        new_attack.GetComponent<Rigidbody>().AddForce(transform.forward*firingSpeed); //Unlike the warrior's basic attack, we give the arrow a forward momentum
        yield return new WaitForSeconds(cooldown);
        actionOnePossible=true;
    }
//--------------------------------------------------------------------------------------------

// The Action Two
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Two
    public override void ActionTwo()
    {
        bowmanActionTwoCalled?.Invoke(cooldownActionTwo);
        coroutine=ActionTwoCoroutine(cooldownActionTwo);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the trap
    private IEnumerator ActionTwoCoroutine(float cooldown)
    {
        GameObject new_attack=Instantiate(trap);
        new_attack.transform.position=transform.position;
        yield return new WaitForSeconds(cooldown);
        actionTwoPossible=true;
    }
//--------------------------------------------------------------------------------------------

// The Action Three
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Three
    public override void ActionThree()
    {
        bowmanActionThreeCalled?.Invoke(cooldownActionThree);
        coroutine=ActionThreeCoroutine(cooldownActionThree);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the boost
    private IEnumerator ActionThreeCoroutine(float cooldown)
    {
        //We activate the different boosts :
        gameObject.GetComponent<NavMeshAgent>().speed=gameObject.GetComponent<NavMeshAgent>().speed*2f;
        float cooldownSave=cooldownActionOne;
        cooldownActionOne=0.5f;
        yield return new WaitForSeconds(3f);
        //we deactivate the different boosts :
        cooldownActionOne=cooldownSave;
        gameObject.GetComponent<NavMeshAgent>().speed=gameObject.GetComponent<NavMeshAgent>().speed/2f;
        actionThreePossible=true;
    }
//--------------------------------------------------------------------------------------------
}
