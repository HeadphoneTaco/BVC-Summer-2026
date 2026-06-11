using UnityEngine;

namespace _Project.Code.Gameplay.Player
{
    /// <summary>
    ///     Drives the Animator on the character mesh based on PlayerController state.
    ///     Animator parameters expected: "Speed" (float), "IsGrounded" (bool).
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        [SerializeField] private PlayerController playerController;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            // Auto-find on parent if not assigned
            if (playerController == null)
                playerController = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
            if (playerController == null) return;

            var speed = playerController.WorldVelocity.magnitude;
            animator.SetFloat(SpeedHash, speed, 0.1f, Time.deltaTime);
            animator.SetBool(IsGroundedHash, playerController.IsGrounded);
        }
    }
}