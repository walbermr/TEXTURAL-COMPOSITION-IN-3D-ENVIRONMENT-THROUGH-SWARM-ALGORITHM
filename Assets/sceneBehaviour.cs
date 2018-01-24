using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneBehaviour : MonoBehaviour {
	private bool _isRecording = false;

	void Start(){
		Application.targetFrameRate = 30;
		ActivateMultiDisplays ();
	}
		
	private void ActivateMultiDisplays(){
		int qtDisplays = Display.displays.Length;
		Debug.Log("displays connected: " + qtDisplays);

		for (int i = 1; i < qtDisplays; i++)
			Display.displays [i].Activate ();

	}

	IEnumerator StartRecord(int time, Text buttonText, string text){
		AudioClip newSound;
		GameObject scene = this.gameObject;
		int boidNumber = scene.GetComponent<boidsGeneration> ().Flock.Count;

		buttonText.color = Color.green;
		int recordTime = time;
		for (int i = recordTime; i > 0; i--) {
			buttonText.text = text + i.ToString ();	
			yield return new WaitForSeconds (1);
		}


		text = "Recording... ";
		recordTime = Random.Range (2, 5);
		newSound = Microphone.Start(null, false, recordTime, 44100);
		buttonText.color = Color.red;	
		for (int i = recordTime; i > 0; i--) {
			buttonText.text = text + i.ToString ();	
			yield return new WaitForSeconds (1);
		}

		SavWav.Save (Application.dataPath + "/Resources/Sounds", "boid" + boidNumber.ToString(), newSound);
		buttonText.text = "Record";
		buttonText.color = Color.black;
		_isRecording = false;

		scene.GetComponent<boidsGeneration> ().AddBoid (boidNumber, newSound);
	}

	public void RecordButtonBehaviour(){
		if (_isRecording == false) {
			_isRecording = true;
			print ("Record Started");
			GameObject record_button = GameObject.Find ("Record Button");
			Text buttonText = record_button.GetComponentInChildren<Text> ();
			StartCoroutine (StartRecord (5, buttonText, "Recording in "));
		}
	}
}
