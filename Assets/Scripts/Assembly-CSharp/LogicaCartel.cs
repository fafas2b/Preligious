using UnityEngine;

public class LogicaCartel : MonoBehaviour
{
	public bool jugadorCerca;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public Dialogo dialogo;

	private void Start()
	{
		dialogo.leido = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca && !dialogo.leido)
		{
			panelDialogo.SetActive(value: true);
			panelInteraccion.SetActive(value: false);
			botonOk.SetActive(value: true);
			TriggerDialogue();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !dialogo.leido)
		{
			jugadorCerca = true;
			panelInteraccion.SetActive(value: true);
			panelMision.SetActive(value: false);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && !dialogo.leido)
		{
			jugadorCerca = false;
			panelInteraccion.SetActive(value: false);
			panelMision.SetActive(value: true);
		}
	}

	public void TriggerDialogue()
	{
		Object.FindObjectOfType<DialogueManager>().StartDialogue(dialogo);
	}
}
