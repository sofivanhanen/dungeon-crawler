using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	const float speedOfMovement = 5.0f;
	const float speedOfTurn = 0.15f;

	public GameObject sword;

	public int maxHealth;
	public int health;

	public bool dead;

	void Start ()
	{
		maxHealth = 100;
		health = maxHealth;
		dead = false;
	}
	
	void Update ()
	{
		if (dead) return;
		
		// Moving
		var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
		if (!(x==0 && z==0)) {
		Vector3 movement = new Vector3(x, 0, z);
		// TODO: Why does rotation work wrong? Player always faces opposite direction. '*-1* fixes it.
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement*-1), speedOfTurn);
        transform.Translate(movement * Time.deltaTime * speedOfMovement, Space.World);
		}
		
		// Shooting
		if (Input.GetMouseButtonDown(0))
		{
			SwordController swordController = sword.GetComponent<SwordController>();
			swordController.Attack();
		}
	}

	public void GetHit(int damage)
	{
		if (dead) return;
		// TODO Show a cue that we're hurting
		health -= damage;
		if (health <= 0) dead = true;
	}
}
