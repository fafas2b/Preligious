using UnityEngine;

public class caidaObjeto : MonoBehaviour
{
	private Vector2 posicionOriginal;

	public GameObject obj;

	public Animator anim;

	public Animator anim2;

	public Renderer _renderer;

	public Color color1;

	public Color color2;

	public Color color3;

	private void Start()
	{
		posicionOriginal = base.transform.position;
		anim = GetComponent<Animator>();
		anim2 = GetComponent<Animator>();
		_renderer = GetComponent<Renderer>();
	}

	public void caida()
	{
		anim2.SetTrigger("maquina");
		obj.SetActive(value: true);
		anim.SetTrigger("caida");
		base.transform.position = posicionOriginal;
	}

	public void desaparecer()
	{
		obj.SetActive(value: false);
	}

	public void aparecer()
	{
		obj.SetActive(value: true);
	}

	public void changeColor(int i)
	{
		switch (i)
		{
		case 1:
			_renderer.material.color = color1;
			caida();
			break;
		case 2:
			_renderer.material.color = color2;
			caida();
			break;
		case 3:
			_renderer.material.color = color3;
			caida();
			break;
		}
	}
}
