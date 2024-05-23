using UnityEngine;

public class LCartel : MonoBehaviour
{
	public bool jugadorCerca;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject botonOk;

	public DialogoNPC npcDialogo;

	private void Start()
	{
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
		{
			panelDialogo.SetActive(value: true);
			panelInteraccion.SetActive(value: false);
			botonOk.SetActive(value: true);
			TriggerDialogue();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
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
		Object.FindObjectOfType<DialogoM>().StartDialogue(npcDialogo);
	}
}
