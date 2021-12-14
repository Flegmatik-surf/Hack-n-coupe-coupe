using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Samourai class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class SamouraiController : SamouraiActionsController
{
    [SerializeField] private float cooldownActionOne;
    [SerializeField] private float cooldownActionTwo;
    [SerializeField] private float cooldownActionThree;
    private IEnumerator coroutine;

    [SerializeField] private GameObject shuriken;
    public GameObject attackPosition;
    [SerializeField] private float firingSpeed;
    [SerializeField] private Rigidbody samouraiRigidbody;
    [SerializeField] private float dashSpeed;
    [SerializeField] private MoveClick moveClickScript;
    [SerializeField] private GameObject dashAttack;
    [SerializeField] private GameObject chargedStrike;
    private float damageDealt;

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

    //This method is used to instantiate the Dash
    private IEnumerator ActionTwoCoroutine(float cooldown)
    {
        moveClickScript.ChangeState();
        dashAttack.SetActive(true);
        samouraiRigidbody.velocity=transform.forward*dashSpeed;
        yield return new WaitForSeconds(0.3f);
        dashAttack.SetActive(false);
        moveClickScript.ChangeState();
        yield return new WaitForSeconds(cooldown);
        actionTwoPossible=true;
    }
//--------------------------------------------------------------------------------------------

// The Action Three
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Three
    public override void ActionThree(int chargeIndicator)
    {
        coroutine=ActionThreeCoroutine(cooldownActionThree, chargeIndicator);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the ....
    private IEnumerator ActionThreeCoroutine(float cooldown, int chargeIndicator)
    {
        //we start by taking the right cooldown and damage per the charge indicator :
        if(chargeIndicator==1){cooldown=4;damageDealt=4;}
        if(chargeIndicator==2){cooldown=5;damageDealt=7;}
        if(chargeIndicator==3){cooldown=7;damageDealt=10;}
        //We then add the new_attack as always :
        GameObject new_attack=Instantiate(chargedStrike);
        new_attack.GetComponent<BasicAttackController>().damage=damageDealt; //unlike other attacks, we set up a certain damage
        new_attack.transform.position=attackPosition.transform.position;
        new_attack.transform.rotation=transform.rotation;
        yield return new WaitForSeconds(cooldown);
        actionThreePossible=true;
    }
//--------------------------------------------------------------------------------------------
}
