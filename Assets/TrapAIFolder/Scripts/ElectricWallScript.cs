using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWallScript : MonoBehaviour 
{
    [Header("Electric Wall Settings")]
    public float slowDuration;
    public float slowTimer;
    public float speedReducedValue;
    public GameObject player;
    public bool playerIsSlowed;

	void Start() 
	{
        slowTimer = slowDuration; // Set Countdown timer to the duration player is slowed
        playerIsSlowed = false; // Boolean indicating if player is slowed
		this.gameObject.SetActive(false); 
	}

	void Update() 
	{
        if(playerIsSlowed) // If player is slowed, do the following
        {
            slowTimer -= Time.deltaTime; // Countdown timer
            player.GetComponent<PlayerCoreController>().ToggleRunning(false); //Player is slowed

            if(slowTimer <= 0) // After timer reaches 0, slow debuff will be lifted
            {
                slowTimer = slowDuration;
                playerIsSlowed = false;
                player.GetComponent<PlayerCoreController>().ToggleRunning(true);
                Debug.Log("Speed slow debuff is lifted!");
            }
        }
	}

    void OnTriggerEnter(Collider other) //Apply knockback and speed reduction upon collision
    {
        if(other.gameObject.tag == "Player")
        {
            playerIsSlowed = true;

            Debug.Log("Player speed is reduced by " + speedReducedValue);
        }
    }
}
