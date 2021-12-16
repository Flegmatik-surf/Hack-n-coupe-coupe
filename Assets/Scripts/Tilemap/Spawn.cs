using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    [SerializeField] GameObject Player;
    private GameObject floor;
    private float yFloor;
    private Fader fader;
    [SerializeField] float time;


    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        //if (!Player) return; 
        //spawn = transform.position;

        floor = GameObject.FindGameObjectWithTag("Floor");
        yFloor = floor.transform.position.y;
    }



    private IEnumerator RespawnPlayer(Vector3 spawn)
    {
        print("respawwwn");
        print(spawn);
        Fader fader = FindObjectOfType<Fader>();
        yield return fader.FadeOut(time);
        Instantiate(Player, spawn, Quaternion.identity);

        yield return fader.FadeIn(time);

    }


    public void StartPlayerRespawn(Vector3 spawn)
    {
        StartCoroutine(RespawnPlayer(spawn));
    }
}

