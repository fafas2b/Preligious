using System;
using UnityEngine;

public class Jugador : MonoBehaviour
{
	private float speed = 4f;

	public Proyecti laser;

	private bool _proyectilActive;

	public GameObject fallar;

	private Vector2 limites;

	private float width;

	private float height;

	private void Start()
	{
		limites = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		width = base.transform.GetComponent<SpriteRenderer>().bounds.size.x / 2f;
		height = base.transform.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			base.transform.position += Vector3.left * speed * Time.deltaTime;
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, limites.x * -1f + width, limites.x - width), Mathf.Clamp(base.transform.position.y, limites.y * -1f + height, limites.y - height), base.transform.position.z);
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			base.transform.position += Vector3.right * speed * Time.deltaTime;
			base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, limites.x * -1f + width, limites.x - width), Mathf.Clamp(base.transform.position.y, limites.y * -1f + height, limites.y - height), base.transform.position.z);
		}
		if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
		{
			disparar();
		}
	}

	private void disparar()
	{
		if (!_proyectilActive)
		{
			Proyecti proyecti = UnityEngine.Object.Instantiate(laser, base.transform.position, Quaternion.identity);
			proyecti.destroyed = (Action)Delegate.Combine(proyecti.destroyed, new Action(balaDrestruida));
			_proyectilActive = true;
		}
	}

	private void balaDrestruida()
	{
		_proyectilActive = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Invader") || other.gameObject.layer == LayerMask.NameToLayer("Covi"))
		{
			Debug.Log("Perdiste");
			UnityEngine.Object.Destroy(base.gameObject);
			fallar.SetActive(value: true);
		}
	}
}
