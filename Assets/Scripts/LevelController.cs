using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
	public static LevelController Instance;

	[SerializeField] private EnemySpawner spawner;

	public float spawnTimer = 2f;

	[SerializeField] private Image progressBar;

	[Header("Progress Values")]
	public float progress;
	public float maxProgress = 100f;
	private float lerpSpeed;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		progress = 0;

		StartCoroutine(StartLevel());
	}

	private void Update()
	{
		lerpSpeed = 3f * Time.deltaTime;

		ProgressBarFiller();
		ColorChanger();
	}

	private IEnumerator StartLevel()
	{
		spawner.SpawnRandomEnemy();
		yield return new WaitForSeconds(spawnTimer);
		StartCoroutine(StartLevel());
	}

	public void UpdateProgress(int points)
	{
		progress += points;

		if(progress == maxProgress)
		{
			Player.Instance.SetRage();
			progress = 0;
		}
	} 

	private void ProgressBarFiller()
	{
		progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, progress / maxProgress, lerpSpeed);
	}

	private void ColorChanger()
	{
		if (!Player.Instance.rage)
		{
			Color progressColor = Color.Lerp(EnemySpawner.Instance.blue, EnemySpawner.Instance.red, progress / maxProgress);
			progressBar.color = progressColor;
		}
		else
		{
			Color progressColor = Color.Lerp(EnemySpawner.Instance.red, EnemySpawner.Instance.blue, progress / maxProgress);
			progressBar.color = progressColor;
		}
	}
}
