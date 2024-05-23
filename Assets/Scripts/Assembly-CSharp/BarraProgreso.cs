using UnityEngine;
using UnityEngine.UI;

public class BarraProgreso : MonoBehaviour
{
	private Slider Barra;

	private float max;

	private float act;

	public Text valorString;

	private int cont;

	public float porcentaje;

	public GameObject GScene;

	private void Start()
	{
		Barra = GetComponent<Slider>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			pulsar();
		}
		if (porcentaje == 1f)
		{
			GScene.SetActive(value: true);
		}
	}

	private void ActualizarValorBarra(float valormax, float valorAct)
	{
		porcentaje = valorAct / valormax;
		Barra.value = porcentaje;
		valorString.text = porcentaje * 100f + "%";
	}

	private void pulsar()
	{
		if (cont <= 10)
		{
			ActualizarValorBarra(10f, cont);
			cont++;
		}
	}
}
