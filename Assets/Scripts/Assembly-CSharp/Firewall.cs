using UnityEngine;

public class Firewall : MonoBehaviour
{
	public int bandera;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
		{
			base.gameObject.SetActive(value: false);
		}
		if (other.gameObject.layer == LayerMask.NameToLayer("Covi") || other.gameObject.layer == LayerMask.NameToLayer("Bala"))
		{
			bandera++;
			Object.Destroy(other.gameObject);
			if (bandera >= 4)
			{
				base.gameObject.SetActive(value: false);
			}
		}
	}
}
