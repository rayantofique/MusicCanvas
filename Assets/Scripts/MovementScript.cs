using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public int speed;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {

		startPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			transform.position = startPos;
			speed = 3;
		}
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		
	}
}
