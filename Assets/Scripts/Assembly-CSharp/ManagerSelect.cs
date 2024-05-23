using UnityEngine;

public class ManagerSelect : MonoBehaviour
{
	public GameObject animacion;

	public GameObject seleccion;

	public void SeleccionPersonajes()
	{
		animacion.SetActive(value: false);
		seleccion.SetActive(value: true);
	}
}
