using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstEncounterScript : MonoBehaviour 
{
    public List<GameObject> objects;// Apply GameObjects in the order of SurveillenceDrone(1) -> MotionDetector(2) -> Switch(3) -> ElectricFence(4) -> Door(5)

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
		for(int i = 0; i < objects.Count; i++)
		{
            // Calculates distance of player and obj and decides if it is in range. 
			if (Vector3.Distance(transform.position, objects[i].transform.position)  <= distanceFromObject && seenObj[i] == false)
			{
				seenObj[i] = true; // Player is currently withing range of an obj.
			}
		}
    }    
}
