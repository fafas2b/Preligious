using System;
using UnityEngine;

public class Invader : MonoBehaviour
{
	public Sprite[] animationSprite;

	public float animationTime = 1f;

	private SpriteRenderer _spriteRenderer;

	private int _animationFrame;

	public Action muerte;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		InvokeRepeating("AnimatedSprite", animationTime, animationTime);
	}

	private void AnimatedSprite()
	{
		_animationFrame++;
		if (_animationFrame >= animationSprite.Length)
		{
			_animationFrame = 0;
		}
		_spriteRenderer.sprite = animationSprite[_animationFrame];
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Bala"))
		{
			muerte();
			base.gameObject.SetActive(value: false);
		}
	}
}
