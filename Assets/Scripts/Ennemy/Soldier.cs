using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;
    private LifeManager playerLife;

    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;
    [SerializeField] private float speed;

    [SerializeField] private float rangeIn; //D'où il voit le player
    [SerializeField] private float sphereAttack; //D'où il peut l'attaquer

    

    private float timeStamp = 0f;
    private float cooldown = 1f;

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

    private void Chase()
    {
        if (Inrange())
        {
            transform.LookAt(playerTransform);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (Insphere())
        {
            //animation d'attaque
            
            if (Time.time> timeStamp+cooldown)
            {
                playerLife.currentHP -= 5;
                timeStamp = Time.time;
            }
           
        }
        
    }











    private bool Inrange()
    {
        return (Vector3.Distance(transform.position, playerTransform.position) < rangeIn && Vector3.Distance(transform.position, playerTransform.position)> sphereAttack);
}

    private bool Insphere()
    {
        return ( Vector3.Distance(transform.position, playerTransform.position) <= sphereAttack);
    }
}
