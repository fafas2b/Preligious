using UnityEngine;

public class dectectarColicion : MonoBehaviour
{
	private GameObject obj;

	public GameObject vaso1;

	public GameObject vaso2;

	public GameObject vaso3;

	public GameObject tapa1;

	public GameObject tapa2;

	public GameObject tapa3;

	public GameObject derrota;

	private Renderer _renderer;

	public Color color1;

	public Color color2;

	public Color color3;

	public GameObject Objeto1;

	public GameObject Objeto2;

	public GameObject Objeto3;

	private ChecarTarea checarTarea;

	private void Start()
	{
		_renderer = GetComponent<Renderer>();
		checarTarea = base.transform.root.gameObject.GetComponent<ChecarTarea>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("vaso1"))
		{
			obj = GameObject.FindWithTag("vaso1");
			vaso1.SetActive(value: true);
			vaso2.SetActive(value: false);
			vaso3.SetActive(value: false);
		}
		if (collision.CompareTag("vaso2"))
		{
			obj = GameObject.FindWithTag("vaso2");
			vaso1.SetActive(value: false);
			vaso2.SetActive(value: true);
			vaso3.SetActive(value: false);
		}
		if (collision.CompareTag("vaso3"))
		{
			obj = GameObject.FindWithTag("vaso3");
			vaso1.SetActive(value: false);
			vaso2.SetActive(value: false);
			vaso3.SetActive(value: true);
		}
		if (collision.CompareTag("tapa1"))
		{
			obj = GameObject.FindWithTag("tapa1");
			tapa1.SetActive(value: true);
			tapa2.SetActive(value: false);
			tapa3.SetActive(value: false);
			obj = null;
		}
		if (collision.CompareTag("tapa2"))
		{
			obj = GameObject.FindWithTag("tapa2");
			tapa1.SetActive(value: false);
			tapa2.SetActive(value: true);
			tapa3.SetActive(value: false);
			obj = null;
		}
		if (collision.CompareTag("tapa3"))
		{
			obj = GameObject.FindWithTag("tapa3");
			tapa1.SetActive(value: false);
			tapa2.SetActive(value: false);
			tapa3.SetActive(value: true);
		}
		if (collision.CompareTag("refresco"))
		{
			obj = GameObject.FindWithTag("refresco");
			_renderer = obj.GetComponent<Renderer>();
			if (vaso1.activeSelf)
			{
				if (_renderer.material.color == color1)
				{
					vaso1.GetComponent<Renderer>().material.color = color1;
				}
				if (_renderer.material.color == color2)
				{
					vaso1.GetComponent<Renderer>().material.color = color2;
				}
				if (_renderer.material.color == color3)
				{
					vaso1.GetComponent<Renderer>().material.color = color3;
				}
			}
			if (vaso2.activeSelf)
			{
				if (_renderer.material.color == color1)
				{
					vaso2.GetComponent<Renderer>().material.color = color1;
				}
				if (_renderer.material.color == color2)
				{
					vaso2.GetComponent<Renderer>().material.color = color2;
				}
				if (_renderer.material.color == color3)
				{
					vaso2.GetComponent<Renderer>().material.color = color3;
				}
			}
			if (vaso3.activeSelf)
			{
				if (_renderer.material.color == color1)
				{
					vaso3.GetComponent<Renderer>().material.color = color1;
				}
				if (_renderer.material.color == color2)
				{
					vaso3.GetComponent<Renderer>().material.color = color2;
				}
				if (_renderer.material.color == color3)
				{
					vaso3.GetComponent<Renderer>().material.color = color3;
				}
			}
		}
		if (collision.CompareTag("checar"))
		{
			if (Objeto1.activeSelf && vaso1.activeSelf && vaso1.GetComponent<Renderer>().material.color == color2 && tapa3.activeSelf)
			{
				Debug.Log("si");
				checarTarea.cont++;
				checarTarea.TareaCompletada();
			}
			else if (Objeto2.activeSelf && vaso2.activeSelf && vaso2.GetComponent<Renderer>().material.color == color3 && tapa2.activeSelf)
			{
				Debug.Log("si");
				checarTarea.cont++;
				checarTarea.TareaCompletada();
			}
			else if (Objeto3.activeSelf && vaso3.activeSelf && vaso3.GetComponent<Renderer>().material.color == color1 && tapa1.activeSelf)
			{
				Debug.Log("si");
				checarTarea.cont++;
				checarTarea.TareaCompletada();
			}
			else
			{
				derrota.SetActive(value: true);
			}
		}
	}
}
