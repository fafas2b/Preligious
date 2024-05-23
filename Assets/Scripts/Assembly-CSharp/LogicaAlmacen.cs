using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaAlmacen : MonoBehaviour
{
	public GameObject panelInteraccion;

	public GameObject panelMision;

	public GameObject panelTimer;

	public GameObject panelDialogo;

	public GameObject botonSi;

	public GameObject botonNo;

	public GameObject botonThanks;

	public GameObject botonOk;

	public GameObject panelMapa;

	public GameObject textos;

	public RawImage image;

	public Texture texturemapa;

	public GameObject icono;

	public TextMeshProUGUI textoMision;

	public TextMeshProUGUI textoDialogo;

	public TextMeshProUGUI textoTimer;

	public GameObject alumnos;

	public GameObject[] misiones;

	public int numAlumnos;

	public bool jugadorCerca;

	public bool aceptarMision;

	public bool done;

	public bool start;

	public static int segundos = 120;

	private void Start()
	{
		misiones = GameObject.FindGameObjectsWithTag("mision");
		if (done)
		{
			icono.SetActive(value: false);
			return;
		}
		numAlumnos = 5;
		textoMision.text = "Alumnos por avisar: " + numAlumnos;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca && !aceptarMision)
		{
			if (done)
			{
				panelInteraccion.SetActive(value: false);
				textoDialogo.text = "Gracias por la ayuda, ahora puedo retirarme a mis asuntos, es muy importante cuidar bien de los dispositivos electronicos que solicites ademas de entregarlos a tiempo. Pero bueno, gracias de nuevo, tengo asuntos que atender";
				panelDialogo.SetActive(value: true);
				botonThanks.SetActive(value: true);
			}
			else
			{
				textoDialogo.text = "Disculpa, necesito tu ayuda, veras, soy el encargado del almacen y necesito retirarme por una emergencia. El problema es que hay algunos chicos que aun no entregan su material, necesito que les avises para que me devuelvan el material antes de que yo me vaya. ¿Crees que puedas ayudarme?";
				panelDialogo.SetActive(value: true);
				botonNo.SetActive(value: true);
				botonSi.SetActive(value: true);
				panelInteraccion.SetActive(value: false);
			}
		}
		if (!start && segundos > 0 && !done)
		{
			StartCoroutine(timer());
		}
		else if (segundos == 0)
		{
			fracaso();
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
		alumnos.SetActive(value: true);
		panelMapa.SetActive(value: true);
		image.texture = texturemapa;
		textos.SetActive(value: false);
		deny();
		panelMision.SetActive(value: true);
		panelTimer.SetActive(value: true);
		textoMision.text = "Alumnos por avisar: " + numAlumnos;
		start = false;
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
			if (misiones[i].GetComponent<Mision>().numeroMision != 3)
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
		Object.FindObjectOfType<LogicaTaller>().enabled = false;
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
		Object.FindObjectOfType<LogicaTaller>().enabled = true;
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
		panelTimer.SetActive(value: false);
		yield return new WaitForSeconds(5f);
		panelMision.SetActive(value: false);
		alumnos.SetActive(value: false);
	}

	public IEnumerator timer()
	{
		start = true;
		yield return new WaitForSeconds(1f);
		segundos--;
		if (segundos >= 60)
		{
			if (segundos - 60 < 10)
			{
				textoTimer.text = "01:0" + (segundos - 60);
			}
			else
			{
				textoTimer.text = "01:" + (segundos - 60);
			}
		}
		else if (segundos < 10)
		{
			textoTimer.text = "00:0" + segundos;
		}
		else
		{
			textoTimer.text = "00:" + segundos;
		}
		start = false;
	}

	public void fracaso()
	{
		aceptarMision = false;
		start = true;
		done = false;
		numAlumnos = 5;
		segundos = 50;
		panelDialogo.SetActive(value: false);
		botonOk.SetActive(value: false);
		panelInteraccion.SetActive(value: false);
		panelMapa.SetActive(value: false);
		textos.SetActive(value: true);
		textoMision.text = "Has fallado la mision, se te ha agotado el tiempo";
		for (int i = 0; i < numAlumnos; i++)
		{
			Object.FindObjectsOfType<LogicaAlumnos>()[i].dialogo.leido = false;
		}
		StartCoroutine(esconder());
		activate();
	}
}
