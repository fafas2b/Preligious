using System.Collections;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;

public class MenuDialogueManager : MonoBehaviour
{
	public DatabaseReference databaseReference;

	public int numDialogos;

	public string tipo;

	public int numNotas;

	public int numCredenciales;

	public string[] textos;

	public TextMeshProUGUI textoInfo;

	public int index;

	private void Start()
	{
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
		StartCoroutine(checkNotas());
	}

	public void setText(Dialogo dialogo)
	{
		index = 0;
		tipo = dialogo.Lugar;
		textos = dialogo.sentences;
		textoInfo.text = textos[index];
		if (tipo.Equals("notas"))
		{
			numDialogos = numNotas;
		}
		else
		{
			numDialogos = textos.Length;
		}
	}

	public void nextTexto()
	{
		index++;
		if (index == numDialogos)
		{
			index = 0;
		}
		textoInfo.text = textos[index];
	}

	public void prevTexto()
	{
		index--;
		if (index < 0)
		{
			index = numDialogos - 1;
		}
		textoInfo.text = textos[index];
	}

	public IEnumerator checkNotas()
	{
		Task<DataSnapshot> dbTask = databaseReference.Child("Users").Child("Jugador").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
			.GetValueAsync();
		yield return new WaitUntil(() => dbTask.IsCompleted);
		if (dbTask.Exception != null)
		{
			Debug.LogWarning($"Failed to register with {dbTask.Exception}");
		}
		else
		{
			if (dbTask.Result.Value == null)
			{
				yield break;
			}
			DataSnapshot result = dbTask.Result;
			if (!result.Child("notas").Exists)
			{
				yield break;
			}
			Debug.Log("notas");
			numNotas = (int)result.Child("notas").ChildrenCount;
			if (numNotas != 10)
			{
				yield break;
			}
			if (result.Child("Logros").Child("Recolector Dedicado").Exists)
			{
				Debug.Log("Logro ya registrado");
				yield break;
			}
			string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
			Debug.Log(userId);
			Task dbTarea = databaseReference.Child("Users").Child("Jugador").Child(userId)
				.Child("Logros")
				.Child("Recolector Dedicado")
				.SetValueAsync("1");
			yield return new WaitUntil(() => dbTarea.IsCompleted);
			Debug.Log("done");
			if (dbTarea.Exception != null)
			{
				Debug.LogWarning($"Failed to register with {dbTarea.Exception}");
			}
			else
			{
				Debug.Log("Exito");
			}
		}
	}

	public IEnumerator checkCredenciales()
	{
		Task<DataSnapshot> dbTask = databaseReference.Child("Users").Child("Jugador").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
			.GetValueAsync();
		yield return new WaitUntil(() => dbTask.IsCompleted);
		if (dbTask.Exception != null)
		{
			Debug.LogWarning($"Failed to register with {dbTask.Exception}");
		}
		else
		{
			if (dbTask.Result.Value == null)
			{
				yield break;
			}
			DataSnapshot result = dbTask.Result;
			if (!result.Child("credenciales").Exists)
			{
				yield break;
			}
			Debug.Log("credenciales");
			numCredenciales = (int)result.Child("credenciales").ChildrenCount;
			if (numCredenciales != 10)
			{
				yield break;
			}
			if (result.Child("Logros").Child("¿Perdido? ¡Encontrado!").Exists)
			{
				Debug.Log("Logro ya registrado");
				yield break;
			}
			string userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
			Debug.Log(userId);
			Task dbTarea = databaseReference.Child("Users").Child("Jugador").Child(userId)
				.Child("Logros")
				.Child("¿Perdido? ¡Encontrado!")
				.SetValueAsync("1");
			yield return new WaitUntil(() => dbTarea.IsCompleted);
			Debug.Log("done");
			if (dbTarea.Exception != null)
			{
				Debug.LogWarning($"Failed to register with {dbTarea.Exception}");
			}
			else
			{
				Debug.Log("Exito");
			}
		}
	}
}
