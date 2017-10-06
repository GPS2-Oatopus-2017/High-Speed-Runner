using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour {

	private static SpawnManagerScript mInstance;
	public static SpawnManagerScript Instance
	{
		get { return mInstance; }
	}

	public List<GameObject> objectsToPool;
	public List<int> numberOfObjectsToPool;
	Dictionary<string,Stack<GameObject>> pool;

	public float spawnDistance = 4.0f;
	public int reputation;
	public float countDownTimer;
	public float spawnTime;
	public int sdCount;
	public int hdCount;

	void Awake()
	{
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) Destroy(this.gameObject);
	}

	public Transform playerLastLocation;
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
			if(reputation == 1)
			{
				sdCount+=1;
				hdCount+=1;
				Spawn("Surveillance drone",playerLastLocation.position,Quaternion.identity);
				Spawn("Hunting drone",playerLastLocation.position,Quaternion.identity);
				Debug.Log("1SD,1HD");
			}
			else if(reputation == 2)
			{
				hdCount+=2;
				for(int i=0; i<2; i++)
				{
					Spawn("Hunting drone",playerLastLocation.position,Quaternion.identity);
				}
				Debug.Log("2HD");
			}
			else if(reputation == 3)
			{
				sdCount+=1;
				hdCount+=2;
				Spawn("Surveillance drone",playerLastLocation.position,Quaternion.identity);
				for(int i=0; i<2; i++)
				{
					Spawn("Hunting drone",playerLastLocation.position,Quaternion.identity);
				}
				Debug.Log("1SD,2HD");
			}
			else if(reputation == 4)
			{
				sdCount+=1;
				hdCount+=3;
				Spawn("Surveillance drone",playerLastLocation.position,Quaternion.identity);
				for(int i=0; i<3; i++)
				{
					Spawn("Hunting drone",playerLastLocation.position,Quaternion.identity);
				}
				Debug.Log("1SD,3HD");
			}
			else if(reputation == 5)
			{
				sdCount+=3;
				hdCount+=3;
				for(int i=0; i<3; i++)
				{
					Spawn("Surveillance drone",playerLastLocation.position,Quaternion.identity);
				}
				for(int i=0; i<3; i++)
				{
					Spawn("Hunting drone",playerLastLocation.position,Quaternion.identity);
				}
				Debug.Log("3SD,3HD");
			}
			countDownTimer = 0;
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

	public void SpawnMuliple(string objectName,Vector3 newPosition, Quaternion newRotation)
	{
		
	}

	//Despawn
	public void Despawn(GameObject objectToDespawn){
		objectToDespawn.SetActive(false);
		pool[objectToDespawn.name].Push( objectToDespawn );
	}
}
