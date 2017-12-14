using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidBehaviour : MonoBehaviour
{
	private GameObject[] Flock;
	private float VisionRange;
	private List<GameObject> VisionField;
	private float SeparationWeight;
	private float CohesionWeight;
	private float AlignmentWeight;
	private float SafeDistance;
	private float MaxAbsoluteVelocity;

	private float XBorder;
	private float YBorder;
	private float ZBorder;

	public int interval;

	// Use this for initialization
	void Start () {
		//find screen behaviour object
		GameObject sceneBehaviour = GameObject.Find("SceneBehaviour");
		//get boids vector
		boidsGeneration boidsGenerationScript = sceneBehaviour.GetComponent<boidsGeneration>();
		Flock = boidsGenerationScript.Flock;
		VisionRange = boidsGenerationScript.VisionRange;
		SeparationWeight = boidsGenerationScript.SeparationWeight;
		CohesionWeight = boidsGenerationScript.CohesionWeight;
		AlignmentWeight = boidsGenerationScript.AlignmentWeight;
		SafeDistance = boidsGenerationScript.SafeDistance;
		MaxAbsoluteVelocity = boidsGenerationScript.MaxAbsoluteVelocity;

		XBorder = boidsGenerationScript.XBorder;
		YBorder = boidsGenerationScript.YBorder;
		ZBorder = boidsGenerationScript.ZBorder;
	}

	private List<GameObject> BoidsSigthed(GameObject boid){
		List<GameObject> boid_vision = new List<GameObject>();

		foreach (GameObject other_boid in Flock){
			Vector3 other_boid_position = other_boid.transform.position;
			Vector3 vectorial_distance = other_boid_position - boid.transform.position;
			float scalar_distance = vectorial_distance.magnitude;

			if ((other_boid.name != boid.name) && (scalar_distance <= VisionRange)){
				boid_vision.Add (other_boid);
				Debug.Log (other_boid.name + " sigthed by " + boid.name);
			}
		}

		return boid_vision;
	}

	private Vector3 SeparationMovement(GameObject boid){
		Vector3 movement_vector = Vector3.zero;

		foreach (GameObject other_boid in VisionField) {
			Vector3 distance_vector = other_boid.transform.position - boid.transform.position;
			if(distance_vector.magnitude <= SafeDistance)
				movement_vector += distance_vector;
		}

		Debug.Log (boid.name + "SeparationMovement " + movement_vector.ToString ());
		return -movement_vector;
	}

	private Vector3 CohesionMovement(GameObject boid){
		Vector3 movement_vector = Vector3.zero;
		Vector3 center_of_mass = Vector3.zero;

		if (VisionField.Count > 0) {
			foreach (GameObject other_boid in VisionField) {
				center_of_mass += other_boid.transform.position;
			}
				
			center_of_mass /= VisionField.Count;
			movement_vector = center_of_mass - boid.transform.position;
		}
		Debug.Log (boid.name + "CohesionMovement " + movement_vector.ToString ());
		return movement_vector;
	}

	private Vector3 AlignmentMovement(GameObject boid){
		Vector3 movement_vector = Vector3.zero;

		if (VisionField.Count > 0) {
			foreach (GameObject other_boid in VisionField) {
				Rigidbody other_boid_rb = other_boid.GetComponent<Rigidbody> ();
				movement_vector += other_boid_rb.velocity;
			}

			movement_vector /= VisionField.Count;
		}
		Debug.Log (boid.name + "AlignmentMovement " + movement_vector.ToString ());
		return movement_vector;
	}

	private void UpdateVelocity(GameObject boid){		
		Rigidbody boid_rb = boid.GetComponent<Rigidbody> ();
		boid_rb.velocity += (SeparationWeight * SeparationMovement (boid))
			+ (CohesionWeight * CohesionMovement (boid))
			+ (AlignmentWeight * AlignmentMovement (boid));

		boid_rb.velocity = boid_rb.velocity.normalized * MaxAbsoluteVelocity;

		//Debug.Log (boid_rb.velocity.ToString ());
		return;
	}

	private void CheckBounds(GameObject boid){
		float x = boid.transform.position.x;
		float y = boid.transform.position.y;
		float z = boid.transform.position.z;

		Rigidbody rb = boid.GetComponent <Rigidbody> ();
		float v_x = rb.velocity.x;
		float v_y = rb.velocity.y;
		float v_z = rb.velocity.z;

		if (x > XBorder) {
			boid.transform.position = new Vector3 (XBorder, y, z);
			rb.velocity = -rb.velocity;
		}
		
		else if (x < -XBorder) {
			boid.transform.position = new Vector3 (-XBorder, y, z);
			rb.velocity = -rb.velocity;
		}
		
		if (y > YBorder) {
			boid.transform.position = new Vector3 (x, YBorder, z);
			rb.velocity = -rb.velocity;
		}
		
		else if (y < -YBorder) {
			boid.transform.position = new Vector3 (x, -YBorder, z);
			rb.velocity = -rb.velocity;
		}

		if (z > ZBorder) {
			boid.transform.position = new Vector3 (x, y, ZBorder);
			rb.velocity = -rb.velocity;
		}
		
		else if (z < -ZBorder) {
			boid.transform.position = new Vector3 (x, y, -ZBorder);
			rb.velocity = -rb.velocity;
		}

		return;
	}

	// Update is called once per frame
	void Update () {
		if (Time.frameCount % this.interval == 0)
		{
			GameObject thisBoid = this.gameObject;
			VisionField = BoidsSigthed (thisBoid);

			UpdateVelocity (thisBoid);
			CheckBounds (thisBoid);
		}
	}

}
