using UnityEngine;

public class LogicaAlumnos : MonoBehaviour
{
	public bool jugadorCerca;

	public Dialogo dialogo;

	public GameObject panelInteraccion;

	public GameObject panelMision;

	public GameObject panelDialogo;

	public GameObject botonOk;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca && !dialogo.leido && Object.FindObjectOfType<LogicaAlmacen>().aceptarMision)
		{
			panelDialogo.SetActive(value: true);
			panelInteraccion.SetActive(value: false);
			botonOk.SetActive(value: true);
			TriggerDialogue();
		}
		if (!Object.FindObjectOfType<LogicaAlmacen>().aceptarMision)
		{
			jugadorCerca = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !dialogo.leido && Object.FindObjectOfType<LogicaAlmacen>().aceptarMision)
		{
			jugadorCerca = true;
			panelInteraccion.SetActive(value: true);
			panelMision.SetActive(value: false);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && !dialogo.leido && Object.FindObjectOfType<LogicaAlmacen>().aceptarMision)
		{
			jugadorCerca = false;
			panelInteraccion.SetActive(value: false);
			panelMision.SetActive(value: true);
		}
	}

	public void TriggerDialogue()
	{
		Object.FindObjectOfType<DialogueManagerAlumnos>().StartDialogue(dialogo);
	}
}
