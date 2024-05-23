using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;

		public Vector2 look;

		public bool jump;

		public bool sprint;

		private PlayerInput playerInput;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;

		public bool cursorInputForLook = true;

		public GameObject[] botones;

		public GameObject[] paneles;

		public void Awake()
		{
			playerInput = GetComponent<PlayerInput>();
			paneles = GameObject.FindGameObjectsWithTag("panel");
			Debug.Log(paneles.Length);
			botones = GameObject.FindGameObjectsWithTag("boton");
			Debug.Log(botones.Length);
			for (int i = 0; i < botones.Length; i++)
			{
				botones[i].SetActive(value: false);
				Debug.Log(botones[i].name);
			}
			for (int j = 0; j < paneles.Length; j++)
			{
				Debug.Log(paneles[j].name);
				paneles[j].SetActive(value: false);
			}
		}

		public void Update()
		{
			for (int i = 0; i < botones.Length; i++)
			{
				if (botones[i].activeSelf)
				{
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.Confined;
					playerInput.actions.FindActionMap("UI").Enable();
					playerInput.actions.FindActionMap("Player").Disable();
					break;
				}
				if (!botones[i].activeSelf)
				{
					playerInput.actions.FindActionMap("UI").Disable();
					playerInput.actions.FindActionMap("Player").Enable();
					Cursor.visible = false;
					Cursor.lockState = CursorLockMode.Confined;
				}
			}
		}

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
		}

		private void SetCursorState(bool newState)
		{
		}
	}
}
