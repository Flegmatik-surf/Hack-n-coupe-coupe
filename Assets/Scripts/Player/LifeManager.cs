using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//this script is used by the player's life system
public class LifeManager : MonoBehaviour { 

    public float maxHP;
    public float currentHP;
    GameObject spawn;

    //variables relatives au UI du joueur :
    private GameObject playerUI;
    private Slider HPslider;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.FindGameObjectWithTag("Respawn");
        playerUI = GameObject.FindGameObjectWithTag("MainCamera");
        HPslider=playerUI.transform.Find("PlayerUI").Find("HPSlider").GetComponent<Slider>();
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
        currentHP -= damage;
        HPslider.value=currentHP/maxHP; //to adjust the lifebar, we just change it's value between 1 (max=current HP) and 0 (ennemy ded)
        if(currentHP<=0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(2);
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
        HPslider.value=currentHP/maxHP;

    }
}
