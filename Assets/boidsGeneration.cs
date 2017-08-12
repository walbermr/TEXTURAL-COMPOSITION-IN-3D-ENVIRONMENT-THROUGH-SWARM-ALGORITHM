using UnityEngine;
using System.Collections;

public class boidsGeneration : MonoBehaviour {
	//store gameObject reference on global contex
	public int NumberOfBoids;
	public GameObject[] Boids;


	GameObject CreateBoid()
	{
		//spawn boid
		GameObject boid = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//change components
		Vector3 position = new Vector3(
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f));
		boid.transform.position = position;
		boid.name = "boid";
		//add components
		boid.AddComponent<boidBehaviour>();

		return boid;
	}

	void Start()
	{
		Boids = new GameObject[NumberOfBoids];
		for (int i = 0; i < NumberOfBoids; i++)
			Boids [i] = CreateBoid ();

	}
}