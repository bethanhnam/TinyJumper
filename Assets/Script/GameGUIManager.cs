using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGUIManager : Singleton<GameGUIManager>
{
	public GameObject homeGui;
	public GameObject gameGui;
	public Text scoreCoutingText;
	public Image powerBarSlider;

	public Dialog achivementDialog;
	public Dialog helpDialog;
	public Dialog gameoverDialog;
	public override void Awake()
	{
		MakeSingleton(false);
	}
	public void ShowGameGUI(bool isShow)
	{
		if (isShow)
		{
			gameGui.SetActive(isShow);
		}
		if (homeGui)
		{
			homeGui.SetActive(!isShow);
		}
	}
	public void UpdateScoreCouting(int score)
	{
		if (scoreCoutingText)
		{
			scoreCoutingText.text = score.ToString();
		}
	}
	public void UpdatePowerBar(float curVal,float totalVal)
	{
		if(powerBarSlider)
		{
			powerBarSlider.fillAmount = curVal / totalVal;
		}
	}
	public void ShowAchievementDialog()
	{
		if (achivementDialog)
		{
			achivementDialog.Show(true);
		}
	}
	public void ShowHelpDialog()
	{
		if (helpDialog)
		{
			helpDialog.Show(true);
		}
	}
	public void ShowOverDialog()
	{
		if (gameoverDialog)
		{
			gameoverDialog.Show(true);
		}
	}
}
