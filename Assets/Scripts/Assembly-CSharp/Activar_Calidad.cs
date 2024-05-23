using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Activar_Calidad : MonoBehaviour
{
	public bool jugadorCerca;

	public GameObject panelInteraccion;

	public GameObject panelDialogo;

	public GameObject botonSi;

	public GameObject botonNo;

	public GameObject panelLoading;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E) && jugadorCerca)
		{
			panelInteraccion.SetActive(value: false);
			panelDialogo.SetActive(value: true);
			botonNo.SetActive(value: true);
			botonSi.SetActive(value: true);
			panelInteraccion.SetActive(value: false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			jugadorCerca = true;
			panelInteraccion.SetActive(value: true);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			jugadorCerca = false;
			panelInteraccion.SetActive(value: false);
		}
	}

	public void aceptar()
	{
		si();
	}

	public void si()
	{
		panelLoading.SetActive(value: true);
		PlayerPrefs.SetString("position_x", Object.FindObjectOfType<StarterAssetsInputs>().transform.position.x.ToString());
		PlayerPrefs.SetString("position_y", Object.FindObjectOfType<StarterAssetsInputs>().transform.position.y.ToString());
		PlayerPrefs.SetString("position_z", Object.FindObjectOfType<StarterAssetsInputs>().transform.position.z.ToString());
		SceneManager.LoadScene("InicioCalidad", LoadSceneMode.Single);
	}

	public void No()
	{
		panelDialogo.SetActive(value: false);
		panelInteraccion.SetActive(value: false);
		botonNo.SetActive(value: false);
		botonSi.SetActive(value: false);
	}
}
