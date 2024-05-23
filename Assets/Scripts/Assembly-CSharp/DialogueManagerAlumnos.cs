using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManagerAlumnos : MonoBehaviour
{
	private Queue<string> sentences;

	public GameObject paneldialogo;

	public GameObject panelMision;

	public GameObject botonOk;

	public TextMeshProUGUI textoDialogo;

	public string nombre;

	private void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogo dialogo)
	{
		nombre = dialogo.Lugar;
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
		paneldialogo.SetActive(value: false);
		botonOk.SetActive(value: false);
		panelMision.SetActive(value: true);
		int num = Object.FindObjectsOfType<LogicaAlumnos>().Length;
		for (int i = 0; i < num; i++)
		{
			if (Object.FindObjectsOfType<LogicaAlumnos>()[i].dialogo.Lugar == nombre)
			{
				Object.FindObjectsOfType<LogicaAlumnos>()[i].dialogo.leido = true;
			}
		}
		if (Object.FindObjectOfType<LogicaAlmacen>().aceptarMision)
		{
			Object.FindObjectOfType<LogicaAlmacen>().numAlumnos--;
			Object.FindObjectOfType<LogicaAlmacen>().textoMision.text = "Alumnos por avisar: " + Object.FindObjectOfType<LogicaAlmacen>().numAlumnos;
			if (Object.FindObjectOfType<LogicaAlmacen>().numAlumnos == 0)
			{
				Object.FindObjectOfType<LogicaAlmacen>().textoMision.text = "Mision Completada";
				StartCoroutine(Object.FindObjectOfType<LogicaAlmacen>().esconder());
				Object.FindObjectOfType<LogicaAlmacen>().aceptarMision = false;
				Object.FindObjectOfType<LogicaAlmacen>().activate();
			}
		}
	}
}
