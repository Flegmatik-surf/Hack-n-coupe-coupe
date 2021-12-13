using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Samourai class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class SamouraiController : PlayerActionsController
{
    [SerializeField] private float cooldownActionOne;
    [SerializeField] private float cooldownActionTwo;
    [SerializeField] private float cooldownActionThree;
    private IEnumerator coroutine;

    [SerializeField] private GameObject shuriken;
    public GameObject attackPosition;
    [SerializeField] private float firingSpeed;

// The Action One
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action One
    public override void ActionOne()
    {
        coroutine=ActionOneCoroutine(cooldownActionOne);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the shuriken attack
    private IEnumerator ActionOneCoroutine(float cooldown)
    {
        GameObject new_attack = Instantiate(shuriken);
        new_attack.tag="PlayerAttack";
        new_attack.transform.position=attackPosition.transform.position;
        new_attack.GetComponent<Rigidbody>().AddForce(transform.forward*firingSpeed); //Unlike the warrior's basic attack, we give the shuriken a forward momentum
        yield return new WaitForSeconds(cooldown);
        actionOnePossible=true;
    }
//--------------------------------------------------------------------------------------------

// The Action Two
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Two
    public override void ActionTwo()
    {
        coroutine=ActionTwoCoroutine(cooldownActionTwo);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the ....
    private IEnumerator ActionTwoCoroutine(float cooldown)
    {
        yield return null;
        actionTwoPossible=true;
    }
//--------------------------------------------------------------------------------------------

// The Action Three
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Three
    public override void ActionThree()
    {
        coroutine=ActionThreeCoroutine(cooldownActionThree);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the ....
    private IEnumerator ActionThreeCoroutine(float cooldown)
    {
        yield return null;
        actionThreePossible=true;
    }
//--------------------------------------------------------------------------------------------
}
