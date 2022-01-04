using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public static void PlaySound()
    {
        GameObject SoundGameObject = new GameObject("Sound");
        AudioSource audioSource = SoundGameObject.AddComponent<AudioSource>();
        
    }
}
