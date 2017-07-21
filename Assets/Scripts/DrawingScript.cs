using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingScript : MonoBehaviour {

	public AudioClip[] notes;
	private AudioSource aud;

	public GameObject particleSystemNote;
	public GameObject LineRendererSystemNote;
	public TrailRenderer trail;

	private Vector3 startPositionNote;
	private Vector3 endPositionNote;

	private bool flag = false;
	private GameObject particleNote;

	private Note note;

	//public List<Vector3> nodeArray;

	private Vector3 lastActivePoint;

	private Dictionary<Vector3, string> notesInStroke;

	Vector3 noteStartPos;
	Vector3 noteEndPos;





	// Use this for initialization
	void Start () {

		notesInStroke = new Dictionary<Vector3, string> ();
		aud = GetComponent<AudioSource> ();
		//nodeArray = new List<Vector3> ();
		lastActivePoint = Vector3.zero;
		trail = this.GetComponent<TrailRenderer> ();	
		trail.time = 1f;
	}
	
	// Update is called once per frame
	void Update () {


		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 10;
		//print (flag);
		if (Input.GetMouseButtonDown (0)) 
		{
			this.transform.position = mousePos;

			//mousePos.z = 5;
			if (note == null) {

				note = new Note ();
				note.AddPoint (mousePos);
				noteStartPos = mousePos; 
				trail.time = 1;

				//nodeArray.Add (mousePos);
				lastActivePoint = mousePos;
			}
			//aud.clip = AssignNote (0);
			//particleNote.GetComponent<AudioSource> ().clip = AssignNote (0);
			//aud.Play ();
			flag = true;

			//particleNote = GameObject.Instantiate (particleSystemNote, mousePos, Quaternion.identity);
			//particleNote.GetComponent<FollowNotePath> ().AddPathNode (note.LastNode());
			aud.clip = notes [Mathf.FloorToInt (mousePos.y)];
			aud.Play ();
			//print ("workign falsE");


			//instantiate node here
		}
		else if (Input.GetMouseButtonUp (0)) {


			flag = false;
			//print ("tworkingrue");
			//mousePos.z = 5;
			if (note.LastNode () != mousePos) {
				
				note.AddPoint (mousePos);
				//nodeArray.Add (mousePos);
				trail.time = 0;
			} else {

				note.AddPoint(new Vector3(mousePos.x + 0.2f, mousePos.y, mousePos.z));
			}


			//to pass note if pitch and y val does not change
			if ((int)noteStartPos.y == (int)mousePos.y) {

				string singlePitchNoteString = note.GetNoteString(noteStartPos, mousePos);
				notesInStroke.Add (noteStartPos, singlePitchNoteString);
			}


			//particleNote.GetComponent<FollowNotePath> ().AddPathNode (note.LastNode());
			List<Vector3> nodeList = note.getNodes();
			particleNote = GameObject.Instantiate (particleSystemNote, nodeList[0], Quaternion.identity);
			particleNote.GetComponent<FollowNotePath> ().SetNodes (nodeList);


			//*************  add note here to assign notes to instantiated note
			particleNote.GetComponent<PlayMusic>().SaveNoteReferences(notesInStroke);

			notesInStroke.Clear();

			//GameObject lineRenderer = GameObject.Instantiate(LineRendererSystemNote, nodeList[0], Quaternion.identity);
			//lineRenderer.GetComponent<LineRenderer> ().positionCount = nodeList.Count;
			//lineRenderer.GetComponent<LineRenderer> ().SetPositions (nodeList.ToArray ());
			note = null;

		}

		if (flag) 
		{

			this.transform.position = mousePos;

			//mousePos.z = 5;
			if (note == null) {

				//print ("null");
			}
			Vector3 lastNodePos = note.LastNode ();
			Vector3 oldDirection = lastActivePoint - lastNodePos;
			Vector3 newDirection = mousePos - lastActivePoint;
		
			if (newDirection == Vector3.zero) {

				return;
			}

			if ((int)lastNodePos.y != (int)mousePos.y) {

				noteEndPos = mousePos;
				//get string val and pass startPosition + string 
				string noteString = note.GetNoteString(noteStartPos, noteEndPos);
				notesInStroke.Add (noteStartPos, noteString);
				noteStartPos = mousePos;
				noteEndPos = Vector3.zero;

			}

			if (!newDirection.normalized.Equals(oldDirection.normalized)) 
			{
				if (Mathf.Abs(Vector3.Angle (oldDirection, newDirection)) > 5f) {

					note.AddPoint (lastActivePoint);
					trail.time++;

					//nodeArray.Add (lastActivePoint);
					//print ("last node pos" + lastNodePos);
					//print ("new dir" + newDirection);
					//print(Vector3.Angle(lastNodePos, newDirection));
					//particleNote.GetComponent<FollowNotePath> ().AddPathNode (note.LastNode());

				}
			}


		}

		lastActivePoint = mousePos;




		/*if (Input.GetMouseButtonDown (1)) {

			aud.clip = AssignNote (1);
			particleNote.GetComponent<AudioSource> ().clip = AssignNote (1);
			aud.Play ();
		}*/
		/*if (flag) 
		{
			if (particleNote) 
			{
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 5;
				var shape = particleNote.GetComponent<ParticleSystem> ().shape;
				Vector3 length = mousePos - startPositionNote;
				shape.scale = new Vector3(length.x, 1, 1);
				float xPosShift = (endPositionNote.x - startPositionNote.x) / 2;
				Vector3 newPos = new Vector3(startPositionNote.x + xPosShift, mousePos.y, mousePos.z);
				particleNote.transform.position = newPos;
					
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			flag = false;
		}*/
			
	}


	public AudioClip AssignNote(int button)
	{
		
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.z = 5;
		startPositionNote = mousePos;
		particleNote = GameObject.Instantiate (particleSystemNote, mousePos, Quaternion.identity);
		if (button == 0) {
			return notes [Mathf.FloorToInt (mousePos.y)];

		} else {

			return notes [Mathf.FloorToInt (mousePos.y) + 12];
		}
			
	}


	
			


}
