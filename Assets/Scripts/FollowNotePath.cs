using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNotePath : MonoBehaviour {

	public List<Vector3> nodes;
	private Vector3[] directions;

	ParticleSystem p;

	private Dictionary<int, ParticleSystem.Particle[]> particleArrayMap;

	private bool flag = false;
	ParticleSystem.Particle[] particleList;
	private Vector3[] newPoints;



	float totalDistance = 0;


	void Start()
	{
		p = GetComponent<ParticleSystem>();
		particleArrayMap = new Dictionary<int, ParticleSystem.Particle[]> ();

	}


	void Update() {


		if (flag) {

			int particleCount = p.particleCount;
			if (particleArrayMap.ContainsKey (particleCount)) {

				particleList = particleArrayMap [particleCount];

			} else 
			{
				particleArrayMap.Add (particleCount, new ParticleSystem.Particle[particleCount]);
				particleList = particleArrayMap [particleCount];
			}


			//particleList = new ParticleSystem.Particle[p.particleCount];
			int partCount = p.GetParticles(particleList);

			//ParticleSystem p = (ParticleSystem) GetComponent<ParticleSystem>();
			//ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[p.particleCount];
			//int partCount = GetComponent<ParticleSystem>().GetParticles(particleList);


			for(int i = 0; i < partCount; i++) {


				int count = 0;

				float timeAlive = particleList[i].startLifetime - particleList[i].remainingLifetime;							
				while (timeAlive > GetCumulativeSegmentTime (count)) {

					count++;

				}
			
				if (count < directions.Length) {
					particleList [i].velocity = (directions[count].normalized * directions [count].magnitude) / GetSingleSegmentTime(count);

				}

				//print (count);
				//float dist = GetAddedMagnitude((int)(timeAlive));


				/*while (dist > GetAddedMagnitude (count)) {

					count++;

					//print (count);
				}*/

				//float a = particleList [i].remainingLifetime / directions [count].magnitude;
				//particleList [i].remainingLifetime -= a;

				//print (count);

				//print(directions[count]);*/
				/*float lerp = 1 - (particleList[i].lifetime / particleList[i].startLifetime);
				Vector3 currentPointPos;
				if (!particleFlag) {
					currentPointPos = CatmullRomSplin.Interp (newPoints, para);
					para += Time.deltaTime * 0.3f;
					particleFlag = true;
				}
				//lerp = Mathf.Lerp (0, 1, para);
				Vector3 newPosition = CatmullRomSplin.Interp (newPoints, para);

				if (para > 1) {
					para = 0;
				} 

				Vector3 dir = newPoints - */


				//print (lerp + " lerp");
				//print (newPosition + "new Position");

				//particleList [i].position = Vector3.Lerp (particleList [i].position, newPosition + Random.insideUnitSphere * 0.25f, Time.deltaTime);
				//particleList [i].position = newPosition;
					
			}
			p.SetParticles(particleList, partCount);
		}

	}

	private float GetAddedMagnitude(int count) {
		float addedMagnitude = 0;
		for(int i = 0; i < count; i++) {
			addedMagnitude += directions[i].magnitude;
		}
		return addedMagnitude;
	}

	private float GetSingleSegmentTime(int segmentNumber)
	{

		return (directions [segmentNumber].magnitude / totalDistance) * p.startLifetime;
	}

	private float GetCumulativeSegmentTime(int segmentNumber)
	{
		float totalSegmentDistance = 0;
		for (int i = 0; i <= segmentNumber; i++) {

			totalSegmentDistance += (directions [i].magnitude);
		}
		float totalSegmentTimeFraction = (totalSegmentDistance / totalDistance) * p.startLifetime;
		//print (totalSegmentTimeFraction);
		return totalSegmentTimeFraction;
	}


	public void SetNodes(List<Vector3> nodeList)
	{
		nodes = nodeList;

		if(nodes.Count == 0)
			Debug.LogError("Nodes needs to have at least 1 item");
		directions = new Vector3[nodes.Count - 1];
		for(int i = 1; i <= directions.Length; i++) {
			//directions[i] = (nodes[i] - ((i-1 >= 0) ? nodes[i-1] : transform.position));
			directions[i - 1] = nodes[i] - nodes[i - 1];

		}

		for (int i = 0; i < directions.Length; i++) {

			totalDistance += directions [i].magnitude;
		}
			
		GetComponent<ParticleSystem> ().startLifetime = 4f;		
		//GetComponent<ParticleSystem> ().startLifetime = totalDistance * 8f;


		//newPoints = CatmullRomSplin.PathControlPointGenerator (nodes.ToArray ());
		flag = true;
	}
		
}
