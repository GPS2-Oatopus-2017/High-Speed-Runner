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
	public Vector3 pointingPos;

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
		pointingPos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if(!hasConfirmedEvent)
//		{
//			//Check for events
//			if(isInProximity && touchedNodes[0].data.isJunction)
//			{
//				if (SwipeScript.Instance.GetSwipe () == SwipeDirection.Left || Input.GetKeyDown (KeyCode.A)) 
//				{
//					curEvent = EventType.SwipeLeft;
//					hasConfirmedEvent = true;
//				} 
//				else if (SwipeScript.Instance.GetSwipe () == SwipeDirection.Right || Input.GetKeyDown (KeyCode.D)) 
//				{
//					curEvent = EventType.SwipeRight;
//					hasConfirmedEvent = true;
//				}
//				else
//				{
//					curEvent = EventType.None;
//					hasConfirmedEvent = false;
//				}
//			}
//			else
//			{
//				curEvent = EventType.MoveForward;
//			}
//	
//			//Notify the player to turn.
//			if(isInProximity)
//			{
//				switch(curEvent)
//				{
//					case EventType.MoveForward:
//						pointingPos = touchedNodes[0].data.forwardNode.transform.position;
//						break;
//					case EventType.SwipeLeft:
//						pointingPos = touchedNodes[0].data.leftNode.transform.position;
//					Debug.Log("Left");
//						break;
//					case EventType.SwipeRight:
//						pointingPos = touchedNodes[0].data.rightNode.transform.position;
//					Debug.Log("Right");
//						break;
//				}
//			}
//		}
//
//		if(curEvent != EventType.None)
//			player.RotateTowards(pointingPos);
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
