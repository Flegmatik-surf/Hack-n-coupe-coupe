using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used to control the waves of a certain game :
//It works following :
/*
appliqué dans la scène de jeu (puisque celle-ci ne change pas, cf game design doc)
en variable d'entrée, le nom du joueur sélectionné envoyé par le choix des personnages

Start() : instantie le joueur, met les valeurs U0 et U1 aux bons initialisations et lance CalculateWave()
LateUpdate() : va check si tous les ennemis sont morts périodiquement, appelle CalculateWave() et ResetPlayer() si c'est le cas 
CalculateWave(int Un1, int Un2) : calcule le nbre d'ennemis de la vague prochaine et lance CreateWave()
CreateWave(int enemyNumber) : lance la vague en plaçant les ennemis au pif sur leurs points de spawns

*/
public class GameController : MonoBehaviour
{
    //the string containing the type of player selected in the gamelauncher
    public string selectedPlayer;

    //this will contain the active player
    private GameObject player;

    //this will contain the player's UI :
    private GameObject playerUI;
    private Text waveIndicationUI;

    //The three variables containing the three types of player :
    [SerializeField] private GameObject Warrior;
    [SerializeField] private GameObject Samourai;
    [SerializeField] private GameObject Archer;

    //The three variables containing the three types of ennemies :
    [SerializeField] private GameObject Bowman;
    [SerializeField] private GameObject Soldier;
    [SerializeField] private GameObject Guru;
    [SerializeField] private GameObject Boss;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject[] aliveEnemies;
    
    //the variable containing the spawn of the player :
    private GameObject spawn;

    //the variable that will contains all the enemy spawners :
    private GameObject[] enemySpawners;

    //the variable that will contain at a given time all the tombstones dropped by the enemies :
    private GameObject[] tombstones;

    //the variable containing the wave number :
    public int waveIndicator;

    //the variable that will contained the enemy number :
    private int enemyNumber;

    //variables that will be used for the calculateWave :
    private int U1;
    private int U0;

    //events that will be used by the UI
    public static event Action victorySignal;
    public static event Action<string> newWaveSignal;

    //The awake() function serves here to initialize some variables
    private void Awake() 
    {
        //we find the spawn :
        spawn=GameObject.FindGameObjectWithTag("Respawn");
        //we initialise the enemy list :
        enemies.Add(Bowman);
        enemies.Add(Soldier);
        enemies.Add(Guru);
        //we initialise the enemy spawner list :
        enemySpawners=GameObject.FindGameObjectsWithTag("EnnemySpawner");
        selectedPlayer = PlayerPrefs.GetString("selectedPlayer"); //we take the player from the preferences
        Debug.Log("Selected Player : " + selectedPlayer);
    }

    //Launches the first wave and initialize the player :
    private void Start() 
    {
        //we start by selecting the player :
        if(selectedPlayer=="Samourai")
        {
            player = Instantiate(Samourai);
        }
        if(selectedPlayer=="Warrior")
        {
            player = Instantiate(Warrior);
        }
        if(selectedPlayer=="Archer")
        {
            player = Instantiate(Archer);
        }
        player.transform.position=spawn.transform.position;
        
        //we start the first wave :
        enemyNumber=0;
        waveIndicator=1;
        U1=1;
        U0=0;
        CalculateWave(U1,U0);
        playerUI = GameObject.FindGameObjectWithTag("MainCamera");
        waveIndicationUI=playerUI.transform.Find("PlayerUI").Find("WaveIndicator").GetComponent<Text>();
        print("enemies"+enemies.Count);
    }

    //The LateUpdate checks periodically if all the enemies have been killed :
    private void LateUpdate() 
    {
        aliveEnemies=GameObject.FindGameObjectsWithTag("Ennemy");
        if(aliveEnemies.Length==0)
        {
            if(waveIndicator==10)
            {
                EndGame(); //if we reached the final wave and beaten it, we call the EndGame() function
            } else 
            {
                ResetPlayer();
                CalculateWave(U1,U0);
            }
        }
    }

    //the function that will calculate the wave according to the Fibonacci sequence :
    // Un+2 = Un+1 + Un
    // where Un+2 : U2, Un+1 : U1, Un : U0
    private void CalculateWave(int localU1, int localU0)
    {
        enemyNumber = localU1 + localU0; //we calculate the number of ennemies
        StartCoroutine(CreateWave(enemyNumber));
        U1=enemyNumber; //we change the saved values of U1 and U2, for the next time CalculateWave is called by LateUpdate
        U0=localU1;
    }

    //the function that will create the wave :
    private IEnumerator CreateWave(int enemyNumber)
    {
        print("Wave : " + waveIndicator);
        waveIndicationUI.text="Wave : " + waveIndicator;
        newWaveSignal?.Invoke(waveIndicator.ToString());
        if(waveIndicator==10) //if it's the boss' wave :
        {
            int randomSpawnerIndicator = UnityEngine.Random.Range(0,enemySpawners.Length);
            GameObject new_ennemy = Instantiate(Boss);
            new_ennemy.transform.position=enemySpawners[randomSpawnerIndicator].gameObject.transform.position;
        }
        //regardless of the boss or not, we still spawn ennemies :
        for(int i = 0;i<enemyNumber+1;i++)
        {
            int randomSpawnerIndicator = UnityEngine.Random.Range(0,enemySpawners.Length);
            int randomEnnemyIndicator = UnityEngine.Random.Range(0,enemies.Count);
            GameObject new_ennemy = Instantiate(enemies[randomEnnemyIndicator]);
            new_ennemy.transform.position=enemySpawners[randomSpawnerIndicator].gameObject.transform.position;
            yield return new WaitForSeconds(1f);
        }
        waveIndicator+=1;
    }

    //Finally the function that puts the player back to its spawn :
    private void ResetPlayer()
    {
        tombstones=GameObject.FindGameObjectsWithTag("Tombstone");
        for(int i=0;i<tombstones.Length;i++)
        {
            Destroy(tombstones[i]);
        }
        player=GameObject.FindGameObjectWithTag("Player");
        player.transform.position=spawn.transform.position;
        player.gameObject.GetComponent<LifeManager>().Heal(); //we heal the player
    }

    //the function called when the game ends :
    private void EndGame()
    {
        victorySignal?.Invoke();
        print("Game over !");
        //UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        //Application.Quit();
    }

}
