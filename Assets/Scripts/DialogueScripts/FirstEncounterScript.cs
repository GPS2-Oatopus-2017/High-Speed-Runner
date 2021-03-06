﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEncounterScript : MonoBehaviour 
{
    [Header("Lists of Objects in-Scene")]
    public List<GameObject> surveillanceDrones;
    public List<GameObject> motionDetectors;
    public List<GameObject> switches;
    public List<GameObject> electricFences;
    public List<GameObject> doors;

    [Header("HighLight Variables")]
    private Material defaultMat; // Game Objects' default material.
    public Material highlightMat; // Highlight material.

    public float[] distanceFromObject;

    public static FirstEncounterScript Instance;

    public bool[] seenObj; 
   
    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () 
    {
        for(int i = 0; i < seenObj.Length; i++) // Seen Object is all set to false because player have not seen these objects before.
        {
            seenObj[i] = false;
        }
	}

    void Update()
    {
        ObjectEncounter();
    }

    public void ObjectEncounter()
    {
        for(int i = 0; i < surveillanceDrones.Count; i++)
		{
            // Calculates distance of player and obj and decides if it is in range. 
            if (Vector3.Distance(transform.position, surveillanceDrones[i].transform.position)  <= distanceFromObject[0] && seenObj[0] == false)
			{
                seenObj[0] = true; // Player is currently withing range of an obj.

                // Highlight that particular object
				defaultMat= surveillanceDrones[i].transform.GetComponentInChildren<MeshRenderer>().material; // Set objects' default material to it's current material.
				surveillanceDrones[i].transform.GetComponentInChildren<MeshRenderer>().material = highlightMat; // Set objects' material to "highlightMat".
			}
		}

        for(int i = 0; i < motionDetectors.Count; i++)
        {
            if (Vector3.Distance(transform.position, motionDetectors[i].transform.position)  <= distanceFromObject[1] && seenObj[1] == false)
            {
                seenObj[1] = true; 

                // Highlight that particular object
                defaultMat= motionDetectors[i].GetComponent<MeshRenderer>().material; // Set objects' default material to it's current material.
                motionDetectors[i].GetComponent<MeshRenderer>().material = highlightMat; // Set objects' material to "highlightMat".
            }
        }

        for(int i = 0; i < switches.Count; i++)
        {
            if (Vector3.Distance(transform.position, switches[i].transform.position)  <= distanceFromObject[2] && seenObj[2] == false)
            {
                seenObj[2] = true;

                List<MeshRenderer> childRenderer = new List<MeshRenderer>();

                switches[i].GetComponentsInChildren<MeshRenderer>(childRenderer);

                for(int j = 0; j < childRenderer.Count; j++)
                {
                    childRenderer[j].material = highlightMat;
                }
            }
        }

        for(int i = 0; i < electricFences.Count; i++)
        {
            if (Vector3.Distance(transform.position, electricFences[i].transform.position)  <= distanceFromObject[3] && seenObj[3] == false)
            {
                seenObj[3] = true;

                // Highlight that particular object
                defaultMat= electricFences[i].GetComponent<MeshRenderer>().material; // Set objects' default material to it's current material.
                electricFences[i].GetComponent<MeshRenderer>().material = highlightMat; // Set objects' material to "highlightMat".
            }
        }

        for(int i = 0; i < doors.Count; i++)
        {
            if (Vector3.Distance(transform.position, doors[i].transform.position)  <= distanceFromObject[4] && seenObj[4] == false)
            {
                seenObj[4] = true;

                // Highlight that particular object
                defaultMat= doors[i].GetComponent<MeshRenderer>().material; // Set objects' default material to it's current material.
                doors[i].GetComponent<MeshRenderer>().material = highlightMat; // Set objects' material to "highlightMat".
            }
        }
    }    
}
