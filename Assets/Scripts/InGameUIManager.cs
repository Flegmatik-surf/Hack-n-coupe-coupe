using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameUIManager : MonoBehaviour
{
    //donn?es contenant les sprites propres ? chaque perso
    [SerializeField] private GlobalCharacterData data;

    //les r?f?rences des textes ? modifier dynamiquement 
    [SerializeField] private TMP_Text inGameTimerText;
    [SerializeField] private TMP_Text inGameWaveIndicatorText;

    [SerializeField] private TMP_Text victoryTimerText;
    [SerializeField] private TMP_Text defeatTimerText;
    [SerializeField] private TMP_Text defeatWaveIndicatorText;

    //R?f?rences des ?l?ments graphiques ? modifier dynamiquement
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private Image actionIcon1;
    [SerializeField] private Image actionIcon2;
    [SerializeField] private Image actionIcon3;


    //les diff?rents ?l?ments graphiques ? activer/d?sactiver
    [SerializeField] private GameObject victoryPopUp;
    [SerializeField] private GameObject defeatPopUp;
    [SerializeField] private GameObject inGameInfo;
    [SerializeField] private GameObject bossHealthBar;

    private float gameTimer;
    private string currentWave;




    private void Awake()
    {
        gameTimer = 0f;
        playerHealthSlider.value = 1f;
        victoryPopUp.SetActive(false);
        defeatPopUp.SetActive(false);
        inGameInfo.SetActive(true);
        //TODO : impl?menter (apr?s qu'henri ait fini) les events pour bossHP
        GameController.newWaveSignal += OnNewWave;
        LifeManager.playerHPChanged += OnPlayerHPChanged;
        GameController.victorySignal += OnVictory;
        LifeManager.defeatSignal += OnDefeat;
        Fosse.dieOnFosse += OnDefeat;
        Presets();
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
        inGameTimerText.text = $"Time : {TimerToString(gameTimer): 0s}"; 
    }




    //---------------- OnEvents functions -------------------

    private void OnNewWave(string waveIndicator)
    {
        currentWave = waveIndicator;
        inGameWaveIndicatorText.text = $"Wave : {waveIndicator: 0}";
    }

    private void OnPlayerHPChanged(float HealthRate)
    {
        playerHealthSlider.value = HealthRate;
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
        defeatWaveIndicatorText.text = $"You died on wave : {currentWave: 0}";
    }

    private void OnActionOneCalled(float cooldown)
    {
        StartCoroutine(ReloadIcon(actionIcon1, cooldown));
    }

    private void OnActionTwoCalled(float cooldown)
    {
        StartCoroutine(ReloadIcon(actionIcon2, cooldown));
    }

    private void OnActionThreeCalled(float cooldown)
    {
        StartCoroutine(ReloadIcon(actionIcon3, cooldown));
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





//---------------- Auxilary functions -------------------

    private void Presets()
    {
        String selectedPlayer = PlayerPrefs.GetString("selectedPlayer");

        if (selectedPlayer == "Samourai")
        {
            SamouraiController.samouraiActionOneCalled += OnActionOneCalled;
            SamouraiController.samouraiActionTwoCalled += OnActionTwoCalled;
            SamouraiController.samouraiActionThreeCalled += OnActionThreeCalled;
            actionIcon1.sprite = data.samourai.actionOneIcon;
            actionIcon2.sprite = data.samourai.actionTwoIcon;
            actionIcon3.sprite = data.samourai.actionThreeIcon;
        }
        if (selectedPlayer == "Warrior")
        {
            WarriorController.warriorActionOneCalled += OnActionOneCalled;
            WarriorController.warriorActionTwoCalled += OnActionTwoCalled;
            WarriorController.warriorActionThreeCalled += OnActionThreeCalled;
            actionIcon1.sprite = data.warrior.actionOneIcon;
            actionIcon2.sprite = data.warrior.actionTwoIcon;
            actionIcon3.sprite = data.warrior.actionThreeIcon;
         
        }
        if (selectedPlayer == "Archer")
        {
            ArcherController.bowmanActionOneCalled += OnActionOneCalled;
            ArcherController.bowmanActionTwoCalled += OnActionTwoCalled;
            ArcherController.bowmanActionThreeCalled += OnActionThreeCalled;
            actionIcon1.sprite = data.bowman.actionOneIcon;
            actionIcon2.sprite = data.bowman.actionTwoIcon;
            actionIcon3.sprite = data.bowman.actionThreeIcon;
        }
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

    private IEnumerator ReloadIcon(Image icon, float cooldown)
    {
        icon.fillAmount = 0;
        float fps = 60;
        float deltaAmount = (1 / cooldown) * 1/fps ;

        while (icon.fillAmount < 1)
        {
            icon.fillAmount += deltaAmount;
            yield return new WaitForSeconds(1/fps);
        }
        yield return null;
    }
}
