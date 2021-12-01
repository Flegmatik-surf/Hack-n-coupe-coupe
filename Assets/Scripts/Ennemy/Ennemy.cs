using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    protected GameObject player;
    protected Transform playerTransform;
    protected LifeManager playerLife;

    [SerializeField] protected float maxHP;
    [SerializeField] protected float currentHP;
    [SerializeField] protected float speed;
    [SerializeField] protected float rangeIn; //D'où il voit le player
    [SerializeField] protected float sphereAttack; //D'où il peut l'attaquer
    [SerializeField] protected float cooldown = 1f;


    protected float timeStamp = 0f;


    //true si player dans la sphere de visuel


    //true si player dans la sphere d'attaque
    //private bool Insphere { get { return Vector3.Distance(transform.position, player.position) < sphereAttack; } }



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerLife = player.GetComponent<LifeManager>();
    }

    private void Update()
    {
        Chase();
        Attack();


    }

    protected void Chase()
    {
        if (Inrange())
        {
            transform.LookAt(playerTransform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed += newSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public virtual void Attack()
    {

    }

    protected bool Inrange()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) < rangeIn && Vector3.Distance(transform.position, playerTransform.position) > sphereAttack);
    }

    protected bool Insphere()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) <= sphereAttack);
    }
}
