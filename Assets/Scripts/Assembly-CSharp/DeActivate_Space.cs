using UnityEngine;
using UnityEngine.SceneManagement;

public class DeActivate_Space : MonoBehaviour
{
	public void regresar()
	{
		SceneManager.LoadScene("Juego", LoadSceneMode.Single);
	}
}
