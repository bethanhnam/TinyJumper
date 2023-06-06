using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
	public Text bestScoreText;
	public bool _replayButtonClicked;

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	public override void Show(bool isShow)
	{
		base.Show(isShow);
		if (bestScoreText)
		{
			bestScoreText.text = Prefabs.bestScore.ToString();
		}
	}
	public void Replay()
	{
		_replayButtonClicked = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}
	public void BackToHome()
	{
		GameGUIManager.Ins.ShowGameGUI(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	void OnSceneLoaded(Scene scene,LoadSceneMode mode)
	{
		if (_replayButtonClicked)
		{
			GameGUIManager.Ins.ShowGameGUI(true);
			GameManager.Ins.PlayGame();
		}
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
