using System.Collections;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class StartManager : MonoBehaviour
{
	public DatabaseReference databaseReference;

	public GameObject intro;

	public GameObject animTerminada;

	private void Start()
	{
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
		StartCoroutine(verify());
	}

	public IEnumerator write()
	{
		string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		Task dbTask = databaseReference.Child("Users").Child("Jugador").Child(userId)
			.Child("cinematica")
			.SetValueAsync("1");
		yield return new WaitUntil(() => dbTask.IsCompleted);
		if (dbTask.Exception != null)
		{
			Debug.LogWarning($"Failed to register with {dbTask.Exception}");
		}
		else
		{
			Debug.Log("Exito");
		}
	}

	public IEnumerator verify()
	{
		string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
		Debug.Log(userId);
		Task<DataSnapshot> dbTask = databaseReference.Child("Users").Child("Jugador").Child(userId)
			.GetValueAsync();
		yield return new WaitUntil(() => dbTask.IsCompleted);
		if (dbTask.Exception != null)
		{
			Debug.LogWarning($"Failed to register with {dbTask.Exception}");
		}
		else if (dbTask.Result.Value != null && dbTask.Result.Child("cinematica").Exists)
		{
			intro.SetActive(value: false);
			animTerminada.SetActive(value: true);
			Debug.Log("check");
		}
	}
}
