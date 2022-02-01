using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;

	public AudioSource levelMusic, gameOverMusic;

	public AudioSource[] sfx;

	private void Awake()
	{
		instance = this;
	}

	public void PlayGameOver()
	{
		levelMusic.Stop();

		gameOverMusic.Play();
	}

	public void PlaySFX(int sfxToPlay)
	{
		sfx[sfxToPlay].Stop();
		sfx[sfxToPlay].Play();
	}
}
