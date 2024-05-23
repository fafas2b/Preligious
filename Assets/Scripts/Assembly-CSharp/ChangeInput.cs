using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInput : MonoBehaviour
{
	private EventSystem system;

	public Selectable firstInput;

	public Button submitButton;

	private void Start()
	{
		system = EventSystem.current;
		firstInput.Select();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
		{
			Selectable selectable = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
			if (selectable != null)
			{
				selectable.Select();
			}
		}
		else if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable selectable2 = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
			if (selectable2 != null)
			{
				selectable2.Select();
			}
		}
		else if (Input.GetKeyDown(KeyCode.Return))
		{
			submitButton.onClick.Invoke();
			Debug.Log("Button pressed");
		}
	}
}
