using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
	public static PlayerHealth Instance;

	[SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;

    private float health, maxHealth = 100f;
	private float lerpSpeed;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		health = maxHealth;
	}

	private void Update()
	{
		if (health > maxHealth) 
			health = maxHealth;

		healthText.text = health + " %";

		lerpSpeed = 3f * Time.deltaTime;

		HealthBarFiller();
		ColorChanger();
	}

	private void HealthBarFiller()
	{
		healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
	}

	private void ColorChanger()
	{
		Color healthColor = Color.Lerp(Color.red, Color.green, health / maxHealth);
		healthBar.color = healthColor;
	}

	public void Damage(float damagePoints)
	{
		if (health > 0)
		{
			health -= damagePoints;
		}

		if(health <= 0)
		{
			AudioManager.instance.PlayGameOver();
			PauseMenu.Instance.Death();
		}
	}

	public void Heal(float healingPoints)
	{
		if(health < maxHealth)
		{
			health += healingPoints;
		}
	}
}
