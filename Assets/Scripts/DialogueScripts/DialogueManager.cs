using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
    [Header("Variables")]
    public int bsIndex = 0; // BeginningScene Index count.
    public int feIndex = 0; // First Encounter Index count.

    [Header("Text Settings")]
    public Text dialogue;   
    public Text countDown;
    public List<string> beginningScene; // List of dialogues used at the beginning of the game.
    public List<string> firstEncounter; // List of dialogues used for every FIRST encounter with an object.

    [Header("Game Objects")]
    public GameObject dialogueBox;
    public GameObject ttcText; // ttc = touch-to-continue
    public GameObject[] popUps; // An array of GameObjects for pop-up UIs (i.e HealthBar, Time-line).

    [Header("Timers")]
    public float timer; 
    public float showTimer;
    public float cdTimer; 
    public float setTime;

    [Header("Booleans")]
    public bool[] objectSeen;
    public bool initTimer, initDialogue, startCD;

    [Header("HighLight Variables")]
    private Material defaultMat; // Game Objects' default material.
    private Material childDefMat; // Game Objects' children default material.
    public Material highlightMat; // Highlight material.

    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        timer = showTimer; //Set Timers to desired amount.
        cdTimer = setTime;

        for(int i = 0; i < objectSeen.Length; i++) // Have player ever seen these objects? No. So all booleans are set to false;
        {
            objectSeen[i] = false;
        }

        initTimer = false;      //Initiate timer is set to false.
        initDialogue = false;   //Initial dialogue once scene is loaded is only played once.
        startCD = false;        //Timer for CountDownTimer is set to false.
    }

    void Update()
    {
        if(initTimer == true) //Timer only starts counting down after player sees an object for the first time
        {
            timer -= Time.deltaTime;
        }

        if(startCD == true) // Starts counting down UI after beginning dialogue is completed.
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
            popUps[1].SetActive(true); //Enable HealthBar/Reputation Prefab.   
        }

        if(bsIndex >= beginningScene.Count) //Once beginning dialogue is done, :
        {
            dialogueBox.SetActive(false); //Dialogue Box UI is disabled.
            initDialogue = true; //Set initialDialogue boolean to true.
            startCD = true; //Start counting-down.
        }
    }

    public void WinSceneDialogue() // Call this function at win scene, may not be nessecary if we transition to win scene.
    {                              // DialogueManager.Instance.WinSceneDialogue();
        dialogueBox.SetActive(true);
        dialogue.text = "Proceed to the delivery mission";
    }

    public void LoseSceneDialogue() // Call this function when player loses the game either by dying or by failing to reach end point in time, may not be nessecary if we transition to lose scene.
    {                               // DialogueManager.Instance.LoseSceneDialogue();
        dialogueBox.SetActive(true); 
        dialogue.text = "Another failure";
    }

    public void FirstEncounterDialogue() // Displays a brief run-down of the objects during player's first encounter with it.
    {

        for(int i = 0; i < objectSeen.Length; i++)
        {
            if(FirstEncounterScript.Instance.seenObj[i] == true && objectSeen[i] == false) //seenObj = Is player currently looking at an object?
            {                                                                              //objectSeen = Have player already seen this object before?
                dialogueBox.SetActive(true); // Enable dialogue box.
                dialogue.text = firstEncounter[feIndex + i]; //Displays text based on which enemy it is (feIndex);
                initTimer = true;  


                defaultMat= FirstEncounterScript.Instance.objects[i].GetComponent<MeshRenderer>().material; // Set objects' default material to it's current material.
                //childDefMat = objects[i].GetComponentInChildren<MeshRenderer>().material; // Set childrens' default material to it's current material.
                FirstEncounterScript.Instance.objects[i].GetComponent<MeshRenderer>().material = highlightMat; // Set objects' material to "highlightMat".
                //objects[i].GetComponentInChildren<MeshRenderer>().material = highlightMat; // Set objects' children material to "highlightMat".

                objectSeen[i] = true; // Mark particular object as seen.
                ttcText.SetActive(false); // Disable touch-to-continue text.
            }
        }
    }

    void CountDownTimer() //Countdown UI
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
