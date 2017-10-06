using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReputationManagerScript : MonoBehaviour {

	private static ReputationManagerScript mInstance;
	public static ReputationManagerScript Instance
	{
		get { return mInstance; }
	}

	void Awake()
	{
		if(mInstance == null) mInstance = this;
		else if(mInstance != this) Destroy(this.gameObject);
	}

	public int maxRep;
	public int currentRep;
	public int lastRep;
	public int value;
	public Sprite lightUp;
	public Sprite lightDown;
	public List<Image> starList = new List<Image>();

	public Text enemyAmountText;
	public Text playerStatus;
	string displayECount;
	string status;
	public string[] statusList;

	public float resetCounter;
	public float resetTime;
	// Use this for initialization
	void Start () {
		UpdateCount();
		maxRep = starList.Count;
		//currentRep = maxRep;
	}
	
	// Update is called once per frame
	void Update () {
		if(resetCounter >= resetTime)
		{
			DecreaseReputation();
			resetCounter = 0;
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			DecreaseReputation();
		}
		UpdateBar();
		UpdateCount();
		UpdateStatus();
		if(currentRep == lastRep && currentRep != 0)
		{
			resetCounter += Time.deltaTime;
		}
	}
		

	public void IncreaseReputation()
	{
		currentRep += value;
		resetCounter = 0;
		if(currentRep > maxRep)
		{
			currentRep = maxRep;
		}
	}

	void DecreaseReputation()
	{
		currentRep -= value;
		if(currentRep <= 0)
		{
			currentRep = 0;
		}
	}

	void UpdateBar()
	{
		for(int i=0; i<starList.Count; i++)
		{
			if(i < currentRep)
			{
				starList[i].sprite = lightUp;
			}
			else
			{
				starList[i].sprite = lightDown;
			}
		}
	}

	void UpdateCount() //need to change to sd and hd
	{
		displayECount = "SD Count: " + SpawnManagerScript.Instance.sdCount + "\nHD Count: "+ SpawnManagerScript.Instance.hdCount;
		enemyAmountText.text = displayECount;
	}

	void UpdateStatus()
	{
		for(int i=0; i<=maxRep; i++)
		{
			if(currentRep == i)
			{
				status = "Status : " + statusList[i];
			}
		}
		playerStatus.text = status;
	}

	void LateUpdate()
	{
		lastRep = currentRep;
	}
}
