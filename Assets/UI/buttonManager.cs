using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play()
    {
        SceneManager.LoadScene("gameScene");
    }

    public void quit()
    {
        Application.Quit();
    }

    public void help()
    {
        SceneManager.LoadScene("helpScene");
    }

    public void options()
    {
        SceneManager.LoadScene("optionScene");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("mainMenuScene");
    }

    public void musiucSliderChanger(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
    }

    public void soundEffectSliderChanger(float value)
    {
        PlayerPrefs.SetFloat("sfxVolume", value);
    }

}
