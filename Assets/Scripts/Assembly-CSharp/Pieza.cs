using UnityEngine;

public class Pieza : MonoBehaviour
{
	private bool drag;

	private bool _placed;

	private Vector2 offset;

	private Vector2 Origen;

	private Slot _piezaSlot;

	public ManagerJuntar cont;

	[SerializeField]
	private SpriteRenderer _renderer;

	private void Awake()
	{
		Origen = base.transform.position;
	}

	private void Start()
	{
		cont = Object.FindObjectOfType<ManagerJuntar>();
	}

	private void OnMouseDown()
	{
		drag = true;
		offset = GetMousePosition() - (Vector2)base.transform.position;
	}

	private void Update()
	{
		if (!_placed && drag)
		{
			Vector2 mousePosition = GetMousePosition();
			base.transform.position = mousePosition - offset;
		}
	}

	private Vector2 GetMousePosition()
	{
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	private void OnMouseUp()
	{
		if (Vector2.Distance(base.transform.position, _piezaSlot.transform.position) < 1f)
		{
			base.transform.position = _piezaSlot.transform.position;
			_placed = true;
			cont.contador++;
		}
		else
		{
			base.transform.position = Origen;
			drag = false;
		}
	}

	public void Init(Slot piezaSlot)
	{
		_piezaSlot = piezaSlot;
		_renderer.sprite = _piezaSlot.Renderer.sprite;
	}
}
