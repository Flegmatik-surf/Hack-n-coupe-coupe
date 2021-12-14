using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guru : Ennemy
{
    private float rangeBuff;
    bool m_Started;
    public LayerMask m_LayerMask;
    //buffSpeed
    private float buffSpeed = 1.5f; 
    private float buffSpeedTime = 5f;
   


    public override void Attack()
    {

    }

    

    void Awake()
    {
        
        m_Started = true;
    }

    void FixedUpdate()
    {
        MyCollisions();

    }

    void MyCollisions()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale *10, Quaternion.identity, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            
            if (hitColliders[i].tag == "Ennemy")
            {
                Ennemy ennemy = hitColliders[i].GetComponent<Ennemy>();
                ennemy.GetBuffSpeed(buffSpeed, buffSpeedTime);
                print("!!!!!!!!!Hit : " + hitColliders[i].name + i);
                
                

            }
            
            i++;
        }
        
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        if (m_Started)
            
            Gizmos.DrawWireCube(gameObject.transform.position, transform.localScale * 10);
    }

}
