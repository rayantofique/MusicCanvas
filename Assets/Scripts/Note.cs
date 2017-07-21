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
		//will reference a static noteDatabase
		//could be a map with a string, string
		//if located entry in map is empty, then lift from Resources and add to map, else play from scene
		float instrument = noteStartPoint.z;
		float pitch = noteStartPoint.y;
		float note = Mathf.Abs(noteEndPoint.z - noteStartPoint.z);
		string keyReference = instrument.ToString () + pitch.ToString () + note.ToString ();
		return keyReference;
	}
}
