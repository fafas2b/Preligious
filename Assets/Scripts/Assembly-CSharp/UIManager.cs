using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	public static UIManager instance;

	public GameObject pantInicio;

	public GameObject creditosUI;

	public void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			Debug.Log("Instance already exist, destroying object");
			Object.Destroy(this);
		}
	}

	public void StartScreen()
	{
		pantInicio.SetActive(value: true);
		creditosUI.SetActive(value: false);
	}

	public void shutDown()
	{
		Application.Quit();
	}

	public void iniciar()
	{
		SceneManager.LoadScene("Selection", LoadSceneMode.Single);
	}

	public void creditos()
	{
		pantInicio.SetActive(value: false);
		creditosUI.SetActive(value: true);
	}
}
