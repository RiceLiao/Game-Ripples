using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour {

	public float timeOfScene = 20f;//每场景停留时间
	private float remainTime;
	public float downSpeed = 1f;//背景切换速度
	public float downLength = 13.34f;//背景下落距离
	private float targetLength;
	private GameManager theGameManager;
	private Vector3 startPos;
	public Vector3 relocatedPos;//背景切到初始位置的点
	void Start () {
		theGameManager = FindObjectOfType<GameManager>();
		remainTime = timeOfScene;
		targetLength = transform.position.y - downLength;
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		remainTime -= Time.deltaTime;
		if(remainTime <= 0f)
		{
			transform.Translate(Vector3.down * Time.deltaTime * downSpeed);
			if(transform.position.y <= targetLength)
			{
				//改变关卡数、Circle的Sprite，以及触发下一次切换场景的倒计时
				if(theGameManager.level < 5)
					theGameManager.level += 1;
				theGameManager.theCircle.TurnSprite(theGameManager.level-1);
				remainTime = timeOfScene;
				targetLength = transform.position.y - downLength;
			}
		}
		//背景切换到初始位置
		if(transform.position.y <= relocatedPos.y )
		{
			transform.position = startPos;
		}
	}
}
