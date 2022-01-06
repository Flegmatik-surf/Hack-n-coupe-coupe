using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    protected AudioSource audioSource;
    [SerializeField] public AudioClip soundAttack1;
    [SerializeField] public AudioClip soundAttack2;
    [SerializeField] public AudioClip soundAttack3;

    private float speed;

    //the animator :
    [SerializeField] private Animator animator;
    
    NavMeshAgent agent;
    float duration=2f;
    
    public static event Action<float> warriorActionOneCalled;
    public static event Action<float> warriorActionTwoCalled;
    public static event Action<float> warriorActionThreeCalled;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        speed = gameObject.GetComponent<NavMeshAgent>().speed;
    }

    // The Action One
    //--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action One
    public override void ActionOne()
    {
        warriorActionOneCalled?.Invoke(cooldownActionOne);
        coroutine=ActionOneCoroutine(cooldownActionOne);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the basic attack
    private IEnumerator ActionOneCoroutine(float cooldown)
    {
        StartCoroutine(AttackAnimation());
        audioSource.PlayOneShot(soundAttack1);
        GameObject new_attack = Instantiate(swordStrike);
        new_attack.tag="PlayerAttack";
        new_attack.transform.position=attackPosition.transform.position;
        StartCoroutine(new_attack.gameObject.GetComponent<WarriorAttackController>().LaunchAttack());
        yield return new WaitForSeconds(cooldown);
        actionOnePossible=true;
        actionOneSlider.value=1;
    }
//--------------------------------------------------------------------------------------------

// The Action Two
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Two
    public override void ActionTwo()
    {
        warriorActionTwoCalled?.Invoke(cooldownActionTwo);
        coroutine=ActionTwoCoroutine(cooldownActionTwo);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the ....
    private IEnumerator ActionTwoCoroutine(float cooldown)
    {
        audioSource.PlayOneShot(soundAttack2);
        
        agent.enabled = false;
        Vector3 start = transform.position;
        Vector3 finish = start + transform.forward*6f;
        finish.y = 0;
        float animation = 0f;
        while (animation < duration)
        {
            animation += Time.deltaTime;
            transform.position = MathParabola.Parabola(start,finish, duration, animation / duration);
            yield return null;
        }
        audioSource.PlayOneShot(soundAttack2);
        yield return new WaitForSecondsRealtime(0.5f);
        agent.enabled = true;
        yield return new WaitForSeconds(cooldown);

        actionTwoPossible =true;
        actionTwoSlider.value=1;
    }
//--------------------------------------------------------------------------------------------

// The Action Three
//--------------------------------------------------------------------------------------------
    //Calls the coroutine that handles the action Three
    public override void ActionThree()
    {
        warriorActionThreeCalled?.Invoke(cooldownActionThree);
        coroutine=ActionThreeCoroutine(cooldownActionThree);
        StartCoroutine(coroutine);
    }

    //This method is used to instantiate the tornado-like attack
    private IEnumerator ActionThreeCoroutine(float cooldown)
    {
        StartCoroutine(AttackAnimation());
        audioSource.PlayOneShot(soundAttack3);
        tornadoAttack.SetActive(true);
        gameObject.GetComponent<NavMeshAgent>().speed=speed*1.5f;
        StartCoroutine(tornadoAttack.GetComponent<TornadoStrikeController>().LaunchAttack(1));
        yield return new WaitForSeconds(3f);
        tornadoAttack.SetActive(false);
        gameObject.GetComponent<NavMeshAgent>().speed=speed/1.5f;
        yield return new WaitForSeconds(cooldown);
        actionThreePossible=true;
        actionThreeSlider.value=1;
    }
//--------------------------------------------------------------------------------------------

    private IEnumerator AttackAnimation(){
        animator.SetBool("attacking",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("attacking",false);
    }
}
