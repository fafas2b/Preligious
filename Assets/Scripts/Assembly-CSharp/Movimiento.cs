using UnityEngine;

public class Movimiento : MonoBehaviour
{
	private float mover = 0.0104f;

	private int bandera;

	private int pasos;

	public Animator anim;

	private void Update()
	{
		if (bandera == 1)
		{
			base.transform.Translate(mover, 0f, 0f);
			pasos++;
			if (pasos == 620)
			{
				pasos = 0;
				stop();
			}
		}
	}

	public void llamar()
	{
		anim = GetComponent<Animator>();
		anim.SetTrigger("Movimiento");
		bandera = 1;
		Update();
	}

	private void stop()
	{
		bandera = 0;
	}
}
