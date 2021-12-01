using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject player;
    private LifeManager playerLife;
    private Rigidbody rbArrow;

    private float speed=3;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLife = player.GetComponent<LifeManager>();
        rbArrow = GetComponent<Rigidbody>();
        //transform.forward = player.transform.position-transform.position;
        rbArrow.velocity = speed * (player.transform.position - transform.position).normalized;

    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0,0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("oui");
        print(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            print("popopo");
            playerLife.TakeDamage(10);
        }
        //Destroy(this.gameObject);
    }


}
