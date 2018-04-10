using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

	private Animator animator;

	private const int damage = 20;

	void Start () {
		animator = GetComponent<Animator>();
	}

	public void Attack() {
		animator.SetTrigger("Sword_attack");
	}

	void OnTriggerEnter(Collider collider)
	{
		// Possible because of multiple contact points, OnTriggerEnter gets called repeatedly. Handle this in GetHit.
		if (collider.gameObject.tag == "Enemy" && animator.GetCurrentAnimatorStateInfo(0).IsName("Sword_swing"))
		{
			// TODO What if it's another enemy, not a ghost?
			collider.gameObject.GetComponent<GhostController>().GetHit(damage);
		}
	}
	
}
