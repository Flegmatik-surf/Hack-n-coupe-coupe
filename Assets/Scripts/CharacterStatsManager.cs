using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsManager : MonoBehaviour
{
    [SerializeField] private TMP_Text maxWaveText;
    [SerializeField] private TMP_Text nbOfGamesText;
    [SerializeField] private GameObject medalImage;
    [SerializeField] private string characterName;


    private void Awake()
    {
        LoadAndWriteStats();
    }

    private void LoadAndWriteStats()
    {
        string keyWave = characterName + " : maxWave";
        string keyNbGames = characterName + " : games";
        string keyWin = characterName + " : win";

        maxWaveText.text = $"Max Wave : {PlayerPrefs.GetInt(keyWave) : 0}";
        nbOfGamesText.text = $"Played Games : {PlayerPrefs.GetInt(keyNbGames): 0}";
        if(PlayerPrefs.GetInt(keyWin) == 1)
        {
            medalImage.SetActive(true);
        } else {
            medalImage.SetActive(false);
        }
    }

    
}
