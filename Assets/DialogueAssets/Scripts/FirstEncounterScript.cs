using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEncounterScript : MonoBehaviour 
{
    public List<GameObject> objects;//Apply GameObjects in the order of SurveillenceDrone(1) -> MotionDetector(2) -> Switch(3) -> ElectricFence(4) -> Door(5)

    public float distanceFromObject = 10f;

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
        if (Vector3.Distance(transform.position, objects[0].transform.position)  <= distanceFromObject && seenObj[0] == false)
        {
            seenObj[0] = true;
        }

        if (Vector3.Distance(transform.position, objects[1].transform.position) <= distanceFromObject && seenObj[1] == false)
        {
            seenObj[1] = true;
        }

        if (Vector3.Distance(transform.position, objects[2].transform.position) <= distanceFromObject && seenObj[2] == false)
        {
            seenObj[2] = true;
        }

        if (Vector3.Distance(transform.position, objects[3].transform.position) <= distanceFromObject && seenObj[3] == false)
        {
            seenObj[3] = true;
        }

        if (Vector3.Distance(transform.position, objects[4].transform.position) <= distanceFromObject && seenObj[4] == false)
        {
            seenObj[4] = true;
        }
    }    
}
