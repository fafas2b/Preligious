using UnityEngine;

public class Credencial : MonoBehaviour
{
	public string nombre;

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			StartCoroutine(Object.FindObjectOfType<MenuDialogueManager>().checkCredenciales());
			StartCoroutine(Object.FindObjectOfType<NotasManager>().esconderCred(nombre));
		}
	}
}
