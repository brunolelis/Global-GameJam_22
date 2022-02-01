using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
	[SerializeField] private float speed = 7.5f;

	private Rigidbody2D rb;
	[SerializeField] private GameObject impactEffect;

	[Header("Bullet Direction")]
	[SerializeField] private bool right;
	[SerializeField] private bool left;
	[SerializeField] private bool up;
	[SerializeField] private bool down;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		BulletMovement();
	}

	private void BulletMovement()
	{
		if(right)
			rb.velocity = transform.up * speed;
		if(left)
			rb.velocity = transform.up * -1 * speed;
		if(up)
			rb.velocity = transform.right * - 1 * speed;
		if(down)
			rb.velocity = transform.right * speed;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			Instantiate(impactEffect, transform.position, transform.rotation);
			Destroy(gameObject);
			Destroy(collision.gameObject);
		}
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
