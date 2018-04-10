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
	
	private Color normalColor = new Color(1f, 1f, 1f);
	private Color hurtingColor = new Color(1f, 0.7f, 0.7f);

	private const float maxTimeShowingHurtingColor = 1f;
	private float timeSinceHit;

	void Start ()
	{
		maxHealth = 100;
		health = maxHealth;
		dead = false;
		timeSinceHit = 0f;
	}
	
	void Update ()
	{
		if (Input.GetKey("escape")) 
		{
			// TODO: Make a 'game master' object to control things like this
			Application.Quit();
		}

		if (dead) return;

		if (timeSinceHit < maxTimeShowingHurtingColor)
		{
			timeSinceHit += Time.deltaTime;
			if (timeSinceHit >= maxTimeShowingHurtingColor) this.gameObject.GetComponent<Renderer>().material.color = normalColor;
		}
		
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
		health -= damage;
		if (health <= 0) dead = true;
		this.gameObject.GetComponent<Renderer>().material.color = hurtingColor;
		timeSinceHit = 0f;
	}
}
