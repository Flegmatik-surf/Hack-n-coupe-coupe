using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float rangeIn;
    private bool Inrange { get { return Vector3.Distance(transform.position, player.position) < rangeIn; } }
    [SerializeField] private float rangeOut;

    private void Update()
    {
        Chase();
    }

    private void Chase()
    {
        if (Inrange)
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
}
