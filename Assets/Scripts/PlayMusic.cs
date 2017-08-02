using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour {

	AudioSource aud;
	ParticleSystem particleSys;
	ParticleSystem.MinMaxGradient startColor;

	public Gradient grad;
	Vector3 startScale;

	AudioClip[] clips;
	Vector3[] notePositions;

	int[] noteTransposes;
	int transpose = -4;

	private Dictionary<int, Eppy.Tuple<Vector3, string>> noteReferences;
	//private Dictionary<float, List<AudioClip>> clipsDict;
	//after init, 
	GameObject noteDatabase;

	private bool flag = false;

	private float timeCursor = 0;

	private bool timeToggle = false;
	private float timeRequired;

	private float timeNote;

	private float lerpPara = 0;

	private float timeLerp;

	private int clipIndex = 0;
	// Use this for initialization
	void Start () {

		//noteTransposes = new int[8]{0, 2, 5, 7, 9, 0, 2, 5};
		noteTransposes = new int[8]{0, 2, 4, 5, 7, 9, 11, 12};
		//0,2,5,7,9

		particleSys = gameObject.GetComponent<ParticleSystem> ();
		aud = gameObject.GetComponent<AudioSource> ();
		//aud.loop = true;
		startColor = particleSys.main.startColor;

	}
	
	// Update is called once per frame
	void Update () {

		if (flag) {

			if (clipIndex < notePositions.Length) {


				if (timeToggle) {
					print (clipIndex);
					float noteLength = Mathf.Abs (notePositions [clipIndex - 1].x - notePositions [clipIndex].x);
					timeRequired = noteLength / SettingsManager.instance.cursorSpeed;
					timeNote = Time.time + timeRequired;
					timeToggle = false;
				}

				if (Time.time > timeNote) {

					clipIndex++;
					timeToggle = true;
					lerpPara = 0;

				} else {
					
					Vector3 noteP1 = notePositions [clipIndex - 1];
					Vector3 noteP2 = notePositions [clipIndex];
					int n = noteTransposes [(int)noteP1.y - 1];
					int n2 = noteTransposes [(int)noteP2.y - 1];
					float startPitch = Mathf.Pow (2, (n + transpose) / 12.0f);
					float endPitch = Mathf.Pow (2, (n2 + transpose) / 12.0f);

					if (lerpPara < 1) {

						print ("True");
						float step = 1 / timeRequired;
						lerpPara += Time.deltaTime * step;					
						float pitch = Mathf.Lerp (startPitch, endPitch, lerpPara);
						aud.pitch = pitch;

					}



				}


			} else 
			{
				flag = false;
				clipIndex = 0;
				aud.Stop ();
			}
		}

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			
			timeCursor = 0;
		}
		timeCursor += Time.deltaTime;*/
	}

	IEnumerator PlayNotes()
	{
		timeCursor += Time.deltaTime;
		float roundedTime = Mathf.Round (timeCursor * 100f) / 100f;

		yield return null;
	}

	void OnTriggerEnter(Collider col)
	{
		//when collider hits, determine speed of cursor and time of next


		if (col.tag == "Cursor") 
		{
			//need to make it so that multiple notes can also be played
			aud.clip = clips [0];
			print (notePositions.Length);

			aud.pitch = 1;
			clipIndex++;
			aud.Play ();
			flag = true;
			timeToggle = true;
			//aud.Play ();
			//aud.Play ();
			/*var em = particleSys.emission;
			em.rateOverTime = 30;
			em = particleSys.emission;

			var shape = particleSys.shape;
			startScale = shape.scale;
			shape.scale = new Vector3 (startScale.x + 0.5f, startScale.y + 0.5f, startScale.z + 0.5f);

			ParticleSystem.MainModule main = particleSys.main;
			main.startColor = grad;
			main.startSize = 0.7f;
			main.startSpeed = 0.2f;*/

		}
	}



	public void SaveNoteReferences(Dictionary<int, Eppy.Tuple<Vector3, string>> refs)
	{
		noteReferences = refs;
		clips = new AudioClip[refs.Count];
		notePositions = new Vector3[refs.Count];
		accessAudioFiles ();
	}

	public void accessAudioFiles()
	{

		//clipsDict = new Dictionary<Vector3, List<AudioClip>> ();
		int counter = 0;
		
		foreach (KeyValuePair<int, Eppy.Tuple<Vector3, string>> entry in noteReferences) {

			notePositions[counter] = entry.Value.Item1;
			//float roundedTime = Mathf.Round (noteTimes [counter] * 100f) / 100f;
				
			string val = entry.Value.Item2;
			AudioClip clip = NoteReferenceDataBase.instance.getNoteAudio (val);
			clips [counter] = clip;

			/*if (clipsDict.ContainsKey (roundedT
			 * ime)) {

				clipsDict [roundedTime].Add (NoteReferenceDataBase.instance.getNoteAudio (val));
			} 
			else 
			{
				List<AudioClip> cList = new List<AudioClip> ();
				cList.Add (NoteReferenceDataBase.instance.getNoteAudio (val));
				clipsDict.Add (roundedTime, cList);
			}*/
			counter++;
		}
	}
}
