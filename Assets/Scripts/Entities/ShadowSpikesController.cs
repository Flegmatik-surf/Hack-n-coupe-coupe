using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is used by the shadowspikes : once spawned, they wait till they die. 
//If they're not killed by the player before then, they heal the boss (10 pv) and spawn a skeleton
public class ShadowSpikesController : MonoBehaviour
{
    //the timer used by the shadow spike :
    [SerializeField] private float timer;

    //the spawned mob
    [SerializeField] private GameObject skeleton;

    //variables relatives à la healthbar :
    [SerializeField] private GameObject healthBarUI;
    [SerializeField] private Slider slider;
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    public void Start() 
    {
        StartCoroutine(LifeTimerCoroutine(timer)); //We initiate the life of the spike
    }

    //This function deals with the spike being damaged
    //it handles both the calculus and the lifebar itself
    public void TakeDamage(float damage)
    {
        healthBarUI.SetActive(true);
        currentHP -= damage;
        slider.value=currentHP/maxHP; 
        if(currentHP<=0) //when the spike dies
        {
            Destroy(gameObject);
        }
    }

    //This function spawns the skeleton upon the spike's "death" by timer
    private void SpawnSkeleton()
    {
        GameObject new_skeleton = Instantiate(skeleton);
        new_skeleton.transform.position=transform.position;
    }

    //This function heals the boss
    private void HealBoss()
    {
        GameObject boss = GameObject.Find("Boss");
        boss.GetComponent<BossController>().Heal(10);
    }

    //the timer function :
    private IEnumerator LifeTimerCoroutine(float timer)
    {
        yield return new WaitForSeconds(timer);
        SpawnSkeleton();
        //HealBoss();
        Destroy(gameObject);
    }
}
