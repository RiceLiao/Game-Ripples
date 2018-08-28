using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartController : MonoBehaviour {

	private Animator anim;
	private GameManager GM;
	private bool downable = false;
	//新手提示图
	// private SpriteRenderer theHint;

	//highest score的对象
	private GameObject thsHighScoreText;

	void Start () {
		thsHighScoreText = GameObject.Find("score_text");
		// theHint = GameObject.Find("Hint").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		GM = FindObjectOfType<GameManager>();
	}

	void Update(){
		//触发起点下落
		if(downable && this.gameObject.name == "qishidian")
		{
			anim.enabled = false;
			transform.Translate(Vector3.down * Time.deltaTime * GM.theCircle.downSpeed);
		}
	}

	public void DestoryMyself()
	{
		Destroy(this.gameObject);
	}

	public void StartGame()
	{
		//根据对应的物体触发对应的开场效果
		if(this.gameObject.name == "logo")
		{
			anim.Play("logo_up");
		}else if(this.gameObject.name == "play")
		{
			anim.Play("playButton_disapper");
		}else if(this.gameObject.name == "qishidian")
		{
			anim.Play("qidian_down");
		}else if(this.gameObject.name == "HighestScoreHolder")
		{
			if(thsHighScoreText.GetComponent<Text>().color.a >= 0.6f && thsHighScoreText.GetComponent<Text>().color.a <= 1f)
			{
				anim.Play("hScore_disapper75");
			}else if(thsHighScoreText.GetComponent<Text>().color.a >= 0.2f && thsHighScoreText.GetComponent<Text>().color.a < 0.6f){
				anim.Play("hScore_disapper40");
			}else{
				Destroy(this.gameObject);
			}
		}
	}

	void OnMouseDown()
	{
		if(this.gameObject.name == "play")
		{
			GM.StartGame();
		}
	}

	public void Downable(){
		downable = true;
		// if(PlayerPrefs.HasKey("NeedHint") && PlayerPrefs.GetInt("NeedHint")==0 )
		// {
		// 	Time.timeScale = 0f;
		// 	theHint.enabled = true;
		// 	PlayerPrefs.SetInt("NeedHint",1);
		// }
	}
}
