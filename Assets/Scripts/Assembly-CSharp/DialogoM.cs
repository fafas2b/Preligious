using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogoM : MonoBehaviour
{
	private Queue<string> sentences;

	public TextMeshProUGUI cuadrodialogo;

	public GameObject paneldialogo;

	public GameObject botonOk;

	public string nameNpc;

	public void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(DialogoNPC dialogoNPC)
	{
		nameNpc = dialogoNPC.nombreNPC;
		sentences.Clear();
		string[] array = dialogoNPC.sentences;
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
	}
}
