using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore_MenuDisplay : MonoBehaviour
{
	public static HighScore_MenuDisplay Instance;

	public TextMeshProUGUI menuHighScoreDisplay;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		menuHighScoreDisplay.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}

	public void ResetGame()
	{
		PlayerPrefs.DeleteAll();
		menuHighScoreDisplay.text = "0";
	}
}
