using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is used by the player's life system
public class LifeManager : MonoBehaviour { 

    public float maxHP;
    public float currentHP;
    public bool invulnerable;
    GameObject spawn;

    //event pour l'UI
    public static event Action defeatSignal;
    public static event Action<float> playerHPChanged;

    // Start is called before the first frame update
    void Start()
    {
        invulnerable=false;
        spawn = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP < 0)
        {
            spawn.GetComponent<Spawn>().RespawnPlayer(spawn.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        if(invulnerable==false){
            currentHP -= damage;
            //to adjust the lifebar, we just change it's value between 1 (max=current HP) and 0 (ennemy ded)
            playerHPChanged?.Invoke(currentHP / maxHP);
            if(currentHP<=0)
            {
                defeatSignal?.Invoke();
                //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemy")
        {
            TakeDamage(1);
        }
    }

    //this function is called to ensure that the player is healed when reset-ed
    public void Heal()
    {
        currentHP=maxHP;
        playerHPChanged?.Invoke(currentHP / maxHP);

    }
}
