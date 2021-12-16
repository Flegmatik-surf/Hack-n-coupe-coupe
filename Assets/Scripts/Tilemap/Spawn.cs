using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    private GameObject Player;
    private Fader fader;
    [SerializeField] float time;


    private void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

    }



    public IEnumerator RespawnPlayer(Vector3 spawn)
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

