using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Settings")]
    public GameObject[] menuWindows;
    public string startingLevel;
    public GameObject[] sliders;

    void Start()
    {
        SetupBrightness(sliders[0]);
        SetupBGM(sliders[1]);
        SetupSFX(sliders[2]);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startingLevel); //Start Game
    }

    public void ExitGame()
    {
        Application.Quit(); // Exit the game
        Debug.Log("Exit Game");
    }

    public void OpenMenu(int menu)
    {
        menuWindows[menu].SetActive(true); //Activate Menu
    }

    public void CloseMenu(int menu)
    {
        menuWindows[menu].SetActive(false); //Deactivate Menu
    }

    public void SetupBGM(GameObject slider)  // Will work on this with Syabil soon
    {
        MenuSettings.Instance.bgmVolume  = slider.GetComponent<Slider>().value;
        ChangeBGM(slider);
    }

    public void SetupSFX(GameObject slider)
    {
        MenuSettings.Instance.sfxVolume = slider.GetComponent<Slider>().value;
        ChangeSFX(slider);
    }

    public void ChangeBGM(GameObject slider)
    {
        MenuSettings.Instance.SetBGMVolume(slider.GetComponent<Slider>().value);
        Debug.Log("Current BGM value is : " + slider.GetComponent<Slider>().value);
    }

    public void ChangeSFX(GameObject slider)
    {
        MenuSettings.Instance.SetSFXVolume(slider.GetComponent<Slider>().value);
        Debug.Log("Current SFX value is : " + slider.GetComponent<Slider>().value);
    }

    public void SetupBrightness(GameObject slider) //Set initial screen brightness
    {
        MenuSettings.Instance.brightness = slider.GetComponent<Slider>().value;
        ChangeBrightness(slider);
    }

    public void ChangeBrightness(GameObject slider) //Alter screen brightness value
    {
        MenuSettings.Instance.SetBrightness(slider.GetComponent<Slider>().value);
    }
}
