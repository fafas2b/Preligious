using UnityEngine; // Importa la biblioteca principal de Unity
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem; // Importa la biblioteca del nuevo sistema de entrada si está habilitado
#endif

namespace StarterAssets
{
    // Asegura que el componente CharacterController esté agregado al GameObject
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM 
    // Asegura que el componente PlayerInput esté agregado si el sistema de entrada está habilitado
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        // Variable para controlar el estado de pausa
        private bool isPaused = false;

        [Header("Jugador")] // Encabezado en el inspector para organización
        public float MoveSpeed = 2.0f; // Velocidad de movimiento del personaje
        public float SprintSpeed = 5.335f; // Velocidad de esprint del personaje
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f; // Tiempo de suavizado para la rotación
        public float SpeedChangeRate = 10.0f; // Velocidad de cambio entre velocidades
        public AudioClip LandingAudioClip; // Clip de audio para el aterrizaje
        public AudioClip[] FootstepAudioClips; // Clips de audio para los pasos
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f; // Volumen del audio de los pasos

        [Space(10)] // Espacio en el inspector
        public float JumpHeight = 1f; // Altura del salto
        public float Gravity = -15.0f; // Fuerza de gravedad aplicada al personaje

        [Space(10)] // Otro espacio en el inspector
        public float JumpTimeout = 0.50f; // Tiempo de espera antes de poder saltar de nuevo
        public float FallTimeout = 0.15f; // Tiempo de espera antes de considerar al personaje en caída libre

        [Header("Jugador en Suelo")]
        public bool Grounded = true; // Indica si el personaje está en el suelo
        public float GroundedOffset = -0.14f; // Desplazamiento del punto de verificación del suelo
        public float GroundedRadius = 0.28f; // Radio de la esfera de verificación del suelo
        public LayerMask GroundLayers; // Capas consideradas como suelo

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget; // Objetivo de la cámara de Cinemachine
        public float TopClamp = 70.0f; // Límite superior de la rotación de la cámara
        public float BottomClamp = -30.0f; // Límite inferior de la rotación de la cámara
        public float CameraAngleOverride = 0.0f; // Ángulo de cámara personalizado
        public bool LockCameraPosition = false; // Bloquear la posición de la cámara

        private float _cinemachineTargetYaw; // Yaw (giro horizontal) de la cámara objetivo
        private float _cinemachineTargetPitch; // Pitch (inclinación vertical) de la cámara objetivo
        private float _speed; // Velocidad actual del personaje
        private float _animationBlend; // Mezcla de animación para la velocidad
        private float _targetRotation = 0.0f; // Rotación objetivo del personaje
        private float _rotationVelocity; // Velocidad de rotación
        private float _verticalVelocity; // Velocidad vertical del personaje
        private float _terminalVelocity = 53.0f; // Velocidad terminal para la caída
        private float _jumpTimeoutDelta; // Contador para el tiempo de espera del salto
        private float _fallTimeoutDelta; // Contador para el tiempo de espera de la caída
        private int _animIDSpeed; // ID de la animación de velocidad
        private int _animIDGrounded; // ID de la animación de estar en el suelo
        private int _animIDJump; // ID de la animación de salto
        private int _animIDFreeFall; // ID de la animación de caída libre
        private int _animIDMotionSpeed; // ID de la animación de velocidad de movimiento

#if ENABLE_INPUT_SYSTEM 
        private PlayerInput _playerInput; // Referencia al componente PlayerInput
#endif
        private Animator _animator; // Referencia al componente Animator
        private CharacterController _controller; // Referencia al componente CharacterController
        private StarterAssetsInputs _input; // Referencia a los inputs personalizados
        private GameObject _mainCamera; // Referencia a la cámara principal
        private const float _threshold = 0.01f; // Umbral para detectar movimiento mínimo
        private bool _hasAnimator; // Indica si el objeto tiene un componente Animator

        // Variables públicas de estamina
        public float Estamina; // Nivel de estamina del personaje
        public bool TenerEstamina; // Indica si el personaje tiene suficiente estamina para esprintar

        [Header("Canvas del jugador")]
        public Canvas EstaminaMenu; // Primer Canvas
        public Canvas MenuPausa; // Segundo Canvas
        public Canvas PantallaMuerto; // Segundo Canvas

        // Variables para el cooldown de salto
        [Header("Cooldown de Salto")]
        public float JumpCooldown = 3.0f; // Duración del cooldown para el salto
        private float jumpCooldownTimer = 0f; // Contador para el cooldown de salto
        private bool canJump = true; // Indica si el personaje puede saltar

        // Propiedad para determinar si el dispositivo actual es un ratón
        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }

        private void Awake()
        {
            // Busca y asigna la cámara principal si no está ya asignada
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            // Inicializa valores y referencias
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            _hasAnimator = TryGetComponent(out _animator); // Comprueba si tiene un componente Animator
            _controller = GetComponent<CharacterController>(); // Asigna el componente CharacterController
            _input = GetComponent<StarterAssetsInputs>(); // Asigna el componente StarterAssetsInputs
#if ENABLE_INPUT_SYSTEM 
            _playerInput = GetComponent<PlayerInput>(); // Asigna el componente PlayerInput si está habilitado
#else
            Debug.LogError("Faltan dependencias del paquete Starter Assets. Utilice Tools/Starter Assets/Reinstall Dependencies para solucionarlo");
#endif
            AssignAnimationIDs(); // Asigna los IDs de las animaciones
            _jumpTimeoutDelta = JumpTimeout; // Inicializa el contador de tiempo de espera del salto
            _fallTimeoutDelta = FallTimeout; // Inicializa el contador de tiempo de espera de la caída
        }

        private void Update()
        {
            // Si el juego está en pausa, no actualiza el personaje
            if (isPaused) return;

            // Actualiza las referencias necesarias
            _hasAnimator = TryGetComponent(out _animator);
            JumpAndGravity(); // Maneja el salto y la gravedad
            GroundedCheck(); // Verifica si el personaje está en el suelo
            Move(); // Maneja el movimiento del personaje

            // Actualiza el contador del cooldown de salto
            if (!canJump)
            {
                jumpCooldownTimer -= Time.deltaTime;
                if (jumpCooldownTimer <= 0f)
                {
                    canJump = true;
                }
            }
        }

        public void SetPauseState(bool pause)
        {
            // Método para pausar o despausar el juego
            isPaused = pause;
        }

        private void LateUpdate()
        {
            // Actualiza la rotación de la cámara
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            // Asigna los IDs de las animaciones basándose en los nombres de los parámetros del Animator
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // Calcula la posición de la esfera para verificar el suelo
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

            // Actualiza el Animator con el estado de estar en el suelo
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // Controla la rotación de la cámara basada en la entrada del usuario
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // Restringe la rotación de la cámara dentro de los límites establecidos
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Aplica la rotación calculada al objetivo de la cámara
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            // Calcula la velocidad objetivo basándose en la entrada del usuario y el estado de estamina
            float targetSpeed = _input.sprint && TenerEstamina ? SprintSpeed : MoveSpeed;
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // Calcula la velocidad horizontal actual
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // Ajusta la velocidad del personaje de manera gradual
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // Ajusta la mezcla de animación para la velocidad
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // Calcula la dirección de entrada normalizada
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // Calcula la rotación del personaje basada en la dirección de entrada y la cámara
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            // Calcula la dirección objetivo para el movimiento
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // Mueve el personaje usando el CharacterController
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // Actualiza las animaciones con la velocidad y la magnitud de la entrada
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            // Maneja el salto y la gravedad del personaje
            if (Grounded)
            {
                // Reinicia el contador de caída
                _fallTimeoutDelta = FallTimeout;

                // Actualiza las animaciones de salto y caída libre
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // Ajusta la velocidad vertical para un aterrizaje suave
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Maneja el inicio del salto si se presiona el botón de salto y se puede saltar
                if (_input.jump && _jumpTimeoutDelta <= 0.0f && canJump)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    canJump = false; // Desactiva la capacidad de saltar
                    jumpCooldownTimer = JumpCooldown; // Inicia el contador del cooldown

                    // Actualiza la animación de salto
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // Actualiza el contador de tiempo de espera del salto
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // Reinicia el contador de tiempo de espera del salto
                _jumpTimeoutDelta = JumpTimeout;

                // Maneja la caída libre
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // Desactiva el estado de salto en la entrada
                _input.jump = false;
            }

            // Aplica la gravedad al personaje
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            // Restricción del ángulo para evitar rotaciones extremas
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            // Dibuja una esfera en la posición de verificación del suelo para visualización en el editor
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            // Reproduce un sonido de paso si la animación es válida y los clips de audio están disponibles
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            // Reproduce un sonido de aterrizaje si la animación es válida
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }

        public bool GetEstamina(){
            return TenerEstamina;
        }
    }
}
