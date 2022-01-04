using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script controls the player actions
//It is inherited/interacts with the WarriorController and the ArcherController
//Due to the charged attack, the SamouraiController inherits from SamouraiActionsController
public class PlayerActionsController : MonoBehaviour
{
    public bool actionOnePossible=true; //the boolean that will tell wether or not the action one is "possible" i.e. not on cooldown (handled by the inherited scripts)
    public bool actionTwoPossible=true;
    public bool actionThreePossible=true;

    //Variables related to the player's UI :
    private GameObject playerUI;
    protected Slider actionOneSlider;
    protected Slider actionTwoSlider;
    protected Slider actionThreeSlider;


    private void Start()
    {
        actionOnePossible=true;
        actionTwoPossible=true;
        actionThreePossible=true;
        playerUI = GameObject.FindGameObjectWithTag("MainCamera");
        actionOneSlider=playerUI.transform.Find("PlayerUI").Find("ActionOneSlider").GetComponent<Slider>();
        actionTwoSlider=playerUI.transform.Find("PlayerUI").Find("ActionTwoSlider").GetComponent<Slider>();
        actionThreeSlider=playerUI.transform.Find("PlayerUI").Find("ActionThreeSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0) && actionOnePossible==true)
        {
            ActionOne();
            
            actionOnePossible =false;
            actionOneSlider.value=0;
        }
        if(Input.GetKey(KeyCode.Mouse1) && actionTwoPossible==true)
        {
            ActionTwo();
            
            actionTwoPossible =false;
            actionTwoSlider.value=0;
        }
        if(Input.GetKey(KeyCode.Space) && actionThreePossible==true)
        {
            ActionThree();
            actionThreePossible =false;
            actionThreeSlider.value=0;
        }
    }

    public virtual void ActionOne(){}
    public virtual void ActionTwo(){}
    public virtual void ActionThree(){}
}
