using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaypointNodeData
{
	public bool isJunction;

	public WaypointNodeScript node1;
	public WaypointNodeScript node2;

	public List<WaypointNodeScript> backNodes;

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

	public WaypointNodeData()
	{
		backNodes = new List<WaypointNodeScript>();
	}
}

[System.Serializable]
[ExecuteInEditMode]
public class WaypointNodeScript : MonoBehaviour
{
	public WaypointNodeData data;
	public WaypointNodeData prevData;

	void OnValidate()
	{
		UpdateJunctionMode();
		UpdateBackNodes();
		BackupPreviousData();
	}

	void OnDestroy()
	{
		ResetNodes();
	}

	[ContextMenu("Reset Nodes")]
	void ResetNodes()
	{
		if(data.node1)
		{
			if(data.node1.data.backNodes.Contains(this))
				data.node1.data.backNodes.Remove(this);

			data.node1 = null;
		}
		if(data.node2)
		{
			if(data.node2.data.backNodes.Contains(this))
				data.node2.data.backNodes.Remove(this);

			data.node2 = null;
		}
		BackupPreviousData();
	}

	void UpdateJunctionMode()
	{
		if(data.isJunction != prevData.isJunction)
		{
			if(data.node1)
			{
				if(data.node1.data.backNodes.Contains(this))
					data.node1.data.backNodes.Remove(this);
				
				data.node1 = null;
			}
			if(data.node2)
			{
				if(data.node2.data.backNodes.Contains(this))
					data.node2.data.backNodes.Remove(this);
				
				data.node2 = null;
			}

			prevData.isJunction = data.isJunction;
		}
	}

	void UpdateBackNodes()
	{
		if(!data.isJunction)
		{
			if(data.forwardNode)
			{
				if(!data.forwardNode.data.backNodes.Contains(this))
					data.forwardNode.data.backNodes.Add(this);
			}
			else if(prevData.forwardNode)
			{
				if(prevData.forwardNode.data.backNodes.Contains(this))
					prevData.forwardNode.data.backNodes.Remove(this);
			}
		}
		else
		{
			if(data.leftNode)
			{
				if(!data.leftNode.data.backNodes.Contains(this))
					data.leftNode.data.backNodes.Add(this);
			}
			else if(prevData.leftNode)
			{
				if(prevData.leftNode.data.backNodes.Contains(this))
					prevData.leftNode.data.backNodes.Remove(this);
			}

			if(data.rightNode)
			{
				if(!data.rightNode.data.backNodes.Contains(this))
					data.rightNode.data.backNodes.Add(this);
			}
			else if(prevData.rightNode)
			{
				if(prevData.rightNode.data.backNodes.Contains(this))
					prevData.rightNode.data.backNodes.Remove(this);
			}
		}
	}

	void BackupPreviousData()
	{
		prevData.forwardNode = data.forwardNode;
		prevData.leftNode = data.leftNode;
		prevData.rightNode = data.rightNode;
	}

	// Only In Play Mode
	void OnTriggerEnter(Collider other)
	{
		if(Application.isPlaying)
		{
			if(other.GetComponent<PlayerCoreController>())
			{
				Debug.Log("enter");
				WayPointManagerScript.Instance.RegisterNode(this);;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(Application.isPlaying)
		{
			if(other.GetComponent<PlayerCoreController>())
			{
				Debug.Log("exit");
				WayPointManagerScript.Instance.UnregisterNode(this);
			}
		}
	}
}
