//********************************************************************
//
//	PulseGroupScript
//
//	this script is on the prefabs of pulses
//	(pentpulse, tripulse, sqr pulse)
//	this just destroys the pulse so we don't have a ton of
//	invisible game objects floating around the scene
//
//	in retrospect we could probably just destroy it after a certain
//	duration of time... cause we should be able to calculate how
//	fast and far each pulse is traveling and about when it disappears
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class PulseGroupScript : MonoBehaviour {

	public float pulseMult;
	
	// Update is called once per frame
	void Update () {
		if (transform.childCount < 1) {
			Destroy (gameObject);
		}
	}
}
