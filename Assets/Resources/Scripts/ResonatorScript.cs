//********************************************************************
//
//	ResonatorScript
//
//	attached to the resonating shapes scattered around the level
//	
//	Resonator Logic!
//
//	a resonator is similar to the player
//	it is stationary
//	and will send out pulses when activated
//	resonators are activated (isResonating = true) when it is triggered by another object's pulse

//	when activated, a resonator will pulse a certain number of times before turning itself OFF
//	triangle will pulse 3 times
//	squares will pulse 4 times
//	etc
//
//	a resonator that is OFF will turn ON when triggered
//	a resonator that is ON will turn OFF when triggered
//
//	RUOWEN!!!!
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class ResonatorScript : MonoBehaviour {

	//	what shape the resonator is
	public int numSides;

	//	these are the pulse prefabs
	public GameObject triPulse;
	public GameObject sqrPulse;
	public GameObject pentPulse;

	//	this is the child object of the resonator
	//	its the colored object that sorta grows behind it as it resonates
	//	don't mess with resonArea too much
	//	i may ditch it for aesthetic reasons!
	public GameObject resonArea;

	//	if the object is ON (pulsing) = true
	//	of the object is OFF (not pulsing) = false
	public bool isResonating;

	//	has it been turned on at least once?
	public bool hasBeenActivated;

	//	color when it is not resonating
	Color noRes = new Vector4(0.133f, 0.2f, 0.7f, 1.0f);

	//	equal to the number of sides
	//	for now
	//	please have fun with this!
	public int MAXPULSES;

	//	MAXPULSES - how many times it has pulses since being turned ON
	int pulsesRemaining;

	void Start () {
		
		//	nothing is resonating on start
		isResonating = false;
		hasBeenActivated = false;

		GetComponent<Renderer>().material.color = noRes;

		//	here's where you can mess with pulses
		MAXPULSES = numSides;
		//MAXPULSES = 1;

	}

	void Update() {
		//	leave this one alone for now
		//	basically checks if it is in the rotating pentagon
		//	and if that pentagon is rotating it turns off
		//	if people like it this week this is our next big project
		//	that i'm really gonna need you're help with!
		if (this.GetComponentInParent<PappasBigRotate> () == null) {
			//print ("AH");
		} else if (this.GetComponentInParent<PappasBigRotate> ().isRotating) {
			isResonating = false;
			SetMyColor ();
			CancelInvoke ("ExecutePulse");
		}
	}

	//	I HAVE BEEN ACTIVATED BY ANOTHER PULSE!
	void OnTriggerEnter2D(Collider2D coll) {

		hasBeenActivated = true;
			//	I was OFF and now I am ON
			if (!isResonating) {
				//	waits for a second, before pulsing once every 4 seconds	
				InvokeRepeating ("ExecutePulse", 1, 4);
				isResonating = true;
				//	I was ON and now I am OFF
			} else if (isResonating) {
				isResonating = false;
				CancelInvoke ("ExecutePulse");
			}
			
		SetMyColor ();

		pulsesRemaining = MAXPULSES;
		
	}

	//	resonators pulsate at regular intervals
	//	the shape of this resonator will determine which pulse prefab to instantiate
	void ExecutePulse() {

		if (numSides == 5) {
			Instantiate (pentPulse, transform.position, transform.rotation);
		} if (numSides == 4) {
			Instantiate (sqrPulse, transform.position, transform.rotation);
		} if (numSides == 3) {
			Instantiate (triPulse, transform.position, transform.rotation);
		}
			
		SendNumSides (numSides);

		pulsesRemaining--;

		//	resonArea is a child of this resonator
		//	each time the resonator sends out a pulse
		//	the area of the sprite behind it increases
		//	these are the growing colored areas that radiate out from the resonators
		//	this is the thing that i may not include in the future
		resonArea.transform.localScale *= 2;

		//print ("I have " + numSides + " sides : " + pulsesRemaining);

		//	turns the resonator off if that was its last pulse
		if (pulsesRemaining < 1) {
			isResonating = false;
			SetMyColor ();
			CancelInvoke ("ExecutePulse");
		}

	}

	//	if it is resonating its color is white
	//	if it is not resonating its a darking kinda blackish color
	void SetMyColor () {
		if (isResonating) {
			GetComponent<Renderer> ().material.color = Color.white;
		} else if (!isResonating && hasBeenActivated) {
			GetComponent<Renderer> ().material.color = Color.gray;
		} else if (!isResonating && !hasBeenActivated) {
			GetComponent<Renderer>().material.color = noRes;
		}
	}

	//	sends its identity to the music program
	//	each resonator makes a different sound when it pulses depending on its shape
	void SendNumSides(int x) {
		//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", x);
	}
}
