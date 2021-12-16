using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour { 

    public float maxHP = 50f;
    public float currentHP = 50f;
    GameObject spawn;
    // Start is called before the first frame update
    void Start()
    {
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
        currentHP -= damage;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemy")
        {
            //dosmth
        }
    }
}
