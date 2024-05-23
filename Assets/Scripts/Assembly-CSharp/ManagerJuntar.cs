using UnityEngine;

public class ManagerJuntar : MonoBehaviour
{
	[SerializeField]
	private Slot[] SlotPrefabs;

	[SerializeField]
	private Pieza itemPrefab;

	[SerializeField]
	private Transform slotParent;

	[SerializeField]
	private Transform piezaParent;

	public int contador;

	public GameObject victoria;

	private void Start()
	{
		Spawn();
	}

	private void Spawn()
	{
		for (int i = 0; i < SlotPrefabs.Length; i++)
		{
			Slot piezaSlot = Object.Instantiate(SlotPrefabs[i], slotParent.GetChild(i).position, Quaternion.identity);
			Object.Instantiate(itemPrefab, piezaParent.GetChild(i).position, Quaternion.identity).Init(piezaSlot);
		}
	}

	private void Update()
	{
		if (contador == 3)
		{
			Debug.Log("Victoria");
			victoria.SetActive(value: true);
			contador = 0;
		}
	}
}
