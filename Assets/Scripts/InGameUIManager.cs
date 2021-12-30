using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    //les r?f?rences des textes ? modifier dynamiquement 
    [SerializeField] private TMP_Text inGameTimerText;
    [SerializeField] private TMP_Text inGameWaveIndicatorText;

    [SerializeField] private TMP_Text victoryTimerText;
    [SerializeField] private TMP_Text defeatTimerText;
    [SerializeField] private TMP_Text defeatWaveIndicatorText;

    //les diff?rents ?l?ments graphiques ? activer/d?sactiver
    [SerializeField] private GameObject victoryPopUp;
    [SerializeField] private GameObject defeatPopUp;
    [SerializeField] private GameObject inGameInfo;

    private float gameTimer;




    private void Awake()
    {
        gameTimer = 0f;
        victoryPopUp.SetActive(false);
        defeatPopUp.SetActive(false);
        inGameInfo.SetActive(true);
        GameController.newWaveSignal += OnNewWave;
        GameController.victorySignal += OnVictory;
        LifeManager.defeatSignal += OnDefeat;
        Fosse.dieOnFosse += OnDefeat;
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
        inGameTimerText.text = $"Time : {TimerToString(gameTimer): 0s}"; 
    }



    private void OnNewWave(string waveIndicator)
    {
        inGameWaveIndicatorText.text = $"Wave : {waveIndicator: 0}";
    }

    private void OnVictory()
    {
        victoryPopUp.SetActive(true);
        defeatPopUp.SetActive(false);
        inGameInfo.SetActive(false);
        victoryTimerText.text = $"Time spent : {TimerToString(gameTimer): 0s}";
    }

    private void OnDefeat()
    {
        victoryPopUp.SetActive(false);
        defeatPopUp.SetActive(true);
        inGameInfo.SetActive(false);
        defeatTimerText.text = $"Time spent : {TimerToString(gameTimer): 0s}";
        //defeatWaveIndicatorText.SetText(inGameWaveIndicatorText.GetParsedText());
    }

    public void GoToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
        //TODO : randomizer to implement
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }





    private string TimerToString(float timer)
    {
        string result = "";
        int hours = Math.DivRem((int)timer, 3600, out int remainder);
        Debug.Log(hours + "h " + remainder + "s");
        if (hours > 0)
        {
            result += hours + "h ";
        }

        int minutes = Math.DivRem(remainder, 60, out int seconds);
        if (minutes > 0)
        {
            result += minutes + "m ";
        }

        result += seconds + "s";
        return result;

    }
}
