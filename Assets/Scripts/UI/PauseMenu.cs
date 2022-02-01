using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	public static PauseMenu Instance;

    public GameObject pauseMenu;
	public GameObject death;

	public bool isPaused;
	public bool isDead;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (isDead)
			Cursor.visible = true;

		if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
		{
			PauseUnpause();
		}
	}

	public void PauseUnpause()
	{
		AudioManager.instance.PlaySFX(3);
		if (!isPaused)
		{
			pauseMenu.SetActive(true);

			isPaused = true;

			Time.timeScale = 0f;
		}
		else
		{
			pauseMenu.SetActive(false);

			isPaused = false;

			Time.timeScale = 1f;
		}
	}

	public void Death()
	{
		AudioManager.instance.PlaySFX(1);
		isDead = true;
		death.SetActive(true);
		Time.timeScale = 0f;
	}
}
