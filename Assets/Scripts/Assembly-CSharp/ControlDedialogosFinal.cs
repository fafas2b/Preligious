using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDedialogosFinal : MonoBehaviour
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

	public TextMeshProUGUI[] textContainer;

	public _sentence[] sentences;

	public string scene;

	[SerializeField]
	public GameObject[] dialogBoxes;

	[SerializeField]
	private float speedWrite;

	private int index;

	private void Start()
	{
		CleanText();
		if (sentences[index].active)
		{
			EnableDisableBox(enable: true);
		}
		StartCoroutine(Talk());
	}

	public void CleanText()
	{
		for (int i = 0; i < textContainer.Length; i++)
		{
			textContainer[i].text = "";
		}
	}

	public void EnableDisableBox(bool enable)
	{
		dialogBoxes[sentences[index]._char].SetActive(enable);
	}

	public void ContinueDialog()
	{
		if (!sentences[index].active)
		{
			return;
		}
		EnableDisableBox(enable: false);
		index++;
		if (index < sentences.Length)
		{
			if (sentences[index].active)
			{
				EnableDisableBox(enable: true);
			}
			StartCoroutine(Talk());
		}
		else
		{
			CleanText();
			SceneManager.LoadScene(scene, LoadSceneMode.Single);
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
		if (Input.GetMouseButtonDown(0))
		{
			ContinueDialog();
		}
	}
}
