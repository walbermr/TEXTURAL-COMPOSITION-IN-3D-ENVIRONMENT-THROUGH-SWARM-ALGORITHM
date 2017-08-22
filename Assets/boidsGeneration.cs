using UnityEngine;
using System.Collections;

public class boidsGeneration : MonoBehaviour {
	//store gameObject reference on global contex
	public int QuantityBoids;
	public float VisionRange;
	public float SeparationWeight;
	public float CohesionWeight;
	public float AlignmentWeight;
	public float SafeDistance;
	public float MaxAbsoluteVelocity;

	public float XBorder;
	public float YBorder;
	public float ZBorder;

	public GameObject[] Flock;

	GameObject CreateBoid(int i)
	{
		//spawn boid
		GameObject boid = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		//change components
		Vector3 position = new Vector3(
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f));
		
		Vector3 velocity = new Vector3(
			Random.Range(-1.0f, 1.0f), 
			Random.Range(-1.0f, 1.0f), 
			Random.Range(-1.0f, 1.0f));
		
		boid.transform.position = position;
		boid.name = "boid" + i.ToString();
		//add components
		boid.AddComponent<boidBehaviour> ();
		boid.AddComponent<Rigidbody> ();

		//edit components
		Rigidbody rb = boid.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.isKinematic = false;
		rb.velocity = velocity;

		return boid;
	}

	void Start()
	{
		Flock = new GameObject[QuantityBoids];
		for (int i = 0; i < QuantityBoids; i++)
			Flock [i] = CreateBoid (i);

	}
}