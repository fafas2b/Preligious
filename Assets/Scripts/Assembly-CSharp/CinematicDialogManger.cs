using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CinematicDialogManger : MonoBehaviour
{
	[Serializable]
	public struct _sentence
	{
		public int _char;

		public string _2say;

		public bool active;

		public bool desactive;
	}

	private Animator anim;

	public GameObject intro;

	public GameObject Dialogo;

	public GameObject Cinematica;

	public GameObject animTerminada;

	public TextMeshProUGUI[] textContainer;

	public _sentence[] sentences;

	public GameObject cameracin;

	[SerializeField]
	private float speedWrite;

	private int index;

	private void Start()
	{
		CleanText();
		StartCoroutine(Talk());
	}

	public void CleanText()
	{
		for (int i = 0; i < textContainer.Length; i++)
		{
			textContainer[i].text = "";
		}
	}

	public void ContinueDialog()
	{
		if (sentences[index].active)
		{
			index++;
			if (index < sentences.Length)
			{
				StartCoroutine(Talk());
				return;
			}
			CleanText();
			Cinematica.SetActive(value: true);
			Dialogo.SetActive(value: false);
		}
	}

	private IEnumerator Talk()
	{
		textContainer[sentences[index]._char].text = "";
		char[] array = sentences[index]._2say.ToCharArray();
		foreach (char c in array)
		{
			textContainer[sentences[index]._char].text += c;
			yield return new WaitForSeconds(speedWrite);
		}
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			ContinueDialog();
		}
	}
}
