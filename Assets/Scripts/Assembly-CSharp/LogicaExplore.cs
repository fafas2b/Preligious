using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaExplore : MonoBehaviour
{
	public bool jugadorCerca;

	public bool aceptarMision;

	public bool done;

	public RawImage image;

	public Texture texturemapa;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject panelMapa;

	public GameObject botonSi;

	public GameObject botonNo;

	public GameObject carteles;

	public GameObject botonCerrar;

	public GameObject icono;

	public GameObject panelDone;

	public GameObject panelMapa1;

	public GameObject textos;

	public GameObject[] misiones;

	public TextMeshProUGUI textoMision;

	public int modulos;

	public string[] nombres;

	public void OnEnable()
	{
		if (done)
		{
			icono.SetActive(value: false);
		}
	}

	private void Start()
	{
		Debug.Log(done);
		misiones = GameObject.FindGameObjectsWithTag("mision");
		modulos = 12;
		textoMision.text = "Modulos por visitar: " + modulos;
		Debug.Log(done);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !aceptarMision && jugadorCerca)
		{
			if (done)
			{
				panelMapa.SetActive(value: true);
				botonCerrar.SetActive(value: true);
				panelInteraccion.SetActive(value: false);
			}
			else
			{
				panelDialogo.SetActive(value: true);
				botonNo.SetActive(value: true);
				botonSi.SetActive(value: true);
				panelInteraccion.SetActive(value: false);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			jugadorCerca = true;
			if (!aceptarMision)
			{
				panelInteraccion.SetActive(value: true);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			jugadorCerca = false;
			panelInteraccion.SetActive(value: false);
		}
	}

	public void Si()
	{
		aceptarMision = true;
		jugadorCerca = false;
		botonNo.SetActive(value: false);
		botonSi.SetActive(value: false);
		panelDialogo.SetActive(value: false);
		panelInteraccion.SetActive(value: false);
		icono.SetActive(value: false);
		carteles.SetActive(value: true);
		panelMapa1.SetActive(value: true);
		image.texture = texturemapa;
		textos.SetActive(value: false);
		for (int i = 0; i < carteles.GetComponentsInChildren<LogicaCartel>().Length; i++)
		{
			carteles.GetComponentsInChildren<LogicaCartel>()[i].enabled = true;
		}
		deny();
		panelMision.SetActive(value: true);
	}

	public void No()
	{
		panelDialogo.SetActive(value: false);
		botonNo.SetActive(value: false);
		botonSi.SetActive(value: false);
		panelInteraccion.SetActive(value: true);
	}

	public IEnumerator esconder()
	{
		for (int i = 0; i < Object.FindObjectsOfType<LogicaCartel>().Length; i++)
		{
			Object.FindObjectsOfType<LogicaCartel>()[i].enabled = false;
		}
		panelMision.SetActive(value: false);
		done = true;
		panelMapa1.SetActive(value: false);
		textos.SetActive(value: true);
		panelDone.SetActive(value: true);
		yield return new WaitForSeconds(5f);
		panelDone.SetActive(value: false);
		Debug.Log("finished");
	}

	private void deny()
	{
		for (int i = 0; i < misiones.Length; i++)
		{
			if (misiones[i].GetComponent<Mision>().numeroMision != 2)
			{
				misiones[i].SetActive(value: false);
				Debug.Log(misiones[i].GetComponent<Mision>().numeroMision);
			}
		}
		for (int j = 0; j < Object.FindObjectsOfType<LCartel>().Length; j++)
		{
			Object.FindObjectsOfType<LCartel>()[j].enabled = false;
		}
		Object.FindObjectOfType<LogicaTaller>().enabled = false;
		Object.FindObjectOfType<LogicaNPC>().enabled = false;
		Object.FindObjectOfType<LogicaAlmacen>().enabled = false;
		Object.FindObjectOfType<LogicaAdministrativo>().enabled = false;
		Object.FindObjectOfType<Activar_Alimentos>().enabled = false;
		Object.FindObjectOfType<Activar_Calidad>().enabled = false;
		Object.FindObjectOfType<Activar_Electronico>().enabled = false;
		Object.FindObjectOfType<Activar_Space>().enabled = false;
	}

	public void activate()
	{
		for (int i = 0; i < misiones.Length; i++)
		{
			misiones[i].SetActive(value: true);
			Debug.Log("Panel Activado");
		}
		for (int j = 0; j < Object.FindObjectsOfType<LCartel>().Length; j++)
		{
			Object.FindObjectsOfType<LCartel>()[j].enabled = true;
		}
		Object.FindObjectOfType<LogicaTaller>().enabled = true;
		Object.FindObjectOfType<LogicaNPC>().enabled = true;
		Object.FindObjectOfType<LogicaAlmacen>().enabled = true;
		Object.FindObjectOfType<LogicaAdministrativo>().enabled = true;
		Object.FindObjectOfType<Activar_Alimentos>().enabled = true;
		Object.FindObjectOfType<Activar_Calidad>().enabled = true;
		Object.FindObjectOfType<Activar_Electronico>().enabled = true;
		Object.FindObjectOfType<Activar_Space>().enabled = true;
	}
}
