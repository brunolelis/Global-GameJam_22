using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private Transform player;
	private SpriteRenderer sr;
	private TrailRenderer trailRenderer;

	[Header("Movement Stats")]
	[SerializeField] private int speed = 10;
	[SerializeField] private int rotateSpeed = 200;
	[SerializeField] private float speedSlow = 5;

	[Header("Other Stats")]
	[SerializeField] private int damageAmount = 20;
	[SerializeField] private int scoreAmount = 5;

	[Header("Color Stats")]
	public Color normalColor;
	public Color rageColor;
	[SerializeField] public float fadeSpeed = 1f;

	[Header("Particles")]
	[SerializeField] private GameObject enemyBlueDie;
	[SerializeField] private GameObject enemyImpact;

	private Rigidbody2D rb;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponentInChildren<SpriteRenderer>();
		trailRenderer = sr.GetComponentInChildren<TrailRenderer>();
	}

	private void Start()
	{
		speed = Random.Range(speed - 5, speed + 3);
		rotateSpeed = Random.Range(rotateSpeed - 50, rotateSpeed + 50);
	}

	private void Update()
	{
		//Bounds
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -35f, 35f), Mathf.Clamp(transform.position.y, -19.5f, 18f));

		CheckEnemyRageColor();
	}

	private void FixedUpdate()
	{
		if (!Player.Instance.rage)
			ChasePlayer();
		else
			RunAwayFromPlayer();
	}

	public void RunAwayFromPlayer()
	{
		Vector2 direction = (Vector2)player.position + rb.position;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speedSlow;
	}

	private void ChasePlayer()
	{
		Vector2 direction = (Vector2)player.position - rb.position;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;
		rb.velocity = transform.up * speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
			AudioManager.instance.PlaySFX(0);
			Score_LevelDisplay.Instance.AddLevelScore(scoreAmount);
			Instantiate(enemyImpact, transform.position, transform.rotation);
			Destroy(gameObject);
		}

		if (collision.gameObject.CompareTag("Player"))
		{
			if (!Player.Instance.rage)
			{
				Instantiate(Player.Instance.getHit, Player.Instance.transform.position, Player.Instance.transform.rotation);
				Instantiate(enemyImpact, transform.position, transform.rotation);

				CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
				AudioManager.instance.PlaySFX(2);
				PlayerHealth.Instance.Damage(damageAmount);
			}
			else
			{
				AudioManager.instance.PlaySFX(0);
				CinemachineShake.Instance.ShakeCamera(5f, 0.1f);
				Instantiate(enemyBlueDie, transform.position, transform.rotation);
			}

			Score_LevelDisplay.Instance.AddLevelScore(scoreAmount);

			Destroy(gameObject);
		}
	}

	public void CheckEnemyRageColor()
	{
		if (!Player.Instance.rage)
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 1f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 0f);
			sr.color = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 1f);
		}
		else
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 0f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 0f);
			sr.color = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 1f);
		}
	}
}
