using UnityEngine;

public class PresioneParaContinuar : MonoBehaviour
{
	public GameObject Ocultar;

	public GameObject desocultar;

	public static PresioneParaContinuar continuar;

	private void Start()
	{
		continuar = this;
	}

	private void Update()
	{
	}

	private void OnMouseDown()
	{
		Ocultar.SetActive(value: false);
		desocultar.SetActive(value: true);
	}
}
