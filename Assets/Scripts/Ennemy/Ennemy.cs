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

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float rangeIn; //D'o� il voit le player
    [SerializeField] protected float sphereAttack; //D'o� il peut l'attaquer
    [SerializeField] protected float cooldown = 1f;

    //buff
    protected float buffSpeedTimer=0f;
    protected bool canBeBuffSpeed = true;
    protected float baseSpeed;

    //variables relatives à la healthbar :
    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private Slider slider;

    //variable checkant si l'ennemi est immobilisé
    public bool is_immobilized;


    protected float timeStamp = 0f;


    //true si player dans la sphere de visuel


    //true si player dans la sphere d'attaque
    //private bool Insphere { get { return Vector3.Distance(transform.position, player.position) < sphereAttack; } }



    private void Start()
    {
        is_immobilized=false;
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        baseSpeed = navMeshAgent.speed;
        playerLife=player.GetComponent<LifeManager>();
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
            Animation();
            transform.LookAt(playerTransform);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }

    public virtual void Animation(){}

    public virtual void Attack(){}

    protected bool Inrange()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) < rangeIn && Vector3.Distance(transform.position, playerTransform.position) > sphereAttack);
    }

    protected bool Insphere()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) <= sphereAttack);
    }

    //This function deals with the ennemy being damaged
    //it handles both the calculus and the lifebar itself
    public void TakeDamage(float damage)
    {
        healthBarUI.SetActive(true);
        currentHP -= damage;
        slider.value=currentHP/maxHP; //to adjust the lifebar, we just change it's value between 1 (max=current HP) and 0 (ennemy ded)
        if(currentHP<=0)
        {
            Destroy(gameObject);
        }
    }

}
