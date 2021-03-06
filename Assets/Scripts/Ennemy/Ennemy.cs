using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{
    protected GameObject player;
    protected Transform playerTransform;
    protected LifeManager playerLife;
    protected NavMeshAgent navMeshAgent;

    [SerializeField] protected float rangeIn; //D'o� il voit le player
    [SerializeField] protected float sphereAttack; //D'o� il peut l'attaquer
    [SerializeField] protected float cooldown = 1f;

    
    protected AudioSource audioSource;
    

    //the tombstone used for revive :
    [SerializeField] protected GameObject tombstone;

    //buff
    protected float buffSpeedTimer=0f;
    protected bool canBeBuffSpeed = true;
    protected float baseSpeed;

    //variable checkant si l'ennemi est immobilisé
    public bool is_immobilized;

    [SerializeField] public float maxHP;
    [SerializeField] public float currentHP;

    //variables relatives à la healthbar :
    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private Slider slider;

    [SerializeField] public bool can_be_revived; //indicates wether an ennemy can be revived or not

    protected float timeStamp = 0f;

    //the animator :
    [SerializeField] public Animator animator;

    public void Start()
    {
        is_immobilized=false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        baseSpeed = navMeshAgent.speed;
        playerLife=player.GetComponent<LifeManager>();
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        Chase();
        Attack();
        //buff speed
        buffSpeedTimer -= Time.deltaTime;
        if (buffSpeedTimer <= 0)
        {
            navMeshAgent.speed = baseSpeed;
            buffSpeedTimer = 0f;
            canBeBuffSpeed = true;
        }

    }

    //This function deals with the ennemy being damaged
    //it handles both the calculus and the lifebar itself
    public void TakeDamage(float damage)
    {
        healthBarUI.SetActive(true);
        currentHP -= damage;
        slider.value=currentHP/maxHP; //to adjust the lifebar, we just change it's value between 1 (max=current HP) and 0 (ennemy ded)
        BossTestHealth(); //a special function used EXCLUSIVELY by the boss
        if(currentHP<=0) //when the enemy dies
        {
            if(can_be_revived==true)
            {
                GameObject new_tombstone = Instantiate(tombstone);
                new_tombstone.transform.position=transform.position;
            }
            Destroy(gameObject);
        }
    }

    public void GetBuffSpeed(float buffSpeed, float buffSpeedTime)
    {
        if (canBeBuffSpeed)
        {
            canBeBuffSpeed = false;
            navMeshAgent.speed += buffSpeed;
            buffSpeedTimer += buffSpeedTime;
        }
        
        
    }

    protected void Chase()
    {
        if (Inrange() && is_immobilized==false)
        {
            WalkAnimation();
            Vector3 targetPosition = new Vector3(playerTransform.position.x,
                                        this.transform.position.y,
                                        playerTransform.position.z);
            transform.LookAt(targetPosition);

            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    //the function handling the  walk animation :
    private void WalkAnimation(){
        animator.SetFloat("speed",navMeshAgent.velocity.magnitude/navMeshAgent.speed);
    }

    //the function handling the attack animation :
    public IEnumerator AttackAnimation(){
        animator.SetBool("attacking",true);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("attacking",false);
    }

    public virtual void Attack(){}

    protected bool Inrange()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) < rangeIn && Vector3.Distance(transform.position, playerTransform.position) > sphereAttack);
    }

    protected bool Insphere()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) <= sphereAttack);
    }

    public virtual void BossTestHealth(){} //a special function used EXCLUSIVELY by the boss
}
