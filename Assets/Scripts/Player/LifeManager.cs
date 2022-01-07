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
    private int currentWave;
    GameObject spawn;

    //event pour l'UI
    public static event Action defeatSignal;
    public static event Action<float> playerHPChanged;

    // Start is called before the first frame update
    void Start()
    {
        invulnerable=false;
        spawn = GameObject.FindGameObjectWithTag("Respawn");
        GameController.newWaveSignal += OnNewWave;
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
                MaxWaveUpdateStats(currentWave);
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


   //------------------- Auxiliary function in order to update maxWave stat of the current player ---------
    private void OnNewWave(string waveIndicator)
    {
        currentWave = int.Parse(waveIndicator);
    }

    //auxilary function in order to update the max Wave Stats of the selected player
    private void MaxWaveUpdateStats(int lastWaveOfTheGame)
    {
        string keyMaxWave = PlayerPrefs.GetString("selectedPlayer")  + " : maxWave";
        int registeredMaxWave = PlayerPrefs.GetInt(keyMaxWave);

        if (registeredMaxWave < lastWaveOfTheGame)
        {
            PlayerPrefs.SetInt(keyMaxWave, lastWaveOfTheGame);
        }
    }

    private void OnDestroy()
    {
        GameController.newWaveSignal -= OnNewWave;
    }
}
