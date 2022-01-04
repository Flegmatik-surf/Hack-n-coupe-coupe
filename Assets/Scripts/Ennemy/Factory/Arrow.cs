using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;
    private LifeManager playerLife;
    Vector3 target;

    private float speed=1f;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLife = player.GetComponent<LifeManager>();
        //transform.rotation = new Quaternion(90, 90, 180, 0);
        //transform.forward = player.transform.position-transform.position;
        //rbArrow.velocity = speed * (player.transform.position - transform.position).normalized;
        target = player.transform.position - transform.position;
        target = target.normalized;
        target.y = 0;
        if (target.x > 0)
        {
            transform.Rotate(0, 0, -Mathf.Abs(((180 / Mathf.PI) * Mathf.Acos(target.z))));
        }
        else {
            transform.Rotate(0, 0, Mathf.Abs(((180 / Mathf.PI) * Mathf.Acos(target.z))));
        }
        
        //transform.Rotate(Mathf.PI/2,0,0);
        //transform.rotation = new Quaternion(90,0,90,0);
    }

    private void LateUpdate()
    {


        //transform.rotation = new Quaternion(target.normalized.x, target.normalized.y, target.normalized.z, 90);
        //transform.Translate(speed * target.normalized);
        
        transform.Translate(speed * new Vector3(0,1,0));
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(10);
        }
        if(collision.gameObject.tag=="Wall")
        {
            Destroy(gameObject);
        }
    }


}
