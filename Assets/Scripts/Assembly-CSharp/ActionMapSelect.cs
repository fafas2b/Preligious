using UnityEngine;
using UnityEngine.InputSystem;

public class ActionMapSelect : MonoBehaviour
{
	private PlayerInput playerInput;

	public void ChangeActionMap()
	{
		playerInput.actions.FindActionMap("Player").Enable();
		Cursor.visible = false;
	}
}
