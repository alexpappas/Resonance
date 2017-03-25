//********************************************************************
//
//	FoodScript
//
//	i'm attached to the little circular pearls or bubbles
//	all i do is destroy myself when hit by a pulse
//	when that happens i also tell the game manager i have been 
//	"eaten" and give the player an additional side
//
//	ADRIEN!!!
//
//********************************************************************

using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D coll) {		
		GameManager.instance.IncrementNumSides ();
		Destroy (this.gameObject);
	}
}
