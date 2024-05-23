using System;
using UnityEngine;

public class CaballoTroya : MonoBehaviour
{
	public float speed = 2.5f;

	public float cycleTime = 30f;

	public Action<CaballoTroya> killed;

	public Vector3 leftDestination { get; private set; }

	public Vector3 rightDestination { get; private set; }

	public int direction { get; private set; } = -1;


	public bool spawned { get; private set; }

	private void Start()
	{
		Vector3 vector = Camera.main.ViewportToWorldPoint(Vector3.zero);
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(Vector3.right);
		Vector3 position = base.transform.position;
		position.x = vector.x - 1f;
		leftDestination = position;
		Vector3 position2 = base.transform.position;
		position2.x = vector2.x + 1f;
		rightDestination = position2;
		base.transform.position = leftDestination;
		Despawn();
	}

	private void Update()
	{
		if (spawned)
		{
			if (direction == 1)
			{
				MoveRight();
			}
			else
			{
				MoveLeft();
			}
		}
	}

	private void MoveRight()
	{
		base.transform.position += Vector3.right * speed * Time.deltaTime;
		if (base.transform.position.x >= rightDestination.x)
		{
			Despawn();
		}
	}

	private void MoveLeft()
	{
		base.transform.position += Vector3.left * speed * Time.deltaTime;
		if (base.transform.position.x <= leftDestination.x)
		{
			Despawn();
		}
	}

	private void Spawn()
	{
		direction *= -1;
		if (direction == 1)
		{
			base.transform.position = leftDestination;
		}
		else
		{
			base.transform.position = rightDestination;
		}
		spawned = true;
	}

	private void Despawn()
	{
		spawned = false;
		if (direction == 1)
		{
			base.transform.position = rightDestination;
		}
		else
		{
			base.transform.position = leftDestination;
		}
		Invoke("Spawn", cycleTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
		{
			Despawn();
			if (killed != null)
			{
				killed(this);
			}
		}
	}
}
