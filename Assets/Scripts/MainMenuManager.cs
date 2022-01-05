using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreenContainer;
    [SerializeField] private GameObject settingsScreenContainer;
    [SerializeField] private GameObject selectCharacterScreenContainer;
    [SerializeField] private int startScene;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        print(audioSource);
    }

    public void GoToSettingsScreen()
    {
        audioSource.Play();
        titleScreenContainer.SetActive(false);
        settingsScreenContainer.SetActive(true);
        selectCharacterScreenContainer.SetActive(false);
    }

    public void GoToTitleScreen()
    {
        audioSource.Play();
        titleScreenContainer.SetActive(true);
        settingsScreenContainer.SetActive(false);
        selectCharacterScreenContainer.SetActive(false);
    }

    public void GoToSelectScreen()
    {
        audioSource.Play();
        titleScreenContainer.SetActive(false);
        settingsScreenContainer.SetActive(false);
        selectCharacterScreenContainer.SetActive(true);
    }

    public void PlayWithCharacter(string character)
    {
        audioSource.Play();
        PlayerPrefs.SetString("selectedPlayer", character);
        UnityEngine.SceneManagement.SceneManager.LoadScene(startScene);
    }

    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }
}
