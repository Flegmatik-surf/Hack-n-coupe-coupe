using System;
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

    //Variables containing relevant gameobjects and scripts used by the samourai :
    [SerializeField] private GameObject shuriken;
    public GameObject attackPosition;
    [SerializeField] private float firingSpeed;
    [SerializeField] private Rigidbody samouraiRigidbody;
    [SerializeField] private float dashSpeed;
    [SerializeField] private DeplacementPlayer moveClickScript;
    [SerializeField] private GameObject dashAttack;
    [SerializeField] private GameObject chargedStrike;
    private float damageDealt;

    //Variables relative to audio source :
    protected AudioSource audioSource;
    [SerializeField] public AudioClip soundAttack1;
    [SerializeField] public AudioClip soundAttack2;
    [SerializeField] public AudioClip soundAttack3;

    //the animator :
    [SerializeField] private Animator animator;

    public bool invul;

    private void Awake()
    {
        invul=GetComponent<LifeManager>().invulnerable;
        audioSource = GetComponent<AudioSource>();
    }

    // The Action One
    //--------------------------------------------------------------------------------------------
    public static event Action<float> samouraiActionOneCalled;
    public static event Action<float> samouraiActionTwoCalled;
    public static event Action<float> samouraiActionThreeCalled;

    // The Action One
    //--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action One
    public override void ActionOne()
    {
        samouraiActionOneCalled?.Invoke(cooldownActionOne);
        coroutine=ActionOneCoroutine(cooldownActionOne);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the shuriken attack
    private IEnumerator ActionOneCoroutine(float cooldown)
    {
        audioSource.PlayOneShot(soundAttack1);
        GameObject new_attack = Instantiate(shuriken);
        new_attack.tag="PlayerAttack";
        new_attack.transform.position=attackPosition.transform.position;
        StartCoroutine(AttackAnimation());
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
        samouraiActionTwoCalled?.Invoke(cooldownActionTwo);
        coroutine=ActionTwoCoroutine(cooldownActionTwo);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the Dash
    private IEnumerator ActionTwoCoroutine(float cooldown)
    {
        audioSource.PlayOneShot(soundAttack2);
        moveClickScript.ChangeState();
        dashAttack.SetActive(true);
        GetComponent<LifeManager>().invulnerable=true;
        samouraiRigidbody.velocity=transform.forward*dashSpeed;
        yield return new WaitForSeconds(0.3f);
        dashAttack.SetActive(false);
        GetComponent<LifeManager>().invulnerable=false;
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
        samouraiActionThreeCalled?.Invoke(cooldownActionThree);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the ....
    private IEnumerator ActionThreeCoroutine(float cooldown, int chargeIndicator)
    {
        StartCoroutine(AttackAnimation());
        audioSource.PlayOneShot(soundAttack3);
        //we start by taking the right cooldown and damage per the charge indicator :
        if (chargeIndicator==1){cooldown=4;damageDealt=4;}
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

    private IEnumerator AttackAnimation(){
        animator.SetBool("attacking",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("attacking",false);
    }

}
