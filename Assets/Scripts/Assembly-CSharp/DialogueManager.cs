using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
	private Queue<string> sentences;

	public TextMeshProUGUI cuadrodialogo;

	public GameObject paneldialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public string lugar;

	public void Start()
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
		cuadrodialogo.text = text;
	}

	public void endDialogue()
	{
		Debug.Log("Dialogo terminado");
		paneldialogo.SetActive(value: false);
		botonOk.SetActive(value: false);
		panelMision.SetActive(value: true);
		int num = Object.FindObjectsOfType<LogicaCartel>().Length;
		for (int i = 0; i < num; i++)
		{
			if (Object.FindObjectsOfType<LogicaCartel>()[i].dialogo.Lugar == lugar)
			{
				Object.FindObjectsOfType<LogicaCartel>()[i].dialogo.leido = true;
			}
		}
		if (Object.FindObjectOfType<LogicaExplore>().aceptarMision)
		{
			Object.FindObjectOfType<LogicaExplore>().modulos--;
			Object.FindObjectOfType<LogicaExplore>().textoMision.text = "Modulos por visitar: " + Object.FindObjectOfType<LogicaExplore>().modulos;
			if (Object.FindObjectOfType<LogicaExplore>().modulos == 0)
			{
				StartCoroutine(Object.FindObjectOfType<LogicaExplore>().esconder());
				Object.FindObjectOfType<LogicaExplore>().panelMision.SetActive(value: false);
				Object.FindObjectOfType<LogicaExplore>().aceptarMision = false;
				Object.FindObjectOfType<LogicaExplore>().activate();
			}
		}
	}
}
