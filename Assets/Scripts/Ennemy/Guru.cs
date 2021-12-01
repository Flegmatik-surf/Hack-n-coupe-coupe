using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guru : Ennemy
{
    private float rangeBuff;
    bool m_Started;
    public LayerMask m_LayerMask;
    private float buffSpeed = 1.5f;
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
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position+ new Vector3(1,0,0), transform.localScale *1000, Quaternion.identity, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            
            if (hitColliders[i].tag == "Ennemy")
            {
                hitColliders[i].GetComponent<Ennemy>().SetSpeed(GetSpeed()*buffSpeed);
                print("!!!!!!!!!Hit : " + hitColliders[i].name + i); 
                print(hitColliders[i].GetComponent<Ennemy>().GetSpeed());

            }
            
            i++;
        }
        
    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        if (m_Started)
            
            Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(1, 0, 0), transform.localScale * 10);
    }

}
