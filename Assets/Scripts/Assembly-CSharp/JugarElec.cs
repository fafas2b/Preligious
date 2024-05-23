using UnityEngine;
using UnityEngine.SceneManagement;

public class JugarElec : MonoBehaviour
{
	public void LoadScene(string SceneName)
	{
		SceneManager.LoadScene(SceneName);
	}
}
