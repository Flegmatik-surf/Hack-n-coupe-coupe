using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Warrior class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class WarriorController : PlayerActionsController
{
    [SerializeField] GameObject swordStrike;
    [SerializeField] private float cooldownActionOne;
    [SerializeField] private float cooldownActionTwo;
    [SerializeField] private float cooldownActionThree;
    private IEnumerator coroutine;
    [SerializeField] private GameObject tornadoAttack;
    public GameObject attackPosition;

// The Action One
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action One
    public override void ActionOne()
    {
        coroutine=ActionOneCoroutine(cooldownActionOne);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the basic attack
    private IEnumerator ActionOneCoroutine(float cooldown)
    {
        GameObject new_attack = Instantiate(swordStrike);
        new_attack.tag="PlayerAttack";
        new_attack.transform.position=attackPosition.transform.position;
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
        yield return new WaitForSeconds(cooldown);
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

    //This method is used to instantiate the tornado-like attack
    private IEnumerator ActionThreeCoroutine(float cooldown)
    {
        tornadoAttack.SetActive(true);
        gameObject.GetComponent<MoveClick>().speed=gameObject.GetComponent<MoveClick>().speed*1.5f;
        StartCoroutine(tornadoAttack.GetComponent<TornadoStrikeController>().LaunchAttack());
        yield return new WaitForSeconds(3f);
        tornadoAttack.SetActive(false);
        gameObject.GetComponent<MoveClick>().speed=gameObject.GetComponent<MoveClick>().speed/1.5f;
        yield return new WaitForSeconds(cooldown);
        actionThreePossible=true;
    }
//--------------------------------------------------------------------------------------------
}
