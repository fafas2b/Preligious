using UnityEngine;

public class LogicaPapeleta : MonoBehaviour
{
	public LogicaNPC logicaNPC;

	public GameObject botonOk;

	private void Start()
	{
	}

	private void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			logicaNPC.numObjetivos--;
			logicaNPC.textoObjetivo.text = "Encuentra todos los trozos\nRestantes: " + logicaNPC.numObjetivos;
			if (logicaNPC.numObjetivos <= 0)
			{
				logicaNPC.textoObjetivo.text = "Haz recogido todos los trozos de papeleta \nRegresa con David para devolversela";
				logicaNPC.botonOK.SetActive(value: true);
			}
			base.transform.parent.gameObject.SetActive(value: false);
		}
	}
}
