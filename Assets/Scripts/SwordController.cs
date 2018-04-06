using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
	}

	public void Attack() {
		animator.SetTrigger("Sword_attack");
	}
	
}
