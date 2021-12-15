using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy : MonoBehaviour
{
    protected GameObject player;
    protected Transform playerTransform;
    protected LifeManager playerLife;
    private NavMeshAgent navMeshAgent;

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float speed;
    [SerializeField] protected float rangeIn; //D'o� il voit le player
    [SerializeField] protected float sphereAttack; //D'o� il peut l'attaquer
    [SerializeField] protected float cooldown = 1f;

    //buff
    protected float buffSpeedTimer=0f;
    protected bool canBeBuffSpeed = true;
    protected float baseSpeed;

<<<<<<< HEAD
    



=======
    //variable checkant si l'ennemi est immobilisé
    public bool is_immobilized;
>>>>>>> 8fb70b4a23548340b698f1afcab8c3fa7e7e75c7

    protected float timeStamp = 0f;


    //true si player dans la sphere de visuel


    //true si player dans la sphere d'attaque
    //private bool Insphere { get { return Vector3.Distance(transform.position, player.position) < sphereAttack; } }



    private void Start()
    {
        is_immobilized=false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerLife = player.GetComponent<LifeManager>();
        baseSpeed = speed;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        Chase();
        Attack();
        //buff speed
        buffSpeedTimer -= Time.deltaTime;
        if (buffSpeedTimer <= 0)
        {
            speed = baseSpeed;
        }
        
        

    }

    


    public void GetBuffSpeed(float buffSpeed, float buffSpeedTime)
    {
        if (canBeBuffSpeed)
        {
            canBeBuffSpeed = false;
            speed += buffSpeed;
            buffSpeedTimer += buffSpeedTime;
        }
        
        
    }

    protected void Chase()
    {
        if (Inrange() && is_immobilized==false)
        {
            transform.LookAt(playerTransform);
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    public void SetSpeedBuff(float newSpeed,float time)
    {
        speed += newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
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

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if(currentHP<=0)
        {
            Destroy(gameObject);
        }
    }

}
