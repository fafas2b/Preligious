using System.Collections;
using Firebase.Database;
using UnityEngine;

public class NotasManager : MonoBehaviour
{
	public DatabaseReference databaseReference;

	public GameObject panelNota;

	public GameObject panelCredencial;

	public IEnumerator esconderNota(string nombre)
	{
		panelNota.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		panelNota.SetActive(value: false);
		for (int i = 0; i < Object.FindObjectsOfType<Note>().Length; i++)
		{
			if (Object.FindObjectsOfType<Note>()[i].nombre == nombre)
			{
				Object.FindObjectsOfType<Note>()[i].gameObject.SetActive(value: false);
				break;
			}
		}
	}

	public IEnumerator esconderCred(string nombre)
	{
		panelCredencial.SetActive(value: true);
		yield return new WaitForSeconds(2f);
		panelCredencial.SetActive(value: false);
		for (int i = 0; i < Object.FindObjectsOfType<Credencial>().Length; i++)
		{
			if (Object.FindObjectsOfType<Credencial>()[i].nombre == nombre)
			{
				Object.FindObjectsOfType<Credencial>()[i].gameObject.SetActive(value: false);
				break;
			}
		}
	}
}
