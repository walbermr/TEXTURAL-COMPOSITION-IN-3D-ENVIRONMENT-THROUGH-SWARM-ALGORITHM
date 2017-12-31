using UnityEngine;
using System.Collections;

public class boidsGeneration : MonoBehaviour {

//	public class Boid
//	{
//		public AudioClip boid_sound;
//		public GameObject boid_body;
//		public string boid_name;
//
//
//		public Boid(string name)
//		{
//			boid_name = name;
//			boid_body = GameObject.CreatePrimitive (PrimitiveType.Sphere);
//			boid_sound = (AudioClip) Resources.Load(boid_name);
//		}
//
//	}

	//store gameObject reference on global contex
	public int QuantityBoids;
	public float VisionRange;
	public float SeparationWeight;
	public float CohesionWeight;
	public float AlignmentWeight;
	public float SafeDistance;
	public float MaxAbsoluteVelocity;

	private int FrameInterval;

	public float XBorder;
	public float YBorder;
	public float ZBorder;

	public GameObject[] Flock;

	GameObject CreateBoid(int i)
	{
		//spawn boid
		GameObject boid = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		boid.name = "boid" + i.ToString();

		//add components
		boid.AddComponent<boidBehaviour> ();
		boid.AddComponent<Rigidbody> ();
		boid.AddComponent<AudioSource> ();

		//edit components
		Vector3 position = new Vector3(
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f), 
			Random.Range(-10.0f, 10.0f));

		Vector3 velocity = new Vector3(
			Random.Range(-2.0f, 2.0f), 
			Random.Range(-2.0f, 2.0f), 
			Random.Range(-2.0f, 2.0f));

		boid.transform.position = position;

		Rigidbody rb = boid.GetComponent<Rigidbody> ();
		rb.useGravity = false;
		rb.isKinematic = false;
		rb.velocity = velocity;

		boid.GetComponent<boidBehaviour> ().interval = (i % this.FrameInterval) + 1;

		AudioSource audio = boid.GetComponent<AudioSource> ();
		AudioClip audio_clip = Resources.Load<AudioClip>("Sounds/" + boid.name);
		audio.spatialBlend = 0.75f;
		audio.clip = audio_clip;
		audio.loop = true;
		audio.Play ();

		return boid;
	}

	void Start()
	{
		this.FrameInterval = QuantityBoids;
		Flock = new GameObject[QuantityBoids];
		for (int i = 0; i < QuantityBoids; i++)
			Flock [i] = CreateBoid (i);

	}
}