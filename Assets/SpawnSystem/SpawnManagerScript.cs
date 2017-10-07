using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManagerScript : MonoBehaviour {

	private static SpawnManagerScript mInstance;
	public static SpawnManagerScript Instance
	{
		get { return mInstance; }
	}

	public List<GameObject> objectsToPool;
	public List<int> numberOfObjectsToPool;
	Dictionary<string,Stack<GameObject>> pool;

	public Transform player;
	public float spawnDistance = 4.0f;
	public int reputation;
	public float countDownTimer;
	public float spawnTime;
	public int sdCount;
	public int hdCount;

	Vector3 spawnPoint;

	void Awake()
	{
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) Destroy(this.gameObject);
	}

	public Transform playerLastLocation;//testing

	// Use this for initialization
	void Start () {
		InitializePoolManager();
	}
	
	// Update is called once per frame
	void Update () {
		reputation = ReputationManagerScript.Instance.lastRep;
		if(reputation >= 1 && reputation == ReputationManagerScript.Instance.currentRep)
		{
			countDownTimer += Time.deltaTime;
		}
		else
		{
			countDownTimer = 0;
		}

		if(countDownTimer >= spawnTime)
		{
			countDownTimer = 0;
			CalculateSpawnPoint();
			if(reputation == 1)
			{
				sdCount+=1;
				hdCount+=1;
				Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				Spawn("Hunting drone",spawnPoint,Quaternion.identity);
			}
			else if(reputation == 2)
			{
				hdCount+=2;
				for(int i=0; i<2; i++)
				{
					Spawn("Hunting drone",spawnPoint,Quaternion.identity);
				}
			}
			else if(reputation == 3)
			{
				sdCount+=1;
				hdCount+=2;
				Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				for(int i=0; i<2; i++)
				{
					Spawn("Hunting drone",spawnPoint,Quaternion.identity);
				}
			}
			else if(reputation == 4)
			{
				sdCount+=1;
				hdCount+=3;
				Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				for(int i=0; i<3; i++)
				{
					Spawn("Hunting drone",spawnPoint,Quaternion.identity);
				}
			}
			else if(reputation == 5)
			{
				sdCount+=3;
				hdCount+=3;
				for(int i=0; i<3; i++)
				{
					Spawn("Surveillance_Drone",spawnPoint,Quaternion.identity);
				}
				for(int i=0; i<3; i++)
				{
					Spawn("Hunting drone",spawnPoint,Quaternion.identity);
				}
			}
		}
	}

	void InitializePoolManager(){
		pool = new Dictionary<string,Stack<GameObject>>();

		for(int i = 0; i < objectsToPool.Count; i++){
			pool.Add( objectsToPool[i].name, new Stack<GameObject>() );
			for(int f = 0; f < numberOfObjectsToPool.Count; f++){
				GameObject go = Instantiate(objectsToPool[i]);
				go.transform.SetParent(this.transform);
				go.gameObject.SetActive(false);
				go.name = objectsToPool[i].name;

				pool[objectsToPool[i].name].Push( go );
			}
		}
	}

	void CalculateSpawnPoint()
	{
		WaypointNodeScript lastNode = WayPointManagerScript.Instance.tracePlayerNodes.Last();
		float distance = Vector3.Distance(player.transform.position,lastNode.transform.position);
		Debug.Log(distance);
		if(distance >= spawnDistance)
		{
			//spawnPoint = Vector3.Lerp(player.transform.position,lastNode.transform.position, (distance - spawnDistance)/1.0f);
			spawnPoint = Vector3.Lerp(player.transform.position,lastNode.transform.position, ((distance-spawnDistance)/distance));
			Debug.Log(">=4");
			Debug.Log((distance-spawnDistance)/distance);
			Debug.Log(spawnPoint);
		}
		else if(distance < spawnDistance)
		{
			Debug.Log("<=4");
			for(int i=WayPointManagerScript.Instance.tracePlayerNodes.Count-2; i<0 ; i--)
			{
				distance += Vector3.Distance(WayPointManagerScript.Instance.tracePlayerNodes[i].transform.position,WayPointManagerScript.Instance.tracePlayerNodes[i-1].transform.position);
				if(distance >= spawnDistance)
				{
					spawnPoint = Vector3.Lerp(WayPointManagerScript.Instance.tracePlayerNodes[i].transform.position,WayPointManagerScript.Instance.tracePlayerNodes[i-1].transform.position, (distance - spawnDistance)/1.0f);
				}
			}
		}
	}

	//Spawn
	public void Spawn(string objectName,Vector3 newPosition, Quaternion newRotation){
		if(pool[objectName].Count > 0){
			GameObject go = pool[objectName].Pop();
			go.transform.position = newPosition;
			go.transform.rotation = newRotation;
			go.SetActive(true);
		}
	}

	public void SpawnMuliple(string objectName,Vector3 newPosition, Quaternion newRotation,int amount)
	{
		for(int i=0; i<=amount ; i++)
		{
			if(pool[objectName].Count > 0){
				GameObject go = pool[objectName].Pop();
				go.transform.position = newPosition;
				go.transform.rotation = newRotation;
				go.SetActive(true);
			}
		}
	}

	//Despawn
	public void Despawn(GameObject objectToDespawn){
		objectToDespawn.SetActive(false);
		pool[objectToDespawn.name].Push( objectToDespawn );
	}
}
