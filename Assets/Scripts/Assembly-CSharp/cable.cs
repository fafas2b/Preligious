using UnityEngine;

public class cable : MonoBehaviour
{
	public SpriteRenderer finalCable;

	public GameObject Luz;

	private Vector2 posicionOriginal;

	private Vector2 tamanoOriginal;

	private TareaCable tareaCable;

	public GameObject Defeat;

	private void Start()
	{
		posicionOriginal = base.transform.position;
		tamanoOriginal = finalCable.size;
		tareaCable = base.transform.root.gameObject.GetComponent<TareaCable>();
	}

	private void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			Reiniciar();
		}
	}

	private void OnMouseDrag()
	{
		ActualizarPosicion();
		ComprobarConexion();
		ActualizarRotacion();
		ActualizarTamano();
	}

	private void ActualizarPosicion()
	{
		Vector2 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		base.transform.position = new Vector3(vector.x, vector.y);
	}

	private void ActualizarRotacion()
	{
		Vector2 vector = base.transform.position;
		Vector2 vector2 = base.transform.parent.position;
		Vector2 to = vector - vector2;
		float z = Vector2.SignedAngle(Vector2.right * base.transform.lossyScale, to);
		base.transform.rotation = Quaternion.Euler(0f, 0f, z);
	}

	private void ActualizarTamano()
	{
		Vector2 b = base.transform.position;
		float num = Vector2.Distance(posicionOriginal, b);
		finalCable.size = new Vector2(num * 5f, finalCable.size.y);
	}

	private void Reiniciar()
	{
		base.transform.position = posicionOriginal;
		base.transform.rotation = Quaternion.identity;
		finalCable.size = new Vector2(tamanoOriginal.x, tamanoOriginal.y);
	}

	private void ComprobarConexion()
	{
		Collider2D[] array = Physics2D.OverlapCircleAll(base.transform.position, 0.2f);
		foreach (Collider2D collider2D in array)
		{
			if (collider2D.gameObject != base.gameObject)
			{
				base.transform.position = collider2D.transform.position;
				cable component = collider2D.gameObject.GetComponent<cable>();
				if (finalCable.color != component.finalCable.color)
				{
					Conectar();
					component.Conectar();
					Defeat.SetActive(value: true);
				}
				else
				{
					Conectar();
					component.Conectar();
					tareaCable.conexionesActuales++;
					tareaCable.TareaCompletada();
				}
			}
		}
	}

	public void Conectar()
	{
		Luz.SetActive(value: true);
		Object.Destroy(this);
	}
}
