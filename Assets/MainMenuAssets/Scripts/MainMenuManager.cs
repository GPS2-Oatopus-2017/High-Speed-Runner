using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Settings")]
    public GameObject[] menuWindows;
    public string tutorialLevel;
    public string startingLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(startingLevel); //Start Game
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(tutorialLevel); //Start Tutorial
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

   /* public void SetupBGM(GameObject slider)  // Will work on this with Syabil soon
    {
        slider.GetComponent<Slider>().value = SoundManagerScript.Instance.bgmVolume;
    }

    public void SetupSFX(GameObject slider)
    {
        slider.GetComponent<Slider>().value = SoundManagerScript.Instance.sfxVolume;
    }*/

    /*public void ChangeBGM(GameObject slider)
    {
        SoundManagerScript.Instance.SetBGMVolume(slider.GetComponent<Slider>().value);
    }

    public void ChangeSFX(GameObject slider)
    {
        SoundManagerScript.Instance.SetSFXVolume(slider.GetComponent<Slider>().value);
    }*/

    public void SetupBrightness(GameObject slider) //Set initial screen brightness
    {
        slider.GetComponent<Slider>().value = MenuSettings.Instance.brightness;
    }

    public void ChangeBrightness(GameObject slider) //Alter screen brightness value
    {
        MenuSettings.Instance.SetBrightness(slider.GetComponent<Slider>().value);
    }
}
