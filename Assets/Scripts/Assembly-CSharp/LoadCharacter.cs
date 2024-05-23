using UnityEngine;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] meshes;

	public Animator anim;

	public Avatar[] avatar;

	public Transform spawnpoint;

	public void Start()
	{
		int @int = PlayerPrefs.GetInt("selectedCharacter");
		for (int i = 0; i < meshes.Length; i++)
		{
			Debug.Log(meshes[i].name);
			if (i != @int)
			{
				meshes[i].SetActive(value: false);
			}
			if (i == @int)
			{
				anim.avatar = avatar[i];
			}
		}
		Debug.Log("Character Spawned");
	}
}
