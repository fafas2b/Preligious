using System;
using UnityEngine;

public class Proyecti : MonoBehaviour
{
	public Vector3 direccion;

	public float speed;

	public Action destroyed;

	private void Update()
	{
		base.transform.position += direccion * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (destroyed != null)
		{
			destroyed();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}
}
