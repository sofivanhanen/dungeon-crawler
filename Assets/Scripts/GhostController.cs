using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

	public GameObject player;
	
	private const int maxRange = 5;
	
	private const float speedOfMovement = 1.0f;
	private const float speedOfTurn = 0.15f;

	private const float minTimeBetweenAttacks = 1f;
	private float attackTimer;

	private const int damage = 30;

	void Start ()
	{
		player = GameObject.FindWithTag("Player");
		attackTimer = minTimeBetweenAttacks;
	}
	
	void Update ()
	{
		attackTimer += Time.deltaTime;
		var heading = player.transform.position - this.transform.position;
		// Using sqrMagnitude to save processing power
		if (heading.sqrMagnitude < maxRange * maxRange) {
			// Player is within range! Let's get 'em!!
			var distance = heading.magnitude;
			var movement = heading / distance;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement*-1), speedOfTurn);
			transform.Translate(movement * Time.deltaTime * speedOfMovement, Space.World);
		}
	}

	void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.name == "Player" && attackTimer >= minTimeBetweenAttacks)
		{
			player.GetComponent<PlayerController>().GetHit(damage);
			attackTimer = 0;
		}
	}
}
