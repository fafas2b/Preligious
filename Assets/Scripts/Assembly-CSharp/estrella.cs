using UnityEngine;

public class estrella : MonoBehaviour
{
	public int rows = 4;

	public int columns = 5;

	public estrellas[] prefabs;

	private void Awake()
	{
		for (int i = 0; i < rows; i++)
		{
			float num = 2f * (float)(columns - 1);
			float num2 = 1f * (float)(rows - 1);
			Vector2 vector = new Vector2((0f - num) / 2f, (0f - num2) / 2f);
			Vector3 vector2 = new Vector3(vector.x + (float)i * 0.7f, vector.y + (float)i * 1f, 0f);
			for (int j = 0; j < columns; j++)
			{
				estrellas obj = Object.Instantiate(prefabs[i], base.transform);
				Vector3 localPosition = vector2;
				localPosition.x += Random.value + ((float)j + 0.7f) * (Random.value + 1f);
				localPosition.y += (Random.value + ((float)j + 1.2f)) * (Random.value - 0.5f);
				obj.transform.localPosition = localPosition;
			}
		}
	}
}
