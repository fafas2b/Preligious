using UnityEngine;

public class RandomizerComp : MonoBehaviour
{
	private void Awake()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			GameObject gameObject = base.transform.GetChild(i).gameObject;
			GameObject obj = base.transform.GetChild(Random.Range(0, base.transform.childCount)).gameObject;
			Vector2 vector = obj.transform.position;
			Vector2 vector2 = gameObject.transform.position;
			gameObject.transform.position = vector;
			obj.transform.position = vector2;
		}
	}
}
