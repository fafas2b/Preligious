using UnityEngine;

public class estrellas : MonoBehaviour
{
	public Sprite[] animationSprite;

	public float animationTime = 1f;

	private SpriteRenderer _spriteRenderer;

	private int _animationFrame;

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
}
