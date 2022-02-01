using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance;

	[SerializeField] private Image fadeScreen;
	[SerializeField] private float fadeSpeed = 1f;
	private bool fadeToBlack, fadeOutBlack;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		fadeOutBlack = true;
		fadeToBlack = false;
	}

	private void Update()
	{
		if (fadeOutBlack)
		{
			fadeScreen.gameObject.SetActive(true);

			fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed *  Time.deltaTime));
			if(fadeScreen.color.a == 0f)
			{
				fadeOutBlack = false;
				fadeScreen.gameObject.SetActive(false);
			}
		}

		if (fadeToBlack)
		{
			fadeScreen.gameObject.SetActive(true);

			fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
			if (fadeScreen.color.a == 1f)
			{
				fadeToBlack = false;
				fadeScreen.gameObject.SetActive(false);
			}
		}
	}

	public void GoToMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
		StartFadeToBlack();
	}

	public void GoToLevel()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene(1);
		StartFadeToBlack();
	}

	public void Quit()
	{
		Application.Quit();
	}

	private void StartFadeToBlack()
	{
		fadeToBlack = true;
		fadeOutBlack = false;
	}
}
