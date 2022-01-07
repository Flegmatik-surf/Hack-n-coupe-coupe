using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreenContainer;
    [SerializeField] private GameObject settingsScreenContainer;
    [SerializeField] private GameObject selectCharacterScreenContainer;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer sfxAudioMixer;
    private float globalVolumeFactor = 0.8f;
    [SerializeField] private float deltaVolume = 70f; //penalty volume applied when global volume = 0

    [SerializeField] private int[] sceneIndexs = { 1 };
    AudioSource audioSource;
    Resolution[] resolutions;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        print(audioSource);

        int startResolution = 0;
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.height)
            {
                startResolution = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = startResolution;
        resolutionDropdown.RefreshShownValue();
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




    public void SetGraphicsQuality(int qualityIndex)
    {
        Debug.Log("quality : " + qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionInt)
    {
        Resolution resolution = resolutions[resolutionInt];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicAudioMixer.SetFloat("mainVolume", musicVolume - deltaVolume * (1 - globalVolumeFactor) );
    }

    public void SetSFXVolume(float sfxVolume)
    {
        sfxAudioMixer.SetFloat("sfxVolume", sfxVolume - deltaVolume * (1 - globalVolumeFactor));
    }

    public void SetGeneralVolume(float newGlobalVolumeFactor)
    {
        float oldGlobalVolumeFactor = globalVolumeFactor;
        globalVolumeFactor = newGlobalVolumeFactor;

        //update directly the new global menu :
        float oldSFXVolume;
        float oldMusicVolume;
        sfxAudioMixer.GetFloat("sfxVolume", out oldSFXVolume);
        sfxAudioMixer.SetFloat("sfxVolume", oldSFXVolume + deltaVolume * (globalVolumeFactor - oldGlobalVolumeFactor));
        musicAudioMixer.GetFloat("mainVolume", out oldMusicVolume);
        musicAudioMixer.SetFloat("mainVolume", oldMusicVolume + deltaVolume * (globalVolumeFactor - oldGlobalVolumeFactor));

    }



    public void PlayWithCharacter(string character)
    {
        audioSource.Play();
        PlayerPrefs.SetString("selectedPlayer", character);
        UnityEngine.SceneManagement.SceneManager.LoadScene(RandomScene());
    }

    public void QuitGame()
    {
        audioSource.Play();
        Debug.Log("On quitte le jeu !");
        Application.Quit();
    }


    //--------------- auxilary functions ------------------
    private int RandomScene()
    {
        int n = sceneIndexs.Length;
        return sceneIndexs[Random.Range(0, n)];
    }

}
