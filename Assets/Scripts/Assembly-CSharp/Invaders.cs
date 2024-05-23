using System;
using UnityEngine;

public class Invaders : MonoBehaviour
{
	public int rows = 5;

	public int columns = 11;

	public Invader[] prefabs;

	private Vector3 direccion = Vector2.right;

	public AnimationCurve speed;

	public GameObject victoria;

	public Proyecti cobiPrefab;

	public float AtaqueCovi = 1f;

	public int ContadorMuertes { get; private set; }

	public int ContadorVivos => totalInvaders - ContadorMuertes;

	public int totalInvaders => rows * columns;

	public float porcentaje => (float)ContadorMuertes / (float)totalInvaders;

	private void Awake()
	{
		for (int i = 0; i < rows; i++)
		{
			float num = 0.8f * (float)(columns - 1);
			float num2 = 0.8f * (float)(rows - 1);
			Vector2 vector = new Vector2((0f - num) / 2f, (0f - num2) / 2f);
			Vector3 vector2 = new Vector3(vector.x, vector.y + (float)i * 0.8f, 0f);
			for (int j = 0; j < columns; j++)
			{
				Invader invader = UnityEngine.Object.Instantiate(prefabs[i], base.transform);
				invader.muerte = (Action)Delegate.Combine(invader.muerte, new Action(muerteInvader));
				Vector3 localPosition = vector2;
				localPosition.x += (float)j * 0.8f;
				invader.transform.localPosition = localPosition;
			}
		}
	}

	private void Start()
	{
		InvokeRepeating("CobiAttack", AtaqueCovi, AtaqueCovi);
	}

	private void Update()
	{
		base.transform.position += direccion * speed.Evaluate(porcentaje) * Time.deltaTime;
		Vector3 vector = Camera.main.ViewportToWorldPoint(Vector3.zero);
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(Vector3.right);
		foreach (Transform item in base.transform)
		{
			if (item.gameObject.activeInHierarchy)
			{
				if (direccion == Vector3.right && item.position.x >= vector2.x - 0.5f)
				{
					ChangeMove();
				}
				else if (direccion == Vector3.left && item.position.x <= vector.x + 0.5f)
				{
					ChangeMove();
				}
			}
		}
	}

	private void ChangeMove()
	{
		direccion *= -1f;
		Vector3 position = base.transform.position;
		position.y -= 0.8f;
		base.transform.position = position;
	}

	private void muerteInvader()
	{
		ContadorMuertes++;
		if (ContadorMuertes >= totalInvaders)
		{
			Debug.Log("Ganate Felicidades");
			victoria.SetActive(value: true);
		}
	}

	private void CobiAttack()
	{
		foreach (Transform item in base.transform)
		{
			if (item.gameObject.activeInHierarchy && UnityEngine.Random.value < 1f / (float)ContadorVivos)
			{
				UnityEngine.Object.Instantiate(cobiPrefab, item.position, Quaternion.identity);
				break;
			}
		}
	}
}
