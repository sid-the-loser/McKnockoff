// Code by: Sidharth S (aka SidTheLoser)

using General;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Properties")] [SerializeField]
        private float jumpHeight = 4.5f;

        [SerializeField] private float walkingSpeed = 10f;
        [SerializeField] private float sprintingSpeed = 16f;
        [SerializeField] private float mouseSensitivity = 2f;
        [SerializeField] private float gravity = -9.8f;
        [SerializeField] private float lerpSpeed = 10f;

        [Header("Ground Checks")] [SerializeField]
        private Transform groundCheck;

        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;

        [Header("Misc")] [SerializeField] private GameObject headObject;
        [SerializeField] private bool alwaysSprint;

        private CharacterController _controller;

        private float _headRotationX;
        private float _headRotationY;
        private float _currentSpeed;

        private Vector3 _velocity;
        private Vector3 _moveVector;

        private bool _isGrounded;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();

            _currentSpeed = walkingSpeed;
            var localEulerAngles = headObject.transform.localEulerAngles;
            _headRotationX = localEulerAngles.x;
            _headRotationY = localEulerAngles.y;
            InputManager.ToggleMouseCapture(true);
        }

        private void Update()
        {
            if (!GlobalVariables.GamePaused)
            {
                #region Mouse Logic

                _headRotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;
                _headRotationY += Input.GetAxis("Mouse X") * mouseSensitivity;

                _headRotationX = Mathf.Clamp(_headRotationX, -89f, 89f);

                headObject.transform.localEulerAngles = new Vector3(_headRotationX, 0, 0);
                transform.localEulerAngles = new Vector3(0, _headRotationY, 0);

                #endregion

                #region Ground Checks

                _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                #endregion

                #region Movement Logic

                if (!alwaysSprint) _currentSpeed = InputManager.SprintButtonPressed ? sprintingSpeed : walkingSpeed;
                else _currentSpeed = sprintingSpeed;

                if (_velocity.y < 0 && _isGrounded) _velocity.y = -2f;

                var transform1 = transform;

                var newMoveVector = transform1.right * InputManager.MovementVector.x +
                                    transform1.forward * InputManager.MovementVector.z;

                _moveVector = Vector3.Lerp(_moveVector, newMoveVector, lerpSpeed * Time.deltaTime);

                _controller.Move(_moveVector * (_currentSpeed * Time.deltaTime));

                if (InputManager.JumpButtonPressed && _isGrounded)
                    _velocity.y = Mathf.Sqrt(jumpHeight * 2f * Mathf.Abs(gravity));

                _velocity.y += gravity * Time.deltaTime;

                _controller.Move(_velocity * Time.deltaTime);

                #endregion
            }
        }
    }
}