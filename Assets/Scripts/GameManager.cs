using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public float timeForStarting2 = 1f;
	public float timeForStart = 1.5f;
	public float timeForCreatDot = 3f;
	public float timeForCreatDress = 4f;
	private float remainTime;
	private float remainTimeOfDress;
	//游戏开始时间，用于计分
	private float startTime;
	//续命点
	public GameObject[] theDot = new GameObject[2];
	public int level;
	public CircleController theCircle;
	public GameObject[] dressOfBG = new GameObject[5];
	public GameState gameState = GameState.Ready;
	//存放当前分数
	public Text textScore;
	//存放最高分数
	public Text textHighestScore;
	private int score;
	private int highestScore = 0;
	//游戏结束背景
	public FadeCtrl GOFadeCtrl;
	//结束画面中的内容
	public FadeCtrl GOHScore_image;
	public FadeCtrl GOHScore_text;
	public FadeCtrl GOHHScore_image;
	public FadeCtrl GOHHScore_text;
	// public FadeCtrl GOHHome_image;
	public FadeCtrl GOHHome_button;
	public FadeCtrl GOHHome_ripples1;
	public FadeCtrl GOHHome_ripples2;

	public GameStartController[] gscs;
	public AudioSource[] BGM = new AudioSource[3];//替换BGM
	private static int BGMnum = 0;
	//减少的分数
	private int reduceScore;
	void Start () {
		gscs = FindObjectsOfType<GameStartController>();
		level = 1;
		remainTime = 0f;
		remainTimeOfDress = 0f;
		theCircle = FindObjectOfType<CircleController>();
		score = 0;
		reduceScore = 0;
		//读取最高分
		if(PlayerPrefs.HasKey("HighestScore"))
		{
			highestScore = PlayerPrefs.GetInt("HighestScore");
			textHighestScore.text = highestScore.ToString();
		}
		BGM[BGMnum].Play();
		//判断是否需要提示
		if(!PlayerPrefs.HasKey("NeedHint"))
		{
			PlayerPrefs.SetInt("NeedHint",0);
		}
		// StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameState == GameState.Start || gameState == GameState.Starting2)
		{
			//每隔固定时间生成圆圈
			remainTime -= Time.deltaTime;
			if(remainTime <= 0f)
			{
				Instantiate(theDot[UnityEngine.Random.Range(0,2)], new Vector3(UnityEngine.Random.Range(-2f,2f), 6, -2), Quaternion.identity);
				remainTime = timeForCreatDot;
			}
			//计算分数
			if(gameState == GameState.Starting2)
			{
				score = 0;
			}else
			{
				score = (int)(Time.time - startTime) - reduceScore;
			}
			textScore.text = score.ToString();
		}
		//每隔固定时间生成随机装饰物
		remainTimeOfDress -= Time.deltaTime;
		if(remainTimeOfDress <= 0f)
		{
			GameObject dress = Instantiate(dressOfBG[UnityEngine.Random.Range(0,dressOfBG.Length)], new Vector3(UnityEngine.Random.Range(-3,3), 8, -1.1f), Quaternion.identity);
			remainTimeOfDress = timeForCreatDress;
		}
	}

	public int AddScore(int scoreToAdd){
		score += scoreToAdd;
		return score;
	}

	public int ReduceScore(int scoreToReduce){
		if(score <= 0)
		{
			return score;
		}
		reduceScore += scoreToReduce;
		return score;
	}

	public int GetCurScore(){
		return score;
	}

	public int GetHighestScore(){
		return highestScore;
	}

	public void ResetGame(){
		//切换BGM
		if(BGMnum < 2)
		{
			BGMnum += 1;
		}else if(BGMnum == 2){
			BGMnum = 0;
		}
		BGM[BGMnum].Play();
		SceneManager.LoadScene("Level_1");
	}

	public void GameOver(){
		//原计分表消失
		textScore.text = "";
		//结束画面显现
		Invoke("GameOverUI",3f);
	}

	private void GameOverUI(){
		//分数部分
		GOHScore_text.gameObject.GetComponent<Text>().text = score.ToString();
		GOHHScore_text.gameObject.GetComponent<Text>().text = highestScore.ToString();

		//图片部分
		GOFadeCtrl.FadeIn();
		GOHScore_image.FadeIn();
		GOHScore_text.FadeIn();
		GOHHScore_image.FadeIn();
		GOHHScore_text.FadeIn();
		// GOHHome_image.FadeIn();
		GOHHome_button.FadeIn();
		GOHHome_ripples1.FadeIn();
		GOHHome_ripples2.FadeIn();
	}

	// private void FadeSth(FadeCtrl theFd){
	// 	if(theFd != null)
	// 		theFd.FadeIn();
	// }

	public void StartGame(){
		gameState = GameState.Starting1;
		foreach(GameStartController gsc in gscs )
		{
			gsc.StartGame();
		}
		Invoke("GameStarting2",timeForStarting2);
	}

	public void GameStarting2(){
		gameState = GameState.Starting2;
		Invoke("GameBegin",timeForStart);
	}
	public void GameBegin(){
		gameState = GameState.Start;
		theCircle.theSprReder.enabled = true;
		startTime = Time.time;
	}

	public void ReplaceCircle(CircleController theCirCtrl){
		theCircle = theCirCtrl;
	}
}
