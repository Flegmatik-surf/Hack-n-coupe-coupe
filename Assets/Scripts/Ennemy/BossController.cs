using System;
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
- FindTombstones() :     finds the number of active tombstones on the map
- CooldownTimer(float cooldown, int action)   : (coroutine) used to calculate adequate timers of cooldowns
- SpawnMobs(int mobsNumber) :    spawn MobNumber de squelettes autour de lui
- ReanimateMobs(int mobsNumber) :    réanime MobNumber de monstres de la map
- ReanimateAll() :      appelle ReanimateMobs avec en paramètre d'entrée le nombre de Tombstone présentes dans la map (check via FindGameObjectsWithTag("Tombstone"))
- BlastAttack(int action) :    lance une boule d'énergie (para entrée : le num associée à l'action)
- ShadowSpikesAttack(int spikesNumber) : spawn SpikesNumber piques d'ombres autour de la map
- Heal(int HealthNumber) : heals the boss for HealthNumber HP - either called by the shadow spikes or ReanimateAll(), cannot heal beyond MaxHp
*/
public class BossController : Ennemy
{
    //-----------------------------------------------------------------------------------------
    //The various variables :

    //the variable indicating the phase
    public int phaseIndicator=1; 

    //the different cooldowns of the boss' actions :
    [SerializeField] private float bigCooldown=10f;
    [SerializeField] private float mediumCooldown=5f;
    [SerializeField] private float smallCooldown=3f;

    //the gameObject of the ball of energy :
    [SerializeField] private GameObject energyBall;
    [SerializeField] private float firingSpeed; //the speed at which the balls move

    //the GameObject of the skeleton spawned by the boss :
    [SerializeField] private GameObject skeleton;

    //the GameObject of the shadowspike spawned by the boss :
    [SerializeField] private GameObject spike;

    //the attack position of the boss :
    [SerializeField] private GameObject attackPosition;

    //Two boolean variables, used by the boss to determine which attack is possible :
    private bool actionOnePossible;
    private bool actionTwoPossible;

    //event pour mettre à jour l'UI du Boss.
    public static event Action bossSpawn;
    public static event Action<float> bossLifeChanged;

    //a list that will contain, at a given time, the number of active Tombstones :
    private GameObject[] activeTombstones;
    private float initialMaxHP;

    //----------------------------------------------------------------------------------------
    //The various functions :
    private void Awake() 
    {
        actionOnePossible=true;
        actionTwoPossible=true;
        initialMaxHP = maxHP;
    }

    private new void Start()
    {
        base.Start();
        bossSpawn?.Invoke();
    }


    //This function is called everytime the boss takes damage :
    //it is called AFTER CURRENTHP AND SLIDER (LOCAL) HAVE BEEN CHANGED
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
        bossLifeChanged?.Invoke(currentHP / initialMaxHP); //invoke : update the Boss UI Health Bar
    }

    //This function calls the different attacks of the boss :
    //Called by Ennemy.cs script every update :
    public override void Attack()
    {
        //The first phase of the boss 
        if(phaseIndicator==1)
        { 
            if(actionTwoPossible==true) //if action 2 possible, execute
            {
                //réanime 5 ennemis aléatoires. Si aucun/pas assez d’ennemis morts, il spawn 5 squelettes. Cooldown de 10 secondes.
                FindTombstones(); //we start by finding all active tombstones on the map
                if(activeTombstones.Length>=5) //if there's 5 or more tombstones, we reanimate 5 mobs
                {ReanimateMobs(5);}
                else  //we spawn 5 mobs since there's not enough tombstones
                {SpawnMobs(5);}
                actionTwoPossible=false;
                StartCoroutine(CooldownTimer(bigCooldown, 2)); //we launch the cooldown on the ability
            } 
            if(actionOnePossible==true) //elif action 1 possible, execute
            {
                //Lance une boule d’énergie violette vers le joueur qui inflige 10 dégâts. Cooldown de 3 secondes.
                actionOnePossible=false;
                BlastAttack();
                StartCoroutine(CooldownTimer(smallCooldown,1));
            }
            else {
                //do something ?
            }

        }

        //The second phase of the boss 
        if(phaseIndicator==2)
        {
            if(actionTwoPossible==true) //if action 2 possible, execute
            {
                //réanime tous les ennemis présents dans l’arène. Il gagne 2 PV par ennemi réanimé. Cooldown de 10 secondes.
                FindTombstones();
                ReanimateAll(); //we reanimate everyone
                Heal(activeTombstones.Length*2); //the boss is healed for 2PV per tombstones
                StartCoroutine(CooldownTimer(bigCooldown, 2));
                actionTwoPossible=false;
            } 
            if(actionOnePossible==true) //elif action 1 possible, execute
            {
                //Spawn 5 squelettes, medium cooldown
                SpawnMobs(5);
                StartCoroutine(CooldownTimer(mediumCooldown, 1));
                actionOnePossible=false;
            } else {
                //do something ?
            }

        }

        //The third phase of the boss 
        if(phaseIndicator==3)
        {
            if(actionTwoPossible==true) //if action 2 possible, execute
            {
                //Fait apparaître 3 pics d’ombres aléatoirement sur la map (pas sur case de fosse/mur). Au bout de 3 secondes, s'il n’est pas tué, le pic se transforme en squelette et donne 10 PV au boss. Le pic a 20 PV. Cooldown de 10 secondes
                ShadowSpikesAttack(3);
                actionTwoPossible=false;
            } 
            if(actionOnePossible==true) //elif action 1 possible, execute
            {
                //Spawn 10 squelettes et réanime 10 ennemis aléatoires. Cooldown de 5 secondes. Si pas assez de monstres à réanimer, il spawn les squelettes puis lance une boule d’énergie
                SpawnMobs(10);
                FindTombstones();
                if(activeTombstones.Length<10)
                {
                    BlastAttack();
                } else 
                {
                    ReanimateMobs(5);
                }
                StartCoroutine(CooldownTimer(mediumCooldown,1));
                actionOnePossible=false;
            } else {
                //do something ?
            }

        }

    }

    //This function, used at various points by the boss, checks the number of dead ennemies :
    private void FindTombstones()
    {
        activeTombstones=GameObject.FindGameObjectsWithTag("Tombstone");
    }

    //This function, used by the boss, handles the various cooldowns :
    private IEnumerator CooldownTimer(float cooldown, int actionBool)
    {
        yield return new WaitForSeconds(cooldown);
        if(actionBool==1)
        {actionOnePossible=true;}
        else{actionTwoPossible=true;} 
    }

    //This function spawns a given amount of mobs around the boss :
    private void SpawnMobs(int mobsNumber)
    {
        for(int i=0;i<mobsNumber;i++)
        {
            GameObject new_skeleton= Instantiate(skeleton);
            skeleton.transform.position=gameObject.transform.position;
        }

    }

    //This function reanimates a given number of mobs :
    private void ReanimateMobs(int mobsNumber)
    {
        int reanimatedMobsNumber=0; //we count the number of mobs reanimated by the function so far
        foreach(GameObject tombstone in activeTombstones) //we reanimate mobs
        {
            if(reanimatedMobsNumber<mobsNumber) //unlike the reanimateAll, we need to limit the number of reanimated mobs :
            {
                GameObject new_skeleton= Instantiate(skeleton);
                new_skeleton.transform.position=tombstone.transform.position;
                Destroy(tombstone);
                new_skeleton.GetComponent<Soldier>().can_be_revived=false;
                reanimatedMobsNumber+=1;
            }
        }

    }

    //This function reanimates all the mobs present when it's called :
    private void ReanimateAll()
    {
        FindTombstones(); //we start by finding all active tombstones on the map
        foreach(GameObject tombstone in activeTombstones) //we reanimate mobs on every available tombstones
        {
            GameObject new_skeleton= Instantiate(skeleton);
            new_skeleton.transform.position=tombstone.transform.position;
            new_skeleton.GetComponent<Soldier>().can_be_revived=false;
            Destroy(tombstone);
        }

    }

    //This function launches a ball of energy :
    private void BlastAttack()
    {
        GameObject new_attack = Instantiate(energyBall);
        new_attack.transform.position=attackPosition.transform.position;
        new_attack.GetComponent<Rigidbody>().AddForce(transform.forward*firingSpeed); //Unlike the warrior's basic attack, we give the shuriken a forward momentum
    }

    //This function spawns a given amount of spikes :
    private void ShadowSpikesAttack(int spikesNumber)
    {
        //We start by finding 3 random locations suitable
        GameObject new_spike = Instantiate(spike);
        new_spike.transform.position=transform.position+new Vector3(1,1,0); //PLACEHOLDER
    }

    //This function heals the boss for a given amount :
    //It is called either by the shadow spikes or by actions within BossController
    public void Heal(int HealthNumber)
    {
        if(currentHP+HealthNumber>maxHP) //the healing cannot exceed the set maxHP
        {
            currentHP=maxHP;
        }
        else
        {
            currentHP=currentHP+HealthNumber;
        }
        bossLifeChanged?.Invoke(currentHP / initialMaxHP);
    }
}
