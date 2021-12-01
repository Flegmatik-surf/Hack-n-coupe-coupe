using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used by the Warrior class
//It is inherited from the PlayerActionsController script that controls the player's actions
public class WarriorController : PlayerActionsController
{
    private float speed=5f;
    [SerializeField] GameObject swordStrike;

    //This method is used to instantiate the basic attack
    public override void ActionOne()
    {
        GameObject new_attack = Instantiate(swordStrike);
        swordStrike.transform.position=transform.position+new Vector3(2,0,0);
    }
    public override void ActionTwo(){}
    public override void ActionThree(){}
}
