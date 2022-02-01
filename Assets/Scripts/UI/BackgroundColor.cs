using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer walpaperBackGround;
	[SerializeField] private Color walpaperNormalColor;
	[SerializeField] private Color walpaperRageColor;


	[SerializeField] private float fadeSpeed = 1f;

	private void Update()
	{
		CheckWalpaperColor();
	}

	private void CheckWalpaperColor()
	{
		if (!Player.Instance.rage)
			walpaperBackGround.color = new Color(Mathf.MoveTowards(walpaperNormalColor.r, walpaperRageColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(walpaperNormalColor.g, walpaperRageColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(walpaperNormalColor.b, walpaperRageColor.b, fadeSpeed * Time.deltaTime), 1f);
		else
			walpaperBackGround.color = new Color(Mathf.MoveTowards(walpaperRageColor.r, walpaperNormalColor.r, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(walpaperRageColor.g, walpaperNormalColor.g, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(walpaperRageColor.b, walpaperNormalColor.b, fadeSpeed * Time.deltaTime), 1f);
	}
}
