using UnityEngine;

public class LogicaMaestros : MonoBehaviour
{
	public Dialogo dialogo;

	public bool jugadorCerca;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public GameObject aura;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca && !dialogo.leido)
		{
			aura.SetActive(value: false);
			panelInteraccion.SetActive(value: false);
			panelDialogo.SetActive(value: true);
			panelMision.SetActive(value: false);
			botonOk.SetActive(value: true);
			dialogo.leido = true;
			TriggerDialogue();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !Object.FindObjectOfType<LogicaAdministrativo>().done && !dialogo.leido)
		{
			jugadorCerca = true;
			panelInteraccion.SetActive(value: true);
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

	public void TriggerDialogue()
	{
		Object.FindObjectOfType<DialogueManagerCarreras>().StartDialogue(dialogo);
	}
}
