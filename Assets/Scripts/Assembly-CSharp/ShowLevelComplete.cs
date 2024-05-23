using UnityEngine;

public class ShowLevelComplete : MonoBehaviour
{
	public GameObject panelLevel;

	public static ShowLevelComplete showLevelComplete;

	private void Start()
	{
		showLevelComplete = this;
	}

	public void show()
	{
		panelLevel.SetActive(value: true);
	}

	private void Update()
	{
	}
}
