using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteReferenceDataBase : MonoBehaviour {


	public static NoteReferenceDataBase instance = null;
	private Dictionary<string, AudioClip> audioClipDatabase;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {

			Destroy (gameObject);
		}
	}

	void Start()
	{
		audioClipDatabase = new Dictionary<string, AudioClip> ();
	}

	//single function, which will check if called


	public AudioClip getNoteAudio(string strRef)
	{
		//check if called Note exists, if not then load from resources, else return directly
		if (audioClipDatabase.ContainsKey (strRef)) {

			return audioClipDatabase [strRef];
		}
		else
		{
			AudioClip clip = Resources.Load ("Notes/" + strRef) as AudioClip;
			if (clip == null) {

				Debug.Log ("File does not exist in resource");
				return null;
			}
			audioClipDatabase.Add (strRef, clip);
			return audioClipDatabase [strRef];
		}
	}
}