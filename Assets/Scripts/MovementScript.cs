﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float speed;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {

		speed = SettingsManager.instance.cursorSpeed;
		startPos = SettingsManager.instance.CursorStartPos;

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			transform.position = startPos;


		}
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
		
	}
}
