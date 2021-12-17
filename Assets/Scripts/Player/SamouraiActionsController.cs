using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//The script used to guide the samourai's actions
public class SamouraiActionsController : MonoBehaviour
{
    public bool actionOnePossible=true; //the boolean that will tell wether or not the action one is "possible" i.e. not on cooldown (handled by the inherited scripts)
    public bool actionTwoPossible=true;
    public bool actionThreePossible=true;

    private float timer;
    private bool is_charging;
    private int chargeIndicator; //Will contain the charge indicator ()

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
        is_charging=false;
        timer=0f;
        playerUI = GameObject.FindGameObjectWithTag("MainCamera");
        actionOneSlider=playerUI.transform.Find("PlayerUI").Find("ActionOneSlider").GetComponent<Slider>();
        actionTwoSlider=playerUI.transform.Find("PlayerUI").Find("ActionTwoSlider").GetComponent<Slider>();
        actionThreeSlider=playerUI.transform.Find("PlayerUI").Find("ActionThreeSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Update()
    {
        Timer();
        if(Input.GetKey(KeyCode.Mouse0) && actionOnePossible==true)
        {
            ActionOne();
            actionOnePossible=false;
            actionOneSlider.value=0;
        }
        if(Input.GetKey(KeyCode.Mouse1) && actionTwoPossible==true)
        {
            ActionTwo();
            actionTwoPossible=false;
            actionTwoSlider.value=0;
        }
        //Unlike the two other characters, the charged attack needs to be charged hence a different way of handling the code :
        //this first if is called ONCE when the spacebar is pressed
        //it will simply launch properly the timer for the attack
        if(Input.GetKeyDown(KeyCode.Space) && actionThreePossible==true)
        {
            is_charging=true;
        }
        //this second if will be called ONCE when spacebar is released
        if(Input.GetKeyUp(KeyCode.Space) && actionThreePossible==true)
        {
            is_charging=false;
            if(timer<0.25f)
            {
                print("attack charged level 1");
                chargeIndicator=1;
            }
            if(0.25f<timer && timer<=1f)
            {
                print("attack charged level 2");
                chargeIndicator=2;
            }
            if(1f<timer)
            {
                print("attack charged level 3");
                chargeIndicator=3;
            }
            ActionThree(chargeIndicator);
            actionThreePossible=false;
            actionThreeSlider.value=0;
        }
    }

    public virtual void ActionOne(){}
    public virtual void ActionTwo(){}
    public virtual void ActionThree(int chargeIndicator){}

    //The function handling the timer of the charged attack :
    private void Timer()
    {
        if(is_charging)
        {
            timer += Time.deltaTime;
        } else {
            timer = 0;
        }
    }
}
