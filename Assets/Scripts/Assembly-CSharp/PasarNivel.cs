using UnityEngine;
using UnityEngine.SceneManagement;

public class PasarNivel : MonoBehaviour
{
	public string nivel;

	public void changeScene()
	{
		SceneManager.LoadScene(nivel, LoadSceneMode.Single);
		Debug.Log("check     " + nivel);
	}
}
