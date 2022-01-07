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
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider globalVolumeSlider;
    [SerializeField] private AudioMixer musicAudioMixer;
    [SerializeField] private AudioMixer sfxAudioMixer;
    private float globalVolume = 0.8f;
    private int defaultResolution;


    [SerializeField] private int[] sceneIndexs = { 1 };
    AudioSource audioSource;
    Resolution[] resolutions;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        print(audioSource);

        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.height)
            {
                defaultResolution = i;
            }
        }
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        ApplyDefaults();
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
        musicAudioMixer.SetFloat("mainVolume", musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        sfxAudioMixer.SetFloat("sfxVolume", sfxVolume);
    }

    public void SetGeneralVolume(float newGlobalVolume)
    {
        globalVolume = newGlobalVolume;

        sfxSlider.value = globalVolume;
        musicSlider.value = globalVolume;
        sfxAudioMixer.SetFloat("sfxVolume", globalVolume);
        musicAudioMixer.SetFloat("mainVolume", globalVolume);

    }

    public void SetToDefaults()
    {
        ApplyDefaults();
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

    private void ApplyDefaults()
    {
        globalVolume = 0f;
        globalVolumeSlider.value = 0f;
        musicAudioMixer.SetFloat("mainVolume", 0f);
        musicSlider.value = 0f;
        sfxAudioMixer.SetFloat("sfxVolume", 0f);
        sfxSlider.value = 0f;

        resolutionDropdown.value = defaultResolution;
        resolutionDropdown.RefreshShownValue();
        SetResolution(defaultResolution);
        qualityDropdown.value = 1;
        qualityDropdown.RefreshShownValue();
        SetGraphicsQuality(1);

    }

}
