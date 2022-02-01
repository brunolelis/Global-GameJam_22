using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner Instance;

	[SerializeField] private Transform[] spawnPoints;
	[SerializeField] private GameObject[] enemyPrefab;
	[SerializeField] private Light2D[] lights;

	public Color blue;
	public Color red;

	private void Awake()
	{
		Instance = this;
	}

	private void Update()
	{
		if (!Player.Instance.rage)
		{
			for (int i = 0; i < lights.Length; i++)
			{
				lights[i].color = Color.Lerp(red, blue, 1 * Time.deltaTime);
			}
		}
		else
		{
			for (int i = 0; i < lights.Length; i++)
			{
				lights[i].color = Color.Lerp(blue, red, 1 * Time.deltaTime);
			}
		}
	}

	public void SpawnRandomEnemy()
	{
		int randEnemy = Random.Range(0, enemyPrefab.Length);
		int randSpawnPoint = Random.Range(0, spawnPoints.Length);

		Instantiate(enemyPrefab[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
	}
}
