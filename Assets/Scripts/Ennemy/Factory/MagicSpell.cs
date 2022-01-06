using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    private GameObject player;
    private LifeManager playerLife;
    Vector3 target;

    private float speed = 0.1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerLife = player.GetComponent<LifeManager>();

        //transform.forward = player.transform.position-transform.position;
        //rbArrow.velocity = speed * (player.transform.position - transform.position).normalized;
        target = player.transform.position - transform.position;
        target.y = 0;

    }

    private void LateUpdate()
    {
        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.Translate(speed * target.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            playerLife.TakeDamage(10);
        }
        Destroy(this.gameObject);
    }


}
