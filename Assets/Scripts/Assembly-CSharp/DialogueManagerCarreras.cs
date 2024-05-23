using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManagerCarreras : MonoBehaviour
{
	private Queue<string> sentences;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public GameObject note;

	public TextMeshProUGUI textoDialogo;

	public TextMeshProUGUI textoMision;

	public int maestros;

	public string lugar;

	public string lista;

	private void Start()
	{
		sentences = new Queue<string>();
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
		lista = textoMision.text;
		Debug.Log("Dialogo terminado");
		switch (lugar)
		{
		case "Administracion":
			textoMision.text = "Objetivos:\nHabla con el profesor de\nDesarrollo de Software\nDesarrollo Electronico\nTecnologias Quimicas\nCalidad Total y Productividad";
			panelDialogo.SetActive(value: false);
			botonOk.SetActive(value: false);
			panelMision.SetActive(value: true);
			break;
		case "Desarrollo de Software":
			textoMision.text = lista.Remove(lista.IndexOf(lugar), 22);
			Object.FindObjectOfType<LogicaAdministrativo>().numMaestros--;
			break;
		case "Desarrollo Electronico":
			textoMision.text = lista.Remove(lista.IndexOf(lugar), 22);
			Object.FindObjectOfType<LogicaAdministrativo>().numMaestros--;
			break;
		case "Tecnologias Quimicas":
			textoMision.text = lista.Remove(lista.IndexOf(lugar), 20);
			Object.FindObjectOfType<LogicaAdministrativo>().numMaestros--;
			break;
		case "Calidad Total y Productividad":
			textoMision.text = lista.Remove(lista.IndexOf(lugar), 29);
			Object.FindObjectOfType<LogicaAdministrativo>().numMaestros--;
			break;
		case "Note":
			textoMision.text = "Mision Completada";
			StartCoroutine(Object.FindObjectOfType<LogicaAdministrativo>().esconder());
			break;
		}
		panelDialogo.SetActive(value: false);
		botonOk.SetActive(value: false);
		panelMision.SetActive(value: true);
		if (Object.FindObjectOfType<LogicaAdministrativo>().numMaestros == 0 && !note.GetComponent<LogicaNote>().dialogo.leido)
		{
			textoMision.text = "Objetivos:\nColoca la nota en el salon E23";
			note.SetActive(value: true);
		}
	}
}
