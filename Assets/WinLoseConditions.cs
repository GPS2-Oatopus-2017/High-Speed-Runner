using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            DialogueManager.Instance.LoseSceneDialogue();

            if(DialogueManager.Instance.loseIndex >= DialogueManager.Instance.loseDialogue.Count)
            {
                GameObject.FindWithTag("GameManager").GetComponent<ChangeSceneScript>().ChangeScenes(1);
            }
        }
    }

    void CharacterDeath()
    {
        if(PlayerScript.Instance.health <= 0) //After character health reaches "0" change scene to [LoseScene]
        {
            DialogueManager.Instance.LoseSceneDialogue();

            if(DialogueManager.Instance.loseIndex >= DialogueManager.Instance.loseDialogue.Count)
            {
                GameObject.FindWithTag("GameManager").GetComponent<ChangeSceneScript>().ChangeScenes(1);
            }
        }
    }
}
