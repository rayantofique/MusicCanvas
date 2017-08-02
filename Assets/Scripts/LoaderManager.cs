using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderManager : MonoBehaviour {

	public GameObject UniversalManager;

	void Awake()
	{
		if (NoteReferenceDataBase.instance == null) {

			Instantiate (UniversalManager);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
