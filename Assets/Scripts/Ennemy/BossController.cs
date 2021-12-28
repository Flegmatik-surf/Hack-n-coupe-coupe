using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the script used by the boss :
/*
Variables :
phaseIndicator : bool : indique le comportement du boss
bigCooldown : float : vaut 10f
mediumCooldown : float : vaut 5f
smallCooldown : float : vaut 3f

Fonctions :
- BossTestHealth() :    appelée à chaque fois que le boss prend des dégâts, teste dans quelle action le boss est, met à jour phaseIndicator
- Attack() :    appelée par Ennemy.cs, lance les actions 1 et 2 du comportement selon phaseIndicator
- SpawnMobs(int mobsNumber) :    spawn MobNumber de squelettes autour de lui
- ReanimateMobs(int mobsNumber) :    réanime MobNumber de monstres de la map
- ReanimateAll() :      appelle ReanimateMobs avec en paramètre d'entrée le nombre de Tombstone présentes dans la map (check via FindGameObjectsWithTag("Tombstone"))
- BlastAttack() :    lance une boule d'énergie 
- ShadowSpikesAttack(int spikesNumber) : spawn SpikesNumber piques d'ombres autour de la map
- Heal(int HealthNumber) : heals the boss for HealthNumber HP - either called by the shadow spikes or ReanimateAll(), cannot heal beyond MaxHp
*/
public class BossController : Ennemy
{
    //the variable indicating the phase
    public int phaseIndicator=1; 

    //the different cooldowns of the boss' actions :
    [SerializeField] private float bigCooldown=10f;
    [SerializeField] private float mediumCooldown=5f;
    [SerializeField] private float smallCooldown=3f;

    //This function is called everytime the boss takes damage :
    //it is called AFTER CURRENTHP AND SLIDER HAVE BEEN CHANGED
    //DO NOT CHANGE THEM HERE
    public override void BossTestHealth()
    {
        if(currentHP<=100 && currentHP>50) //if the boss' HP drops below 50%
        {
            phaseIndicator=2; //we indicate a new phase
            maxHP=100; //we update maxHP to avoid healing beyond this stage
        }
        if(currentHP<=50)
        {
            phaseIndicator=3;
            maxHP=50;
        }

    }

    //This function calls the different attacks of the boss :
    //Called by Ennemy.cs script :
    public override void Attack()
    {

    }

    //This function spawns a given amount of mobs around the boss :
    private void SpawnMobs(int mobsNumber)
    {

    }

    //This function reanimates a given number of mobs :
    private void ReanimateMobs(int mobsNumber)
    {

    }

    //This function reanimates all the mobs present when it's called :
    private void ReanimateAll()
    {

    }

    //This function launches a ball of energy :
    private void BlastAttack()
    {

    }

    //This function spawns a given amount of spikes :
    private void ShadowSpikesAttack(int spikesNumber)
    {

    }

    //This function heals the boss for a given amount :
    //It is called either by the shadow spikes or by actions within BossController
    public void Heal(int HealthNumber)
    {

    }
}
