using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnMouseDown()
	{
		if(GetComponent<SpriteRenderer>().enabled == true)
		{
			Time.timeScale = 1;
			Destroy(this.gameObject);
		}
	}
}
