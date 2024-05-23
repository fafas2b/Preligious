using UnityEngine;

public class LogicaNote : MonoBehaviour
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
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
		{
			aura.SetActive(value: false);
			panelInteraccion.SetActive(value: false);
			base.gameObject.GetComponent<MeshRenderer>().enabled = true;
			panelDialogo.SetActive(value: true);
			botonOk.SetActive(value: true);
			dialogo.leido = true;
			TriggerDialogue();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !Object.FindObjectOfType<LogicaAdministrativo>().done)
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
