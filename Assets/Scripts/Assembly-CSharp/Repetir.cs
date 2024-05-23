using UnityEngine;
using UnityEngine.SceneManagement;

public class Repetir : MonoBehaviour
{
	public int Scene;

	private void OnMouseDown()
	{
		SceneManager.LoadScene(Scene);
	}
}
