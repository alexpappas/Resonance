using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PappasBigRotate : MonoBehaviour {

	public bool isRotating;

	float startRotation;

	// Use this for initialization
	void Start () {
		isRotating = false;
	}
	
	// Update is called once per frame
	void Update () {

		//	so this is SUPER buggy, with unity flips to negative angles
		if (isRotating && ((transform.rotation.eulerAngles.z - startRotation) > 72)) {
			isRotating = false;
			foreach (Transform child in this.transform) {
				if (child.CompareTag ("resonator")) {
					child.GetComponent<ResonatorScript> ().hasBeenActivated = false;
					child.GetComponent<ResonatorScript> ().isResonating = false;
				}
			}
		}

		CheckShouldRotate ();

		if (isRotating) {
			SpinIt ();
		}

	}

	//	if you have unenabled children in this object it WILL NOT ROTATE!!!
	void CheckShouldRotate() {
		bool please = true;
		foreach (Transform child in this.transform) {
			if (child.CompareTag ("resonator")) {
				please = please && child.GetComponent<ResonatorScript> ().hasBeenActivated;
			}
		}

		if (please) {
			isRotating = true;
		}

	}

	void SpinIt() {
		this.transform.RotateAround (this.transform.position, this.transform.forward, 1);
	}

}
