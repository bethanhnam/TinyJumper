using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public Player playerPrefab;
	public Platform platformPrefab;
	public float minSpawnX;
	public float minSpawnY;
	public float maxSpawnX;
	public float maxSpawnY;
	public CamController cam;
	public float powerBarUp;
	Player _player;
	int score;
	bool _isGameStarted;

	public bool IsGameStarted { get => _isGameStarted; set => _isGameStarted = value; }

	public override void Awake()
	{
		MakeSingleton(false);
	}
	public override void Start()
	{
		base.Start();
		GameGUIManager.Ins.UpdateScoreCouting(score);
		
		GameGUIManager.Ins.UpdatePowerBar(0, 1);
		AudioController.Ins.PlayBackgroundMusic();
	}

	public void PlayGame()
	{
		StartCoroutine(PlatformInit());
		GameGUIManager.Ins.ShowGameGUI(true);

	}
	IEnumerator PlatformInit()
	{
		Platform platformClone = null;
		if (platformPrefab != null)
		{
			platformClone = Instantiate(platformPrefab, new Vector2(0, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
			platformClone.id = platformClone.gameObject.GetInstanceID();
		}
		yield return new WaitForSeconds(0.5f);
		if (playerPrefab)
		{
			_player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			_player.lastPlatformId = platformClone.id;
		}
		if (platformPrefab)
		{
			float spawnX = _player.transform.position.x + minSpawnX;
			float spawnY = Random.Range(minSpawnY, maxSpawnY);

			Platform platformClone02 = Instantiate(platformPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
			platformClone02.id = platformClone02.gameObject.GetInstanceID();
		}
		yield return new WaitForSeconds(0.5f);
		_isGameStarted = true;
	}
	public void CreatePlatform()
	{
		if (!platformPrefab || !_player) return;

		float spawnX = Random.Range(_player.transform.position.x + minSpawnX, _player.transform.position.x + maxSpawnX);
		float spawmY = Random.Range(minSpawnY, maxSpawnY);
		Platform platformClone = Instantiate(platformPrefab, new Vector2(spawnX, spawmY), Quaternion.identity);
		platformClone.id = platformClone.gameObject.GetInstanceID();
	}
	public void CreatePlatformAndLerp(float playerXPos)
	{
		if (cam)
		{
			cam.LerpTrigger(playerXPos + minSpawnX);
		}
		CreatePlatform();
	}
	public void AddScore()
	{
		score++;
		Prefabs.bestScore = score;
		GameGUIManager.Ins.UpdateScoreCouting(score);
		AudioController.Ins.PlaySound(AudioController.Ins.getScore);
	}
}
