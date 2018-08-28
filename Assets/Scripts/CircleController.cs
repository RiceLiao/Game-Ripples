using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour {

	public CircleState theState = CircleState.normal;
	public float timeForClick;//为点击预留的时间
	private Transform collideredDot;//碰撞到的点
	public SpriteRenderer theSprReder;
	private Vector3 startScale;
	private float theAlpha = 1;//Circle的透明度
	public float downSpeed = 0.2f;//下降速度
	public float spreadSpeed = 0.2f;//传播速度
	public float alphaSpeed = 0.4f;//透明度递减速度
	public Sprite[] circleSprite = new Sprite[5];//替换Circle的元素
	public AudioSource[] clickAudio = new AudioSource[7];//替换碰撞音乐
	public GameObject preCircle;

	private GameManager theGM;
	private GameObject newCir;
	private DotController[] dots;
	void Start ()
	{	
		theGM = FindObjectOfType<GameManager>();
		theSprReder = GetComponent<SpriteRenderer>();

		//修改所有Dot的碰撞对象以及GM绑定的对象
		dots = FindObjectsOfType<DotController>();
		foreach(DotController dot in dots)
		{
			dot.ReplaceCircle(this);
		}
		theGM.ReplaceCircle(this);

		startScale = transform.localScale;
		theAlpha = 1;
		theState = CircleState.normal;
	}
	
	void Update ()
	{
		if(theGM.gameState == GameState.Start)
		{
			// if(theState == CircleState.normal || theState == CircleState.canClick)
			// {
			// 	//改变Circle的position、scale 和 透明度
			// 	transform.localScale = Vector3.Lerp(transform.localScale, 
			// 	new Vector3(this.transform.localScale.x  + spreadSpeed, this.transform.localScale .y + spreadSpeed,1f), Time.deltaTime);
			// 	transform.Translate(Vector3.down * Time.deltaTime * downSpeed);
			// 	theSprReder.color = new Color( theSprReder.color.r, theSprReder.color.g, theSprReder.color.b,theAlpha);
			// 	theAlpha -= Time.deltaTime * alphaSpeed;
			// }

			//改变Circle的position、scale 和 透明度
			transform.localScale = Vector3.Lerp(transform.localScale, 
			new Vector3(this.transform.localScale.x  + spreadSpeed, this.transform.localScale .y + spreadSpeed,1f), Time.deltaTime);
			transform.Translate(Vector3.down * Time.deltaTime * downSpeed);
			theSprReder.color = new Color( theSprReder.color.r, theSprReder.color.g, theSprReder.color.b,theAlpha);
			theAlpha -= Time.deltaTime * alphaSpeed;

			//判断能否继续下一个圆
			if(theState == CircleState.canClick && Input.GetMouseButtonDown(0) && collideredDot != null)
			{
				//设置为失效状态
				theState = CircleState.disable;
				GetComponent<CircleCollider2D>().enabled = false;
				// transform.position = collideredDot.position;
				// transform.localScale = startScale;
				//新生成波纹
				newCir = Instantiate(preCircle,collideredDot.position,Quaternion.identity);
				newCir.transform.localScale = startScale;
				newCir.GetComponent<CircleCollider2D>().enabled = true;
				//重置透明度
				// theAlpha = 1;
				// theState = CircleState.normal;
				Destroy(collideredDot.gameObject);
				//播放音效
				clickAudio[UnityEngine.Random.Range(0,6)].Play();

			}else if(theState == CircleState.normal && Input.GetMouseButtonDown(0))
			{
				theGM.ReduceScore(1);
			}
			if((theState == CircleState.normal || theState == CircleState.canClick) && theAlpha <= 0f)
			{
				Debug.Log(this.gameObject.name + " Over");
				PlayDeath();
			}else if(theAlpha <= 0f){
				Debug.Log("Destory");
				Destroy(this.gameObject);
			}
		}

	}

	//玩家此时可以点击屏幕来继续下一个圆
	public void CanClick(Transform theDot)
	{
		theState = CircleState.canClick;
		collideredDot = theDot;
		Invoke("CanNotClick",timeForClick);
	}

	public void CanNotClick()
	{
		if(theState == CircleState.canClick)
			theState = CircleState.normal;
	}

	public void PlayDeath()
	{
		theGM.gameState = GameState.Ending;
		//保存最高分
		if(theGM.GetCurScore() > theGM.GetHighestScore())
		{
			PlayerPrefs.SetInt("HighestScore",theGM.GetCurScore());
		}
		theGM.GameOver();
	}

	public void TurnSprite(int spriteNum)
	{
		theSprReder.sprite = circleSprite[spriteNum];
	}
}
