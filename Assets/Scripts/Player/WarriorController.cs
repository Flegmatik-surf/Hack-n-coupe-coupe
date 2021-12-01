using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Warrior class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class WarriorController : PlayerActionsController
{
    private float speed=5f;
    [SerializeField] GameObject swordStrike;
    [SerializeField] private float cooldownActionOne;
    private IEnumerator coroutine;

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
        swordStrike.transform.position=transform.position+new Vector3(2,0,0);
        yield return new WaitForSeconds(cooldown);
        actionOnePossible=true;
    }
//--------------------------------------------------------------------------------------------
    public override void ActionTwo(){}
    public override void ActionThree(){}
}
