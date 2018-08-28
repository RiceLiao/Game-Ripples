using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeCtrl : MonoBehaviour {

	private Image theImage;
	private Text theText;
	private Button theButton;
	//渐变所用时间
	public float timeForFade = 2f;
	// Use this for initialization
	void Start () {
		theImage = GetComponent<Image>();
		theText = GetComponent<Text>();
		theButton = GetComponent<Button>();
	}

	public void FadeIn(){
		if(theImage != null)
		{
			theImage.enabled = true;
			theImage.DOFade(1,timeForFade);
		}
		if(theText != null)
		{
			theText.DOFade(1,timeForFade);
		}
		if(theButton != null)
		{
			theButton.enabled = true;
		}
	}
}
