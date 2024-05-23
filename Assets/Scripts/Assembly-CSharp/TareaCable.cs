using UnityEngine;

public class TareaCable : MonoBehaviour
{
	public int conexionesActuales;

	public GameObject scene1;

	public GameObject scene2;

	public GameObject scene3;

	public GameObject completado;

	public void TareaCompletada()
	{
		if (conexionesActuales == 3)
		{
			scene1.SetActive(value: true);
			scene2.SetActive(value: false);
			scene3.SetActive(value: false);
		}
		if (conexionesActuales == 6)
		{
			scene1.SetActive(value: false);
			scene2.SetActive(value: true);
			scene3.SetActive(value: false);
		}
		if (conexionesActuales == 9)
		{
			completado.SetActive(value: true);
		}
	}
}
