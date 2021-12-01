using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the player actions
//It is inherited/interacts with the WarriorController, the ArcherController and the SamouraiController
public class PlayerActionsController : MonoBehaviour
{
    public bool actionOnePossible; //the boolean that will tell wether or not the action one is "possible" i.e. not on cooldown (handled by the inherited scripts)

    private void Start()
    {
        actionOnePossible=true;
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && actionOnePossible==true)
        {
            ActionOne();
            actionOnePossible=false;
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            ActionTwo();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            ActionThree();
        }
    }

    public virtual void ActionOne(){}
    public virtual void ActionTwo(){}
    public virtual void ActionThree(){}
}
