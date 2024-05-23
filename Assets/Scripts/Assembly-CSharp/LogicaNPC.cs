using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicaNPC : MonoBehaviour
{
	public GameObject simboloMision;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject panelInteraccion;

	public GameObject panelMapa;

	public TextMeshProUGUI dialogo;

	public TextMeshProUGUI textoObjetivo;

	public bool jugadorCerca;

	public bool aceptarMision;

	public GameObject objetivos;

	public GameObject textos;

	public GameObject[] misiones;

	public Texture texturemapa;

	public int numObjetivos;

	public GameObject botonOK;

	public GameObject botonContinuar;

	public GameObject botonSi;

	public GameObject botonNo;

	public GameObject botonYes;

	public RawImage image;

	public bool end;

	public bool done;

	private bool dialog;

	private void Start()
	{
		if (done)
		{
			simboloMision.SetActive(value: false);
			return;
		}
		numObjetivos = GameObject.FindGameObjectsWithTag("papeleta").Length;
		textoObjetivo.text = "Obtén todos los trozos \nRestantes: " + numObjetivos;
		simboloMision.SetActive(value: true);
		objetivos.SetActive(value: false);
		panelDialogo.SetActive(value: false);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && !aceptarMision && jugadorCerca)
		{
			if (done)
			{
				dialogo.text = "Muchas gracias por el favor que me hiciste. La papeleta es el formato que debes de utilizar para poder pedir material en el almacen.";
				panelDialogo.SetActive(value: true);
				panelInteraccion.SetActive(value: false);
				botonContinuar.SetActive(value: true);
			}
			else if (end)
			{
				panelInteraccion.SetActive(value: false);
				panelDialogo.SetActive(value: true);
				botonYes.SetActive(value: true);
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

	public void No()
	{
		panelDialogo.SetActive(value: false);
		botonNo.SetActive(value: false);
		botonSi.SetActive(value: false);
		panelInteraccion.SetActive(value: true);
	}

	public void Si()
	{
		if (done)
		{
			panelDialogo.SetActive(value: false);
			botonContinuar.SetActive(value: false);
		}
		else if (dialog)
		{
			aceptarMision = true;
			textos.SetActive(value: false);
			jugadorCerca = false;
			simboloMision.SetActive(value: false);
			botonNo.SetActive(value: false);
			botonSi.SetActive(value: false);
			panelDialogo.SetActive(value: false);
			panelInteraccion.SetActive(value: false);
			botonContinuar.SetActive(value: false);
			panelMision.SetActive(value: true);
			objetivos.SetActive(value: true);
			panelMapa.SetActive(value: true);
			image.texture = texturemapa;
			deny();
		}
	}

	public void Ok()
	{
		end = true;
		panelMision.SetActive(value: false);
		botonOK.SetActive(value: false);
		simboloMision.SetActive(value: true);
		aceptarMision = false;
		dialogo.text = "Muchas gracias, me has un hecho un gran favor, recuerda que es\nmuy importante tener tu papeleta lista para pedir material en el almacen. Procura siempre tener el material adecuado para tus practicas. Gracias de nuevo!";
	}

	public void Continuar()
	{
		if (!dialog)
		{
			dialogo.text = "Muchas gracias, mi nombre es David, perdi mi papeleta y peor aun esta rota en pedazos, antes de darme cuenta visite los kioskos cerca del Modulo D, despues estuve en el Modulo N, y por ultimo me dirigia hacia el almacen del Modulo F. Podrias comenzar buscando por esos lugares.";
			botonNo.SetActive(value: false);
			botonSi.SetActive(value: false);
			botonContinuar.SetActive(value: true);
			dialog = true;
		}
	}

	public void acabar()
	{
		botonYes.SetActive(value: false);
		panelMapa.SetActive(value: false);
		panelDialogo.SetActive(value: false);
		simboloMision.SetActive(value: false);
		textos.SetActive(value: true);
		activate();
		done = true;
	}

	private void deny()
	{
		for (int i = 0; i < misiones.Length; i++)
		{
			if (misiones[i].GetComponent<Mision>().numeroMision != 1)
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
		Object.FindObjectOfType<LogicaTaller>().enabled = false;
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
		Object.FindObjectOfType<LogicaTaller>().enabled = true;
		Object.FindObjectOfType<LogicaAlmacen>().enabled = true;
		Object.FindObjectOfType<LogicaAdministrativo>().enabled = true;
		Object.FindObjectOfType<Activar_Alimentos>().enabled = true;
		Object.FindObjectOfType<Activar_Calidad>().enabled = true;
		Object.FindObjectOfType<Activar_Electronico>().enabled = true;
		Object.FindObjectOfType<Activar_Space>().enabled = true;
	}
}
