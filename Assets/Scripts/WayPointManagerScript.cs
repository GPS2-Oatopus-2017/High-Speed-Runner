using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManagerScript : MonoBehaviour
{
	//Singleton Setup
	private static WaypointManagerScript mInstance;
	public static WaypointManagerScript Instance
	{
		get { return mInstance; }
	}

	public enum EventType
	{
		None = -1,
		MoveForward = 0,
		SwipeLeft,
		SwipeRight
	}
	public EventType curEvent;
	private bool hasConfirmedEvent = false;

	//The player
	public PlayerCoreController player;
	public WaypointNodeScript pointingNode;

	//Nodes that the player touches
	public List<WaypointNodeScript> tracePlayerNodes = new List<WaypointNodeScript>();
	public List<WaypointNodeScript> touchedNodes = new List<WaypointNodeScript>();
	public bool isInProximity
	{
		get
		{
			return touchedNodes.Count > 0;
		}
	}

	void Awake()
	{
		//Singleton Setup
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) Destroy(this.gameObject);
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isInProximity)
        {
            if (!hasConfirmedEvent)
            {  
                if (SwipeScript.Instance.GetSwipe() == SwipeDirection.Left || Input.GetKeyDown(KeyCode.A))
                {
                    curEvent = EventType.SwipeLeft;
                    hasConfirmedEvent = true;
                }
                else if (SwipeScript.Instance.GetSwipe() == SwipeDirection.Right || Input.GetKeyDown(KeyCode.D))
                {
                    curEvent = EventType.SwipeRight;
                    hasConfirmedEvent = true;
                }
                else
                {
                    curEvent = EventType.MoveForward;
                    hasConfirmedEvent = false;
                }
            }
        }
        else
        {
            hasConfirmedEvent = false;
            curEvent = EventType.None;
        }

        if(hasConfirmedEvent)
        {
            Direction facingDir = Direction.North;
            float thresholdAngle = 45.0f;
            float angle = Quaternion.FromToRotation(Vector3.forward, touchedNodes[0].transform.position - player.transform.position).eulerAngles.y;

            for (int i = 0; i < (int)Direction.Total; i++)
            {
                float minAngle = 90.0f * i - thresholdAngle;
                float maxAngle = 90.0f * i + thresholdAngle;

                if (minAngle < 0.0f) minAngle += 360.0f;
                if (maxAngle >= 360.0f) minAngle -= 360.0f;

                if (angle >= minAngle && angle <= maxAngle)
                {
                    facingDir = (Direction)i;
                    break;
                }
            }

            switch (curEvent)
            {
                case EventType.SwipeLeft:
                    pointingNode = touchedNodes[0].data.leftNode(facingDir);
                    curEvent = EventType.None;
                    break;
                case EventType.SwipeRight:
                    pointingNode = touchedNodes[0].data.rightNode(facingDir);
                    curEvent = EventType.None;
                    break;
                case EventType.MoveForward:
                    pointingNode = touchedNodes[0].data.forwardNode(facingDir);
                    curEvent = EventType.None;
                    break;
            }
        }

        if (curEvent == EventType.None)
            player.RotateTowards(pointingNode.transform.position);
    }

	public void RegisterNode(WaypointNodeScript node)
	{
		tracePlayerNodes.Add(node);
		touchedNodes.Add(node);
	}

	public void UnregisterNode(WaypointNodeScript node)
	{
		touchedNodes.Remove(node);
	}
}
