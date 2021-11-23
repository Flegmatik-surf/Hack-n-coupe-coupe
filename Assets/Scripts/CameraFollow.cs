using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance, height;

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z - distance);
        transform.LookAt(player);
    }


}
