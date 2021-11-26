using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    [SerializeField] private float distance, height;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + height, playerTransform.position.z - distance);
        transform.LookAt(playerTransform);
    }


}
