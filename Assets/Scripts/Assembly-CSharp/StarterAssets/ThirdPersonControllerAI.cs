using UnityEngine;
using UnityEngine.AI;

namespace StarterAssets
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CharacterController))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class ThirdPersonControllerAI : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the AI in m/s, controls NavMeshAgent's speed")]
		public float MoveSpeed = 2f;

		[Tooltip("Sprint speed of the AI in m/s, controls NavMeshAgent's speed")]
		public float SprintSpeed = 5.335f;

		[Tooltip("How fast the AI turns to face movement direction")]
		[Range(0f, 0.3f)]
		public float RotationSmoothTime = 0.12f;

		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10f;

		[Space(10f)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;

		[Tooltip("The AI uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15f;

		[Space(10f)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.5f;

		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the AI is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;

		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;

		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.28f;

		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		private float _speed;

		private float _animationBlend;

		private float _targetRotation;

		private float _rotationVelocity;

		private float _verticalVelocity;

		private float _terminalVelocity = 53f;

		private float _jumpTimeoutDelta;

		private float _fallTimeoutDelta;

		private int _animIDSpeed;

		private int _animIDGrounded;

		private int _animIDJump;

		private int _animIDFreeFall;

		private int _animIDMotionSpeed;

		private Animator _animator;

		private CharacterController _controller;

		private const float _threshold = 0.01f;

		[Tooltip("Target destination for Nav Mesh Agent as Transform")]
		public Transform[] wayPoints;

		[Tooltip("If the AI is sprinting or not.")]
		public bool Sprinting;

		[Tooltip("If the AI will start a Jump or not.")]
		public bool Jump;

		public int time;

		public int timeRest;

		private float timeTrans;

		private int nextPoint;

		private int lastPoint;

		public int contador;

		private NavMeshAgent thisAgent;

		private void Awake()
		{
			lastPoint = wayPoints.Length;
		}

		private void Start()
		{
			_animator = GetComponent<Animator>();
			_controller = GetComponent<CharacterController>();
			thisAgent = GetComponent<NavMeshAgent>();
			thisAgent.updateRotation = false;
			if (Sprinting)
			{
				thisAgent.speed = SprintSpeed;
			}
			else
			{
				thisAgent.speed = MoveSpeed;
			}
			AssignAnimationIDs();
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
		}

		private void Update()
		{
			NpcMove();
			JumpAndGravity();
			GroundedCheck();
			if (Sprinting)
			{
				thisAgent.speed = SprintSpeed;
			}
			else
			{
				thisAgent.speed = MoveSpeed;
			}
			if (thisAgent.remainingDistance > thisAgent.stoppingDistance)
			{
				Move(thisAgent.desiredVelocity.normalized, thisAgent.desiredVelocity.magnitude);
			}
			else
			{
				Move(thisAgent.desiredVelocity.normalized, 0f);
			}
		}

		private void NpcMove()
		{
			timeTrans += Time.deltaTime;
			time = Mathf.RoundToInt(timeTrans);
			if (time == timeRest)
			{
				if (contador == lastPoint)
				{
					contador = 0;
				}
				nextPoint = contador;
				thisAgent.isStopped = false;
				thisAgent.SetDestination(wayPoints[nextPoint].position);
				timeTrans = 0f;
				time = 0;
				contador++;
			}
			if (Mathf.RoundToInt(Vector3.Distance(base.transform.position, wayPoints[nextPoint].position)) == 0)
			{
				thisAgent.isStopped = true;
			}
		}

		private void AssignAnimationIDs()
		{
			_animIDSpeed = Animator.StringToHash("Speed");
			_animIDGrounded = Animator.StringToHash("Grounded");
			_animIDJump = Animator.StringToHash("Jump");
			_animIDFreeFall = Animator.StringToHash("FreeFall");
			_animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
		}

		private void GroundedCheck()
		{
			Vector3 position = new Vector3(base.transform.position.x, base.transform.position.y - GroundedOffset, base.transform.position.z);
			Grounded = Physics.CheckSphere(position, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
			_animator.SetBool(_animIDGrounded, Grounded);
		}

		private void Move(Vector3 AgentDestination, float AgentSpeed)
		{
			if (AgentSpeed > 0f)
			{
				float magnitude = new Vector3(_controller.velocity.x, 0f, _controller.velocity.z).magnitude;
				float num = 0.1f;
				if (magnitude < AgentSpeed - num || magnitude > AgentSpeed + num)
				{
					_speed = Mathf.Lerp(magnitude, AgentSpeed, Time.deltaTime * SpeedChangeRate);
					_speed = Mathf.Round(_speed * 1000f) / 1000f;
				}
				else
				{
					_speed = AgentSpeed;
				}
				_animationBlend = Mathf.Lerp(_animationBlend, AgentSpeed, Time.deltaTime * SpeedChangeRate);
				if (_speed != 0f)
				{
					_targetRotation = Mathf.Atan2(AgentDestination.x, AgentDestination.z) * 57.29578f;
					float y = Mathf.SmoothDampAngle(base.transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
					base.transform.rotation = Quaternion.Euler(0f, y, 0f);
				}
				Vector3 vector = Quaternion.Euler(0f, _targetRotation, 0f) * Vector3.forward;
				_controller.Move(vector.normalized * (_speed * Time.deltaTime) + new Vector3(0f, _verticalVelocity, 0f) * Time.deltaTime);
				float value = 1f;
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, value);
			}
			else
			{
				_animationBlend = Mathf.Lerp(_animationBlend, 0f, Time.deltaTime * SpeedChangeRate);
				_animator.SetFloat(_animIDSpeed, _animationBlend);
				_animator.SetFloat(_animIDMotionSpeed, 1f);
			}
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				_fallTimeoutDelta = FallTimeout;
				_animator.SetBool(_animIDJump, value: false);
				_animator.SetBool(_animIDFreeFall, value: false);
				if (_verticalVelocity < 0f)
				{
					_verticalVelocity = -2f;
				}
				if (Jump && _jumpTimeoutDelta <= 0f)
				{
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
					_animator.SetBool(_animIDJump, value: true);
				}
				if (_jumpTimeoutDelta >= 0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				_jumpTimeoutDelta = JumpTimeout;
				if (_fallTimeoutDelta >= 0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				else
				{
					_animator.SetBool(_animIDFreeFall, value: true);
				}
				Jump = false;
			}
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
			Jump = false;
		}

		private void OnDrawGizmosSelected()
		{
			Color color = new Color(0f, 1f, 0f, 0.35f);
			Color color2 = new Color(1f, 0f, 0f, 0.35f);
			if (Grounded)
			{
				Gizmos.color = color;
			}
			else
			{
				Gizmos.color = color2;
			}
			Gizmos.DrawSphere(new Vector3(base.transform.position.x, base.transform.position.y - GroundedOffset, base.transform.position.z), GroundedRadius);
		}
	}
}
