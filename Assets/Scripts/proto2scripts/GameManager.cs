//********************************************************************
//
//	GameManager!!!
//
//	I control a lot of stuff
//	attached to game object GM
//	there is only one instance of me
//	I currently also handle communication with the music program
//
//
//********************************************************************

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private int numSides;

	void Awake () {	
		//	create singleton instance of GameManager
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		//	for communication with music program
		OSCHandler.Instance.Init ();

		//	a message of 0 turns the signal processing on
		//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", 0);

		//	start game with 5 sides
		numSides = 5;

	}
	
	// Update is called once per frame
	void Update () {
		
		//	press Tab to resart a scene
		if (Input.GetKeyDown (KeyCode.Tab)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

	}

	//	decreases the number of sides the player has by 1
	public void DecrementNumSides() {
		numSides--;
		SendNumSides (numSides);
	}

	//	increases the number of sides the player has by 1
	public void IncrementNumSides() {
		numSides++;
		SendNumSides (numSides);
	}

	//	returns the number of sides the player has
	public int GetNumSides() {
		return numSides;
	}

	//	sends the number of sides the player has to the music program
	void SendNumSides(int x) {
		//OSCHandler.Instance.SendMessageToClient ("MAX", "127.0.0.1", x);
	}
}
