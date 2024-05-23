using UnityEngine;

public class Note : MonoBehaviour
{
	public GameObject panelNota;

	public string nombre;

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			StartCoroutine(Object.FindObjectOfType<MenuDialogueManager>().checkNotas());
			StartCoroutine(Object.FindObjectOfType<NotasManager>().esconderNota(nombre));
		}
	}
}
