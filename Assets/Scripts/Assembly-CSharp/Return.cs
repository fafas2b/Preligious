using UnityEngine;

public class Return : MonoBehaviour
{
	public GameObject CJ;

	private bool bandera;

	public GameObject Menu;

	public void regresar()
	{
		if (!bandera)
		{
			CJ.SetActive(value: true);
			Menu.SetActive(value: false);
			bandera = true;
		}
		else if (bandera)
		{
			CJ.SetActive(value: false);
			Menu.SetActive(value: true);
			bandera = false;
		}
	}
}
