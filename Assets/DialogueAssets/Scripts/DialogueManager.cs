using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
    [Header("Variables")]
    public int bsIndex = 0;
    public int feIndex = 0;
    public Text dialogue;       
    public Text countDown;
    public GameObject dialogueBox;
    public GameObject ttcText;
    public float timer;
    public float showTimer;
    public float cdTimer;
    public float setTime;
    public List<string> beginningScene;
    public List<string> firstEncounter;
    public GameObject[] popUps;
    public bool[] objectSeen;
    public bool initTimer, initDialogue, startCD, gameWin, gameLose;

    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = showTimer;
        cdTimer = setTime;

        for(int i = 0; i < objectSeen.Length; i++) // Have player ever seen these objects? No. So all booleans are set to false;
        {
            objectSeen[i] = false;
        }

        initTimer = false; 
        initDialogue = false;
        startCD = false;
    }

    void Update()
    {
        if(initTimer == true) //Timer only starts counting down after player sees an object for the first time
        {
            timer -= Time.deltaTime;
        }

        if(startCD == true)
        {
            cdTimer -= Time.deltaTime;
            CountDownTimer();
        }

        if(timer <= 0) //Disable dialogue box after timer expires
        {
            dialogueBox.SetActive(false);
            timer = showTimer;
            initTimer = false;
        }

        if(!initDialogue) //Displays Beginning Dialogue once
        {
            BeginningSceneDialogue();
        }
            
        FirstEncounterDialogue(); 

        if(Input.GetKeyDown(KeyCode.A))
        {
            popUps[3].SetActive(true);
            popUps[4].SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            popUps[3].SetActive(false);
            popUps[4].SetActive(true);
        }
    }

    public void BeginningSceneDialogue() // Displays Dialogue in the beginning of the game 
    {
        dialogueBox.SetActive(true);
        ttcText.SetActive(true);
        dialogue.text = beginningScene[bsIndex];

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && bsIndex <= beginningScene.Count|| Input.GetKeyDown(KeyCode.Space))
        {
            bsIndex++;
        }

        if(bsIndex == 1)
        {
            popUps[0].SetActive(true); //Enable TimeLine Prefab.
        }

        if(bsIndex == 2)
        {
            popUps[1].SetActive(true); //Enable HealthBar Prefab.   
        }

        if(bsIndex >= beginningScene.Count)
        {
            dialogueBox.SetActive(false);
            initDialogue = true;
            startCD = true;
        }
    }

    public void WinSceneDialogue() // Call this function at win scene
    {                              // DialogueManager.Instance.WinSceneDialogue();
        dialogueBox.SetActive(true);
        dialogue.text = "Proceed to the delivery mission";
    }

    public void LoseSceneDialogue() // Call this function when player loses the game either by dying or by failing to reach end point in time.
    {                               // DialogueManager.Instance.LoseSceneDialogue();
        dialogueBox.SetActive(true);
        dialogue.text = "Another failure";
    }

    public void FirstEncounterDialogue() // Displays a brief run-down of the objects during player's first encounter with it.
    {
        if(FirstEncounterScript.Instance.seenObj[0] == true && objectSeen[0] == false) //seenObj = Player is currently see-ing an object.
        {                                                                              //objectSeen = Have player already seen this object before?
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

    void CountDownTimer() //Countdown
    {
        popUps[2].SetActive(true);
   
        if(cdTimer <= 4 && cdTimer > 3)
            countDown.text = "!";
        else if(cdTimer <= 3 && cdTimer > 2)
            countDown.text = "3";
        else if(cdTimer <= 2 && cdTimer > 1)
            countDown.text = "2";
        else if(cdTimer <= 1 && cdTimer > 0)
            countDown.text = "1";
        else if(cdTimer <= 0)
        {
            popUps[2].SetActive(false);
            startCD = false;
            cdTimer = 0;
        }
    }
}
