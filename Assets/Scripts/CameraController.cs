using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;
	const float interpolation = 5.0f;

	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		// Follow player smoothly
		Vector3 position = this.transform.position;
        position.z = Mathf.Lerp(this.transform.position.z, player.transform.position.z + offset.z, interpolation * Time.deltaTime);
        position.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x + offset.x, interpolation * Time.deltaTime);
        this.transform.position = position;
	}
}
