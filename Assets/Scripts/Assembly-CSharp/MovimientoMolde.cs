using UnityEngine;

public class MovimientoMolde : MonoBehaviour
{
	public Animator anim;

	private void Start()
	{
		anim.GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			anim.SetTrigger("Movimiento");
		}
	}
}
