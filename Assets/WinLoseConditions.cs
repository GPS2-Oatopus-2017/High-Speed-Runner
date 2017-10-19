using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseConditions : MonoBehaviour 
{
    public TimerScript timerScript;

	void Update () 
    {
        CharacterDeath();

        TimesUp();
	}

    void TimesUp()
    {
        if(timerScript.totalTimeLevel1 <= 0) // After count-down timer reaches "0" change scene to [LoseScene]
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
