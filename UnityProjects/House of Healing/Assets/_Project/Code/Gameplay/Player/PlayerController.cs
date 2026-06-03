using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Code.Gameplay.Player
{
    /// <summary>
    ///     Third-person character controller.
    ///     Requires a CharacterController component on the same GameObject.
    ///     Wire the InputActionReferences to the Player action map in InputSystem_Actions.
    ///     Point CameraTransform at the Main Camera (or a Cinemachine brain).
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float walkSpeed = 4f;

        [SerializeField] private float sprintSpeed = 7f;
        [SerializeField] private float rotationSpeed = 12f;
        [SerializeField] private float gravity = -18f;
        [SerializeField] private float jumpHeight = 1f;

        [Header("Input Actions")] [SerializeField]
        private InputActionReference moveAction;

        [SerializeField] private InputActionReference lookAction;
        [SerializeField] private InputActionReference jumpAction;
        [SerializeField] private InputActionReference sprintAction;

        [Header("References")]
        [Tooltip("Assign the Main Camera transform so movement is camera-relative.")]
        [SerializeField]
        private Transform cameraTransform;

        private CharacterController controller;
        private Vector3 velocity;
        private bool wasGrounded;

        // Exposed for PlayerAnimator
        public Vector3 WorldVelocity { get; private set; }
        public bool IsGrounded => controller.isGrounded;
        public bool IsSprinting => sprintAction != null && sprintAction.action.IsPressed();

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleGravityAndJump();
            HandleMovement();
        }

        private void OnEnable()
        {
            moveAction?.action.Enable();
            jumpAction?.action.Enable();
            sprintAction?.action.Enable();
        }

        private void OnDisable()
        {
            moveAction?.action.Disable();
            jumpAction?.action.Disable();
            sprintAction?.action.Disable();
        }

        private void HandleMovement()
        {
            var input = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;

            if (input.sqrMagnitude < 0.01f)
            {
                WorldVelocity = Vector3.zero;
                return;
            }

            var cam = cameraTransform != null ? cameraTransform : Camera.main?.transform;
            var forward = cam != null ? Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized : Vector3.forward;
            var right = cam != null ? Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized : Vector3.right;

            var moveDir = (forward * input.y + right * input.x).normalized;
            var speed = IsSprinting ? sprintSpeed : walkSpeed;

            // Rotate character toward movement direction
            var targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

            WorldVelocity = moveDir * speed;
            controller.Move(WorldVelocity * Time.deltaTime);
        }

        private void HandleGravityAndJump()
        {
            if (controller.isGrounded && velocity.y < 0f)
                velocity.y = -2f; // keep grounded

            var jumpPressed = jumpAction != null && jumpAction.action.WasPressedThisFrame();
            if (jumpPressed && controller.isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}