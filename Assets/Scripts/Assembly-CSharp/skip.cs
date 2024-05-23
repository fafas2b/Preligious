using UnityEngine;

public class skip : MonoBehaviour
{
	public GameObject cameracin;

	public GameObject Dialogo;

	public GameObject animTerminada;

	public GameObject msg;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			Dialogo.SetActive(value: false);
			cameracin.SetActive(value: false);
			animTerminada.SetActive(value: true);
			msg.SetActive(value: false);
		}
	}
}
