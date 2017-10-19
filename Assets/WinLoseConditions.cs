using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseConditions : MonoBehaviour 
{
    public float timer;
    public float setTimer = 10f;

    public TimerScript timerScript;

	void Start () 
    {
        timer = setTimer; // Count-down timer's value is set to desired amount of time at the beginning of the level
	}

	void Update () 
    {
        CharacterDeath();

        if(timerScript.hasStarted == true) // After the [hasStarted] boolean from the TimerScript is set to true, start [TimesUp] function.
        {
            TimesUp();
        }
	}

    void TimesUp()
    {
        timer -= Time.deltaTime;

        if(timer <= 0) // After count-down timer reaches "0" change scene to [LoseScene]
        {
            GetComponent<ChangeSceneScript>().ChangeScenes(1); 
        }
    }

    void CharacterDeath()
    {
        if(PlayerScript.Instance.health <= 0) //After character health reaches "0" change scene to [LoseScene]
        {
            GetComponent<ChangeSceneScript>().ChangeScenes(1);
        }
    }
}
