using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManagerPosters : MonoBehaviour
{
	private Queue<string> sentences;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public TextMeshProUGUI textoDialogo;

	public TextMeshProUGUI textoMision;

	public int posters;

	public string lugar;

	public string lista;

	private void Start()
	{
		sentences = new Queue<string>();
	}

	private void Update()
	{
	}

	public void StartDialogue(Dialogo dialogo)
	{
		lugar = dialogo.Lugar;
		sentences.Clear();
		string[] array = dialogo.sentences;
		foreach (string item in array)
		{
			sentences.Enqueue(item);
		}
		DisplayNext();
	}

	public void DisplayNext()
	{
		if (sentences.Count == 0)
		{
			endDialogue();
			return;
		}
		string text = sentences.Dequeue();
		textoDialogo.text = text;
	}

	public void endDialogue()
	{
		Debug.Log("Dialogo terminado");
		panelDialogo.SetActive(value: false);
		botonOk.SetActive(value: false);
		panelMision.SetActive(value: true);
		if (lugar.Equals("Profesor"))
		{
			panelMision.SetActive(value: true);
			botonOk.SetActive(value: false);
			textoMision.text = "Modulos:\nModulo A\nModulo N\nModulo H\nModulo B\nModulo C\nModulo I";
			lista = textoMision.text;
			return;
		}
		posters = Object.FindObjectsOfType<LogicaPoster>().Length;
		for (int i = 0; i < posters; i++)
		{
			if (Object.FindObjectsOfType<LogicaPoster>()[i].dialogo.Lugar == lugar)
			{
				Object.FindObjectsOfType<LogicaPoster>()[i].dialogo.leido = true;
				Object.FindObjectOfType<LogicaTaller>().numPosters--;
				lista = lista.Remove(lista.IndexOf(lugar), 8);
				textoMision.text = lista;
			}
		}
		if (Object.FindObjectOfType<LogicaTaller>().aceptarMision && Object.FindObjectOfType<LogicaTaller>().numPosters == 0)
		{
			textoMision.text = "Mision Completada";
			StartCoroutine(Object.FindObjectOfType<LogicaTaller>().esconder());
			Object.FindObjectOfType<LogicaTaller>().activate();
		}
	}
}
