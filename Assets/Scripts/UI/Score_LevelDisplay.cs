using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_LevelDisplay : MonoBehaviour
{
	public static Score_LevelDisplay Instance;

    [SerializeField] private TextMeshProUGUI levelScoreDisplay;

	public int scoreLevel = 0;

	[Header("Level Per Score Values")]
	[SerializeField] private int firstRoundValue;
	[SerializeField] private int secondRoundValue;
	[SerializeField] private int thirdRoundValue;
	[SerializeField] private int fourthRoundValue;
	[SerializeField] private int fifthRoundValue;
	[SerializeField] private int sixtyRoundValue;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		CheckProgression();
		ColorChanger();
		levelScoreDisplay.text = scoreLevel.ToString();

		if (scoreLevel > PlayerPrefs.GetInt("HighScore", 0))
		{
			PlayerPrefs.SetInt("HighScore", scoreLevel);
		}
	}

	public void AddLevelScore(int score)
	{
		scoreLevel += score;
		LevelController.Instance.UpdateProgress(score);
	}

	private void ColorChanger()
	{
		if (!Player.Instance.rage)
		{
			levelScoreDisplay.color = EnemySpawner.Instance.blue;
		}
		else
		{
			levelScoreDisplay.color = EnemySpawner.Instance.red;
		}
	}

	private void CheckProgression()
	{
		if(scoreLevel < firstRoundValue)
		{
			LevelController.Instance.maxProgress = 5;
			LevelController.Instance.spawnTimer = 2.2f;
			Debug.Log("1");
			return;
		}
		else if (scoreLevel < secondRoundValue)
		{
			LevelController.Instance.maxProgress = 15;
			LevelController.Instance.spawnTimer = 1.75f;
			Debug.Log("2");
			return;
		}
		else if (scoreLevel < thirdRoundValue)
		{
			LevelController.Instance.maxProgress = 20;
			LevelController.Instance.spawnTimer = 1f;
			Debug.Log("3");
			return;
		}
		else if (scoreLevel < fourthRoundValue)
		{
			LevelController.Instance.maxProgress = 40;
			LevelController.Instance.spawnTimer = 0.8f;
			Debug.Log("4");
			return;
		}
		else if (scoreLevel < fifthRoundValue)
		{
			LevelController.Instance.maxProgress = 80;
			LevelController.Instance.spawnTimer = 0.4f;
			Debug.Log("5");
			return;
		}
		else if (scoreLevel < sixtyRoundValue)
		{
			LevelController.Instance.maxProgress = 100;
			LevelController.Instance.spawnTimer = 0.15f;
			Debug.Log("6");
			return;
		}
	}
}
