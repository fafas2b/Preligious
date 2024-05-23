using System.Collections;
using System.Globalization;
using StarterAssets;
using UnityEngine;

public class Verify : MonoBehaviour
{
	public GameObject panelLoading;

	private void Awake()
	{
		StartCoroutine(getPosicion());
	}

	private void Start()
	{
		Debug.Log(Object.FindObjectOfType<StarterAssetsInputs>().transform.position.x);
		Debug.Log(PlayerPrefs.GetString("position_x"));
	}

	public IEnumerator getPosicion()
	{
		panelLoading.SetActive(value: true);
		float num = float.Parse(PlayerPrefs.GetString("position_x"), CultureInfo.InvariantCulture.NumberFormat);
		float num2 = float.Parse(PlayerPrefs.GetString("position_y"), CultureInfo.InvariantCulture.NumberFormat);
		float num3 = float.Parse(PlayerPrefs.GetString("position_z"), CultureInfo.InvariantCulture.NumberFormat);
		Vector3 position = default(Vector3);
		position.x = num;
		Debug.Log(num);
		position.y = num2;
		Debug.Log(num2);
		position.z = num3;
		Debug.Log(num3);
		Object.FindObjectOfType<StarterAssetsInputs>().transform.position = position;
		Debug.Log("Posicion Actualizada");
		yield return new WaitForSeconds(2f);
		panelLoading.SetActive(value: false);
	}
}
