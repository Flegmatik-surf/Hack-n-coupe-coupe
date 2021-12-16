using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreenContainer;
    [SerializeField] private GameObject settingsScreenContainer;
    [SerializeField] private GameObject selectCharacterScreenContainer;

    public void GoToSettingsScreen()
    {
        titleScreenContainer.SetActive(false);
        settingsScreenContainer.SetActive(true);
        selectCharacterScreenContainer.SetActive(false);
    }

    public void GoToTitleScreen()
    {
        titleScreenContainer.SetActive(true);
        settingsScreenContainer.SetActive(false);
        selectCharacterScreenContainer.SetActive(false);
    }

    public void GoToSelectScreen()
    {
        titleScreenContainer.SetActive(false);
        settingsScreenContainer.SetActive(false);
        selectCharacterScreenContainer.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }
}
