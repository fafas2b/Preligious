using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text TextTiempo;

	public float timer;

	private int cont;

	public bool tiempoRest = true;

	public GameObject fallar;

	private void Update()
	{
		if (tiempoRest)
		{
			timer -= Time.deltaTime;
		}
		TextTiempo.text = "0" + cont.ToString("f0") + ":" + timer.ToString("f0");
		if (timer > 60f)
		{
			cont++;
			timer -= 60f;
		}
		if (timer <= 0f && cont > 0)
		{
			cont--;
			timer = 59f;
		}
		else if (timer <= 0f && cont == 0)
		{
			TextTiempo.text = "00:00";
			fallar.SetActive(value: true);
		}
		if (timer < 10f)
		{
			TextTiempo.text = "0" + cont.ToString("f0") + ":0" + timer.ToString("f0");
		}
	}
}
