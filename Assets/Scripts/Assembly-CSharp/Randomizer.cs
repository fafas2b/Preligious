using UnityEngine;

public class Randomizer : MonoBehaviour
{
	public GameObject Objeto1;

	public GameObject Objeto2;

	public GameObject Objeto3;

	public int Num;

	private void Start()
	{
		Num = Random.Range(1, 4);
		RandomizarNumero();
	}

	public void RandomizarNumero()
	{
		if (Num == 1)
		{
			Objeto1.SetActive(value: true);
			Objeto2.SetActive(value: false);
			Objeto3.SetActive(value: false);
		}
		if (Num == 2)
		{
			Objeto1.SetActive(value: false);
			Objeto2.SetActive(value: true);
			Objeto3.SetActive(value: false);
		}
		if (Num == 3)
		{
			Objeto1.SetActive(value: false);
			Objeto2.SetActive(value: false);
			Objeto3.SetActive(value: true);
		}
	}
}
