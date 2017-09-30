using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeData
{
	public bool isJunction;

	public WaypointNodeScript node1;
	public WaypointNodeScript node2;

	public WaypointNodeScript forwardNode
	{
		get
		{
			if(!isJunction)
				return node1;
			else
				return null;
		}
		set
		{
			if(!isJunction)
				node1 = value;
		}
	}

	public WaypointNodeScript leftNode
	{
		get
		{
			if(isJunction)
				return node1;
			else
				return null;
		}
		set
		{
			if(isJunction)
				node1 = value;
		}
	}

	public WaypointNodeScript rightNode
	{
		get
		{
			if(isJunction)
				return node2;
			else
				return null;
		}
		set
		{
			if(isJunction)
				node2 = value;
		}
	}
}

[System.Serializable]
public class WaypointNodeScript : MonoBehaviour
{
	public NodeData data;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<PlayerCoreController>())
		{
			Debug.Log("enter");
			WayPointManagerScript.Instance.RegisterNode(this);;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<PlayerCoreController>())
		{
			Debug.Log("exit");
			WayPointManagerScript.Instance.UnregisterNode(this);
		}
	}
}
