using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressController : MonoBehaviour {

	private float downSpeed;
	public float minDownSpeed = 0.1f;
	public float maxDownSpeed = 0.9f;
	// Use this for initialization
	void Start () {
		downSpeed = UnityEngine.Random.Range(minDownSpeed, maxDownSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * downSpeed);

		if(transform.position.y <= -10)
		{
			Destroy(this.gameObject);
		}
	}
}
