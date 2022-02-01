using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance;

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private TrailRenderer trailRenderer;

	[Header("Movement Identification")]
	[SerializeField] public bool rage;

	private Vector3 mousePos;

	[Header("Movement Stats")]
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private float rotateSpeed = 200f;
	[SerializeField] private float moveSpeedRage = 15f;
	[SerializeField] private float rotateSpeedRage = 225f;

	[Header("Shoot Stats")]
	[SerializeField] private GameObject bulletToFire1;
	[SerializeField] private GameObject bulletToFire2;
	[SerializeField] private GameObject bulletToFire3;
	[SerializeField] private float timeBetweenShots = 0.4f;
	private float shotCounter;

	[Header("Color Stats")]
	public Color normalColor;
	public Color rageColor;
	[SerializeField] public float fadeSpeed = 1f;

	[Header("Particles")]
	public GameObject getHit;

	#region Unity Functions

	private void Awake()
	{
		Instance = this;

		rb = GetComponent<Rigidbody2D>();
		sr = GetComponentInChildren<SpriteRenderer>();
		trailRenderer = sr.GetComponentInChildren<TrailRenderer>();
	}

	private void Update()
	{
		//Bounds
		transform.position = new Vector2(Mathf.Clamp(transform.position.x, -35f, 35f), Mathf.Clamp(transform.position.y, -19.5f, 18f));

		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		CheckRageColor();
	}

	private void FixedUpdate()
	{
		if (rage)
			RageMovement();
		else
			MouseMovement();
	}

	#endregion

	#region MouseMovement

	private void MouseMovement()
	{
		Vector2 direction = transform.position - mousePos;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = rotateAmount * rotateSpeed;
		rb.velocity = transform.up * moveSpeed;
	}

	#endregion

	#region Rage

	private void RageMovement()
	{
		Vector2 direction = transform.position - mousePos;
		direction.Normalize();

		float rotateAmount = Vector3.Cross(direction, transform.up).z;

		rb.angularVelocity = rotateAmount * rotateSpeedRage;
		rb.velocity = transform.up * moveSpeedRage;
	}

	private void RageShoot()
	{
		if(shotCounter > 0)
		{
			shotCounter -= Time.deltaTime;
		}
		else if (shotCounter <= 0)
		{
			Instantiate(bulletToFire1, transform.position, transform.rotation);
			Instantiate(bulletToFire2, transform.position, transform.rotation);
			Instantiate(bulletToFire3, transform.position, transform.rotation);

			shotCounter = timeBetweenShots;
		}
	}

	#endregion

	#region ChangeColor

	public void CheckRageColor()
	{
		if (rage)
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 1f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 0f);
			sr.color = new Color(Mathf.MoveTowards(sr.color.r, rageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, rageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, rageColor.b, fadeSpeed * Time.deltaTime), 1f);
		} 
		else
		{
			trailRenderer.startColor = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 1f);
			trailRenderer.endColor = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 0f);
			sr.color = new Color(Mathf.MoveTowards(sr.color.r, normalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.g, normalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(sr.color.b, normalColor.b, fadeSpeed * Time.deltaTime), 1f);

		}
	}

	#endregion

	public void SetRage()
	{
		if (!rage)
		{
			AudioManager.instance.PlaySFX(4);
			rage = true;
			CinemachineShake.Instance.ShakeCamera(5f, 1f);
			PlayerHealth.Instance.Heal(10f);
		}
		else
		{
			rage = false;
		}
	}
}
