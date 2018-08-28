using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotController : MonoBehaviour {


	private CircleController theCirController;
	public float downSpeed = 2f;

	// Use this for initialization
	void Start () {
		theCirController = FindObjectOfType<CircleController>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * downSpeed);

		if(transform.position.y <= -6)
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//触发玩家的可点击状态
		if(other.tag == "Player")
		{
			theCirController.CanClick(this.gameObject.transform);	
		}
	}

	public void ReplaceCircle(CircleController theCirCtrl){
		theCirController = theCirCtrl;
	}
}
