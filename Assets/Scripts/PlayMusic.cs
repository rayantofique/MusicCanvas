using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

	AudioSource aud;
	ParticleSystem particleSys;
	ParticleSystem.MinMaxGradient startColor;

	public Gradient grad;
	Vector3 startScale;


	private Dictionary<Vector3, string> noteReferences;


	// Use this for initialization
	void Start () {

		particleSys = gameObject.GetComponent<ParticleSystem> ();
		aud = gameObject.GetComponent<AudioSource> ();
		print (aud.clip);
		startColor = particleSys.main.startColor;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		print ("yes its colliding without tag");

		if (col.tag == "Cursor") 
		{
			print ("yes its colliding");
			aud.Play ();
			var em = particleSys.emission;
			em.rateOverTime = 30;
			em = particleSys.emission;

			var shape = particleSys.shape;
			startScale = shape.scale;
			shape.scale = new Vector3 (startScale.x + 0.5f, startScale.y + 0.5f, startScale.z + 0.5f);

			ParticleSystem.MainModule main = particleSys.main;
			main.startColor = grad;
			main.startSize = 0.7f;
			main.startSpeed = 0.2f;

		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Cursor") {

			var em = particleSys.emission;
			em.rateOverTime =9;
			em = particleSys.emission;
			ParticleSystem.MainModule main = particleSys.main;
			main.startColor = startColor;
			main.startSize = 0.5f;
			main.startSpeed = 0;

			var shape = particleSys.shape;
			shape.scale = startScale;
		}
	}

	public void SaveNoteReferences(Dictionary<Vector3, string> refs)
	{
		noteReferences = refs;
	}
}
