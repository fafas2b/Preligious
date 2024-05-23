using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaTaller : MonoBehaviour
{
	public bool jugadorCerca;

	public bool aceptarMision;

	public bool done;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject botonSi;

	public GameObject botonNo;

	public GameObject botonOk;

	public GameObject botonThanks;

	public GameObject icono;

	public GameObject panelMapa;

	public GameObject textos;

	public RawImage image;

	public Texture texturemapa;

	public GameObject[] misiones;

	public GameObject posters;

	public int numPosters;

	public TextMeshProUGUI textoInter;

	public TextMeshProUGUI textoDialogo;

	public Dialogo dialogo;

	private void Start()
	{
		misiones = GameObject.FindGameObjectsWithTag("mision");
		numPosters = GameObject.FindGameObjectsWithTag("poster").Length;
		posters.SetActive(value: false);
		if (done)
		{
			icono.SetActive(value: false);
		}
		Debug.Log(done);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !aceptarMision && jugadorCerca)
		{
			Debug.Log(done);
			if (done)
			{
				textoDialogo.text = "Muchas gracias por la ayuda, la verdad a mi me hubiera tomado un buen tiempo, los talleres son una gran oportunidad de crecer como persona, desarrollar un nuevo talento, o simplemente aprender algo nuevo, si deseas mas informes puedes visitar esta oficina.";
				panelDialogo.SetActive(value: true);
				panelInteraccion.SetActive(value: false);
				botonThanks.SetActive(value: true);
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
		panelInteraccion.SetActive(value: false);
		icono.SetActive(value: false);
		deny();
		TriggerDialogue();
		botonOk.SetActive(value: true);
		panelMapa.SetActive(value: true);
		image.texture = texturemapa;
		textos.SetActive(value: false);
		textoInter.text = "Presiona E para interactuar";
		posters.SetActive(value: true);
	}

	public void No()
	{
		panelDialogo.SetActive(value: false);
		botonNo.SetActive(value: false);
		botonSi.SetActive(value: false);
		panelInteraccion.SetActive(value: true);
	}

	private void deny()
	{
		for (int i = 0; i < misiones.Length; i++)
		{
			if (misiones[i].GetComponent<Mision>().numeroMision != 4)
			{
				misiones[i].SetActive(value: false);
				Debug.Log(misiones[i].GetComponent<Mision>().numeroMision);
			}
		}
		for (int j = 0; j < Object.FindObjectsOfType<LCartel>().Length; j++)
		{
			Object.FindObjectsOfType<LCartel>()[j].enabled = false;
		}
		Object.FindObjectOfType<LogicaExplore>().enabled = false;
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
		Object.FindObjectOfType<LogicaExplore>().enabled = true;
		Object.FindObjectOfType<LogicaNPC>().enabled = true;
		Object.FindObjectOfType<LogicaAlmacen>().enabled = true;
		Object.FindObjectOfType<LogicaAdministrativo>().enabled = true;
		Object.FindObjectOfType<Activar_Alimentos>().enabled = true;
		Object.FindObjectOfType<Activar_Calidad>().enabled = true;
		Object.FindObjectOfType<Activar_Electronico>().enabled = true;
		Object.FindObjectOfType<Activar_Space>().enabled = true;
	}

	public IEnumerator esconder()
	{
		done = true;
		panelMapa.SetActive(value: false);
		textos.SetActive(value: true);
		panelMision.SetActive(value: true);
		yield return new WaitForSeconds(10f);
		panelMision.SetActive(value: false);
		aceptarMision = false;
		for (int i = 0; i < Object.FindObjectsOfType<LogicaPoster>().Length; i++)
		{
			Object.FindObjectsOfType<LogicaPoster>()[i].enabled = false;
		}
	}

	public void TriggerDialogue()
	{
		Object.FindObjectOfType<DialogueManagerPosters>().StartDialogue(dialogo);
	}
}
