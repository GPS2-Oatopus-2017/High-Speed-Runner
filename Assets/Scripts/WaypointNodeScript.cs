using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North = 0,
    East,
    South,
    West,

    Total
}

[System.Serializable]
public class WaypointNodeData
{
    public WaypointNodeScript[] directionalNodes;

	public WaypointNodeScript rightNode(Direction fromDir)
	{
		return rightNode((int) fromDir);
	}

	public WaypointNodeScript rightNode(int fromDir)
	{
		int right = (fromDir + 1) % (int)Direction.Total;
//		Debug.Log((Direction)right);
		return directionalNodes[right];
	}

	public WaypointNodeScript forwardNode(Direction fromDir)
	{
		return forwardNode((int) fromDir);
	}

	public WaypointNodeScript forwardNode(int fromDir)
	{
		int forward = (fromDir + 2) % (int)Direction.Total;
//		Debug.Log((Direction)right);
		return directionalNodes[forward];
	}

	public WaypointNodeScript leftNode(Direction fromDir)
	{
		return leftNode((int) fromDir);
	}

	public WaypointNodeScript leftNode(int fromDir)
	{
		int left = (fromDir + 3) % (int)Direction.Total;
//		Debug.Log((Direction)right);
		return directionalNodes[left];
	}

	public WaypointNodeData()
	{
        directionalNodes = new WaypointNodeScript[(int)(Direction.Total)];
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
		UpdateNodes();
	}

	void Awake()
	{
		ResetNodes(true);
	}

	void OnDestroy()
	{
		ResetNodes();
	}

	[ContextMenu("Reset Nodes")]
	void ResetNodes()
	{
		ResetNodes(false);
	}

	void ResetNodes(bool softReset)
	{
		for(int i = 0; i < (int)Direction.Total; i++)
		{
			if(!softReset)
			{
				if(data.directionalNodes[i])
				{
					data.directionalNodes[i].data.directionalNodes[GetOppositeDir(i)] = null;
					data.directionalNodes[i].BackupPreviousData();
				}
			}

			data.directionalNodes[i] = null;
		}
		BackupPreviousData();
	}

	void UpdateNodes()
	{
		for(int i = 0; i < (int)Direction.Total; i++)
		{
			if(data.directionalNodes[i] != prevData.directionalNodes[i])
			{
				if(data.directionalNodes[i])
				{
					// Deny node assignment when assigning this node to itself
					if(data.directionalNodes[i] == this)
					{
						data.directionalNodes[i] = prevData.directionalNodes[i];
					}
					else
					{
						bool isDuplicated = false;
						for(int j = 0; j < (int)Direction.Total; j++)
						{
							// Deny node assignment when it is used in other directions of this node
							if(i != j && data.directionalNodes[i] == data.directionalNodes[j])
							{
								data.directionalNodes[i] = prevData.directionalNodes[i];
								isDuplicated = true;
								break;
							}
						}

						if(!isDuplicated)
						{
							// Deny node assignment when the slot is occupied
							if(data.directionalNodes[i].data.directionalNodes[GetOppositeDir(i)])
							{
								data.directionalNodes[i] = prevData.directionalNodes[i];
							}
							else
							{
								data.directionalNodes[i].data.directionalNodes[GetOppositeDir(i)] = this;
								data.directionalNodes[i].BackupPreviousData();

								if(prevData.directionalNodes[i])
								{
									prevData.directionalNodes[i].data.directionalNodes[GetOppositeDir(i)] = null;
									prevData.directionalNodes[i].BackupPreviousData();
								}
							}
						}
					}
				}
			}
		}
		BackupPreviousData();
	}

	int GetOppositeDir(int i)
	{
		return (i + 2) % (int)Direction.Total;
	}

	public void BackupPreviousData()
	{
		for(int i = 0; i < (int)Direction.Total; i++)
		{
			prevData.directionalNodes[i] = data.directionalNodes[i];
		}
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
