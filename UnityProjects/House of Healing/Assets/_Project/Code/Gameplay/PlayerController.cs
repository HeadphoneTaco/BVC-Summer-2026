using UnityEngine;

namespace _Project.Code.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 5f;

        [Header("Mouse Look")]
        [SerializeField] private float mouseSensitivity = 200f;
    
        [SerializeField] private Rigidbody rb;
        private Camera _playerCamera;
    
        private float _mouseX;
        private float _mouseY;
    
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            _playerCamera = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    
        private void Update()
        {
            CalculateMouseAndCam();
            CalculateMovement();
        }

        private void CalculateMouseAndCam()
        {
            _mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            _mouseY += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0f, _mouseX, 0f);
            //clamp the y value between 90 and -90 so you cannot break your players neck
            var clampedY = Mathf.Clamp(_mouseY, -90f, 90f);
            _playerCamera.transform.localRotation = Quaternion.Euler(-clampedY, 0f, 0f);
        }

        private void CalculateMovement()
        {
            float moveX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
            float moveZ = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
            Vector3 moveDirection = (transform.right * moveX + transform.forward * moveZ).normalized;
            rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);
        }

    }
}
