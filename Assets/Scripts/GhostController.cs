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

	private const int maxHealth = 40;
	private int health;

	private const float minTimeBetweenGettingHit = 0.3f;
	private float hitTimer;

	void Start ()
	{
		player = GameObject.FindWithTag("Player");
		attackTimer = minTimeBetweenAttacks;
		health = maxHealth;
		hitTimer = minTimeBetweenAttacks;
	}
	
	void Update ()
	{
		attackTimer += Time.deltaTime;
		hitTimer += Time.deltaTime;
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
		if(collision.gameObject.tag == "Player" && attackTimer >= minTimeBetweenAttacks)
		{
			player.GetComponent<PlayerController>().GetHit(damage);
			attackTimer = 0;
		}
	}

	public void GetHit(int damage)
	{
		if (hitTimer < minTimeBetweenGettingHit) return;
		health -= damage;
		if (health <= 0) Destroy(this.gameObject);
		hitTimer = 0f;
	}
}
