using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : MonoBehaviour {

	private GameManager theGM;
	// Use this for initialization
	void Start () {
		theGM = FindObjectOfType<GameManager>();
	}

	public void RestartGame(){
		theGM.ResetGame();
	}

}
