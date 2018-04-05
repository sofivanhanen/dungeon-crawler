using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	const float speedOfMovement = 5.0f;
	const float speedOfTurn = 0.15f;

	void Start () {
		
	}
	
	void Update () {
		var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
		if (!(x==0 && z==0)) {
		Vector3 movement = new Vector3(x, 0, z);
		// TODO: Why does rotation work wrong? Player always faces opposite direction. '*-1* fixes it.
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement*-1), speedOfTurn);
        transform.Translate(movement * Time.deltaTime * speedOfMovement, Space.World);
		}
	}
}
