using UnityEngine;

public class ChecarTarea : MonoBehaviour
{
	public int cont;

	public GameObject scene1;

	public GameObject scene2;

	public GameObject scene3;

	public GameObject completado;

	public Timer tiempo;

	private void Start()
	{
		tiempo = Object.FindObjectOfType<Timer>();
	}

	public void TareaCompletada()
	{
		if (cont == 2)
		{
			scene1.SetActive(value: false);
			scene2.SetActive(value: true);
			scene3.SetActive(value: false);
		}
		if (cont == 4)
		{
			scene1.SetActive(value: false);
			scene2.SetActive(value: false);
			scene3.SetActive(value: true);
		}
		if (cont == 6)
		{
			completado.SetActive(value: true);
			tiempo.tiempoRest = false;
		}
	}
}
