using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public List<GameObject> Flock;

	private GameObject DefaultBoid(GameObject boid, int i){
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
		return boid;
	}

	private AudioSource SetBoidDefaultSound(AudioSource audio, AudioClip clip){
		audio.spatialBlend = 0.75f;
		audio.clip = clip;
		audio.loop = true;
		return audio;
	}

	public GameObject CreateBoid(int i)
	{
		//spawn boid
		GameObject boid = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		boid.name = "boid" + i.ToString();

		boid = DefaultBoid (boid, i);

		AudioSource audio = boid.GetComponent<AudioSource> ();
		AudioClip clip = Resources.Load<AudioClip>("Sounds/" + boid.name);
		SetBoidDefaultSound (audio, clip);
		audio.Play ();

		return boid;
	}

	public GameObject CreateBoid(int i, AudioClip clip){
		//spawn boid
		GameObject boid = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		boid.name = "boid" + i.ToString();

		boid = DefaultBoid (boid, i);

		AudioSource audio = boid.GetComponent<AudioSource> ();
		SetBoidDefaultSound (audio, clip);
		audio.Play ();

		return boid;
	}

	public void AddBoid(int i){
		this.FrameInterval = Flock.Count + 1;
		Flock.Add(CreateBoid (i));
	}

	public void AddBoid(int i, AudioClip clip){
		this.FrameInterval = Flock.Count + 1;
		Flock.Add(CreateBoid (i, clip));
	}

	void Start()
	{
		Flock = new List<GameObject> ();
//		for (int i = 0; i < QuantityBoids; i++)
//			Flock [i] = CreateBoid (i);

	}
}