using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
	[Header("Rotation Settings")]
	[Range(1f, 500f)]
	[SerializeField] private float speed = 250f;

	private TrailRenderer trailRenderer;

	private Vector2 targetPos;

	[Header("Color Stats")]
	public Color normalColor;
	public Color rageColor;
	[SerializeField] public float fadeSpeed = 1f;


	private void Awake()
	{
		trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void Start()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		CheckEnemyRageColor();

		if (PauseMenu.Instance.isPaused || PauseMenu.Instance.isDead)
		{
			Cursor.visible = true;
		}
		else if(!PauseMenu.Instance.isDead)
		{
			Cursor.visible = false;

			targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = targetPos;

			transform.Rotate(0, 0, speed * Time.deltaTime);
		}
	}

	private void CheckPauseActive()
	{
		if (PauseMenu.Instance.isPaused)
			Cursor.visible = false;
		else
			Cursor.visible = true;
	}

	private void CheckEnemyRageColor()
	{
		if (Player.Instance.rage)
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(rageColor.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(rageColor.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(rageColor.b, normalColor.b, fadeSpeed * Time.deltaTime), 1f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(rageColor.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(rageColor.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(rageColor.b, normalColor.b, fadeSpeed * Time.deltaTime), 0f);
		}
		else
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(normalColor.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(normalColor.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(normalColor.b, rageColor.b, fadeSpeed * Time.deltaTime), 1f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(normalColor.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(normalColor.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(normalColor.b, rageColor.b, fadeSpeed * Time.deltaTime), 0f);
		}
	}
}
