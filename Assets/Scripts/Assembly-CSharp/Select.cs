using UnityEngine;
using UnityEngine.SceneManagement;

public class Select : MonoBehaviour
{
	private GameObject[] CharacterShowcase;

	private int index;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			toggleLeft();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			toggleRight();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			startGame();
		}
	}

	private void Start()
	{
		CharacterShowcase = new GameObject[base.transform.childCount];
		for (int i = 0; i < base.transform.childCount; i++)
		{
			CharacterShowcase[i] = base.transform.GetChild(i).gameObject;
		}
		GameObject[] characterShowcase = CharacterShowcase;
		for (int j = 0; j < characterShowcase.Length; j++)
		{
			characterShowcase[j].SetActive(value: false);
		}
		if ((bool)CharacterShowcase[0])
		{
			CharacterShowcase[0].SetActive(value: true);
		}
	}

	public void toggleLeft()
	{
		CharacterShowcase[index].SetActive(value: false);
		index--;
		if (index < 0)
		{
			index = CharacterShowcase.Length - 1;
		}
		CharacterShowcase[index].SetActive(value: true);
	}

	public void toggleRight()
	{
		CharacterShowcase[index].SetActive(value: false);
		index++;
		if (index == CharacterShowcase.Length)
		{
			index = 0;
		}
		CharacterShowcase[index].SetActive(value: true);
	}

	public void startGame()
	{
		PlayerPrefs.SetString("position_x", "0.7");
		PlayerPrefs.SetString("position_y", "-2.1");
		PlayerPrefs.SetString("position_z", "30.4");
		PlayerPrefs.SetInt("selectedCharacter", index);
		SceneManager.LoadScene("Juego");
	}
}
