//********************************************************************
//
//	PlayerScript
//
//	attached to the player (shape at the center of the screne)
//	
//	player actions:
//	rotate clockwise
//	rotate counter clockwise
//	pulse (the only real means of interacting with the space)
//
//	THIS ONE IS A MESS!!!
//
//	ADRIEN
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//	we can play with these 3 until we are happy!!!
	//	rotation speed
	float speed = 250;
	//	number of sides
	int numSides = 5;
	//	force for pulses
	float pulseForce = 10;

	bool movingRight = false;	//	this should be like... clockwise...
	bool movingLeft = false;	//	this should be like... counterclockwise...

	//	this is just a list of the prefab pulse shapes
	//	adrien is there a better way to access and instantiate these prefabs without having to make them public
	//	mostly a clutter thing
	public GameObject triPulse;
	public GameObject sqrPulse;
	public GameObject pentPulse;
	public GameObject hexPulse;

	//	just a list of its different appearances... (square, pentagon, triangle...)
	//	this too! is there another way to switch between sprites without having to make them
	//	public?
	public Sprite sprite3;
	public Sprite sprite4;
	public Sprite sprite5;
	public Sprite sprite6;

	Vector3 rotationEuler;

	//	color when player is deactivated (when numSides < 3)
	//	still LOOKS like a triangle but it's out of pulses and inert
	Color noRes = new Vector4(0.133f, 0.2f, 0.7f, 1.0f);
	
	// Update is called once per frame
	void Update () {

		//	gets user inputs, right arrow is clockwise rotation of player, left is counterclockwise...
		if (Input.GetKeyDown (KeyCode.RightArrow))
			movingRight = true;
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			movingLeft = true;

		if (Input.GetKeyUp (KeyCode.RightArrow))
			movingRight = false;
		if (Input.GetKeyUp (KeyCode.LeftArrow))
			movingLeft = false;

		//	converts into rotation information
		if (movingRight)
			transform.Rotate(Vector3.back * speed*Time.deltaTime);
		if (movingLeft)
			transform.Rotate(Vector3.forward * speed*Time.deltaTime);

		//	checks if player has hit spacebar... if so pulse...
		if (Input.GetKeyDown (KeyCode.Space))
			ExecutePulse ();

		//	after the pulse has been sent, it grabs its number of sides from the GM 
		//	(this is probably absurd but remember not only does sending a pulse affect the number of sides,
		//	so does collecting the food/pearls)
		numSides = GameManager.instance.GetNumSides ();

		//	adjusts color to indicate if player is inert (has less than 3 sides and can not send pulses into the space)
		if(numSides < 3)	//	NO PULSE!!!
			GetComponent<Renderer>().material.color = noRes;
		else //	you are still resonanting and can still pulse
			GetComponent<Renderer>().material.color = Color.white;

		//	picks the appropriate sprite, to match numSides
		//	if we're putting it in a separate method called by the Game Manager put this there too
		//	that way it's only called when needed and not executed every frame!
		if (numSides == 2)	//	you have less than 3 sides, but we don't wanna make you a line... so you're still a triangle
			this.GetComponent<SpriteRenderer> ().sprite = sprite3;
		if (numSides == 3)	//	you have 3 sides, you are a triangle
			this.GetComponent<SpriteRenderer> ().sprite = sprite3;
		if (numSides == 4)	//	you have 4 sides, you are a square
			this.GetComponent<SpriteRenderer> ().sprite = sprite4;
		if (numSides == 5)	//	pentagon
			this.GetComponent<SpriteRenderer> ().sprite = sprite5;
		if (numSides == 6)	//	hexagon
			this.GetComponent<SpriteRenderer> ().sprite = sprite6;

	}

	//	this is called when the player presses the spacebar
	//	it instantiates a one of the pulse prefabs based on the number of sides of the object
	//	(if numSides is 5 it will instantiate the pentPulse prefab)
	//	(if numSides is 4 it will instantiate the sqrPulse prefab)
	//	(if it has fewer than 3 sides it will not pulse
	void ExecutePulse() {
		
		//print ("Pulse strength = " + (pulseForce/numSides));

		if (numSides >= 6) {
			Instantiate (hexPulse, transform.position, transform.rotation);
			GameManager.instance.DecrementNumSides ();
		}if (numSides == 5) {
			Instantiate (pentPulse, transform.position, transform.rotation);
			GameManager.instance.DecrementNumSides ();
		} if (numSides == 4) {
			//THIS IS AWESOME! YES BUT LIKE... NO!
			//myPulse = Instantiate (sqrPulse, transform.position, transform.rotation) as GameObject;
			//myPulse.transform.parent = transform;
			Instantiate (sqrPulse, transform.position, transform.rotation);
			GameManager.instance.DecrementNumSides ();
		} if (numSides == 3) {
			Instantiate (triPulse, transform.position, transform.rotation);
			GameManager.instance.DecrementNumSides ();
		} if (numSides < 3) {
			//print ("no pulse");
		}
	}

}
