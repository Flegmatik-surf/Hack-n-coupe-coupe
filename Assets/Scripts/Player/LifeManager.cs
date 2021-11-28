using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour { 

    public float maxHP = 50f;
    public float currentHP = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP < 0)
        {
            print("mdr t nul");
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
            print("aie");
        }
    }
}
