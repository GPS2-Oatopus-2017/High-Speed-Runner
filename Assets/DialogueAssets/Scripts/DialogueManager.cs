using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueState
{
    BEGINNING = 0,
    WIN,
    LOSE,
    FIRST_ENCOUNTER
}

public class DialogueManager : MonoBehaviour 
{
    [Header("Variables")]
    public int bsIndex = 0;
    public int feIndex = 0;
    public Text dialogue;       
    public List<string> beginningScene;
    public List<string> firstEncounter;
    public GameObject dialogueBox;
    public GameObject ttcText;
    public float timer;
    public float showTimer;
    public bool[] objectSeen;
    public bool initTimer, initDialogue;

    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = showTimer;

        for(int i = 0; i < objectSeen.Length; i++)
        {
            objectSeen[i] = false;
        }

        initTimer = false;
        initDialogue = false;
    }

    void Update()
    {
        if(initTimer == true)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0)
        {
            dialogueBox.SetActive(false);
            timer = showTimer;
            initTimer = false;
        }

        if(!initDialogue)
        {
            BeginningSceneDialogue();
        }
            
        FirstEncounterDialogue();
    }

    public void BeginningSceneDialogue() // Displays Dialogue in the beginning of the game 
    {
        dialogueBox.SetActive(true);
        ttcText.SetActive(true);
        dialogue.text = beginningScene[bsIndex];
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bsIndex++;
        }

        if(bsIndex >= beginningScene.Count)
        {
            dialogueBox.SetActive(false);
            initDialogue = true;
        }
    }

    public void WinSceneDialogue() // Call this function at win scene
    {
        dialogueBox.SetActive(true);
        dialogue.text = "Proceed to the delivery mission";
    }

    public void LoseSceneDialogue() // Call this function at lose scene
    {
        dialogueBox.SetActive(true);
        dialogue.text = "Another failure";
    }

    public void FirstEncounterDialogue() // Displays a brief run-down of the objects during player's first encounter with it.
    {
        if(FirstEncounterScript.Instance.seenObj[0] == true && objectSeen[0] == false)
        {
            dialogueBox.SetActive(true);
            dialogue.text = firstEncounter[feIndex];
            initTimer = true;

            objectSeen[0] = true;
            ttcText.SetActive(false);
        }

        else if(FirstEncounterScript.Instance.seenObj[1] == true && objectSeen[1] == false)
        {
            dialogueBox.SetActive(true);
            dialogue.text = firstEncounter[feIndex + 1];
            initTimer = true;

            objectSeen[1] = true;
            ttcText.SetActive(false);
        }

        else if(FirstEncounterScript.Instance.seenObj[2] == true && objectSeen[2] == false)
        {
            dialogueBox.SetActive(true);
            dialogue.text = firstEncounter[feIndex + 2];
            initTimer = true;

            objectSeen[2] = true;
            ttcText.SetActive(false);
        }

        else if(FirstEncounterScript.Instance.seenObj[3] == true && objectSeen[3] == false)
        {
            dialogueBox.SetActive(true);
            dialogue.text = firstEncounter[feIndex + 3];
            initTimer = true;

            objectSeen[3] = true;
            ttcText.SetActive(false);
        }

        else if(FirstEncounterScript.Instance.seenObj[4] == true && objectSeen[4] == false)
        {
            dialogueBox.SetActive(true);
            dialogue.text = firstEncounter[feIndex + 4];
            initTimer = true;

            objectSeen[4] = true;
            ttcText.SetActive(false);
        }
    }
}
