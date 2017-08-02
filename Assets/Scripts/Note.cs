using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note {

	private List<Vector3> points;

	public Note()
	{
		points = new List<Vector3> ();
	}
		
	public void AddPoint(Vector3 point)
	{
		if (points.Count != 0) {

			if (!Vector3.Equals (LastNode (), point)) {

				points.Add (point);
			}

		} else {

			points.Add (point);
		}

	}

	public Vector3 LastNode()
	{
		int count = points.Count;
		return points [count - 1];
	}

	public List<Vector3> getNodes()
	{
		return points;
	}

	public string GetNoteString(Vector3 noteStartPoint, Vector3 noteEndPoint)
	{
		float instrument = 1;
		float pitch = (int)noteStartPoint.y;
		float note = 0;
		float length = Mathf.Abs(noteEndPoint.x - noteStartPoint.x);
		if (length >= 0f && length < 1f) {

			note = 1;
		} else if (length >= 1f && length < 2f) {

			note = 2;
		} else if (length >= 2f && length < 3f) {
			note = 3;
		} else if (length >= 3f && length < 4f) {
			note = 4;
		} else if (length >= 4f) {
			note = 5;
		}
		string keyReference = instrument.ToString () + pitch.ToString () + note.ToString ();
		Debug.Log(keyReference);
		//return keyReference;
		return "longnote";
	}
}
