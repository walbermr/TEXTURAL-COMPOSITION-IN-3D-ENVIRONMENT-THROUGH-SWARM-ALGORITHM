using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidBehaviour : MonoBehaviour {

	public Vector3 BOID_POS;
	private GameObject[] Boids;

	// Use this for initialization
	void Start () 
	{
		//find screen behaviour object
		GameObject sceneBehaviour = GameObject.Find("SceneBehaviour");
		//get boids vector
		boidsGeneration boidsGenerationScript = sceneBehaviour.GetComponent<boidsGeneration>();
		Boids = boidsGenerationScript.Boids;
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject boid in Boids)
		{
			Vector3 position = new Vector3(
				Random.Range(-0.50f, 0.50f), 
				Random.Range(-0.50f, 0.50f), 
				Random.Range(-0.50f, 0.50f));
			
			boid.transform.position += position;
		}
	}
}
