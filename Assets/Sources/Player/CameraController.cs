using UnityEngine;
using UnityEngine.InputSystem;

namespace Roblox.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private Transform _cameraTarget;

        private PlayerInputActions _playerInput;
        private float sensitivity = 10.0f;
        private Vector2 currentRotation;

        private bool _cameraHolding = false;
        private bool _cameraTaped = false;

        private void Awake()
        {
            _playerInput = new PlayerInputActions();
            _playerInput.Enable();

            _playerInput.Player.CameraHold.started += StartHoldingCamera;
            _playerInput.Player.CameraHold.canceled += EndHoldingCamera;
            _playerInput.Player.CameraTap.performed += TapCamera;

            _offset = transform.position - _cameraTarget.position;
        }

        private void Update()
        {
            if (_cameraHolding == true || _cameraTaped == true)
            {
                HandleCameraMovement();
            }
            else
            {
                IdleCameraMovement();
            }
        }

        private void StartHoldingCamera(InputAction.CallbackContext _)
        {
            _cameraHolding = true;
        }

        private void EndHoldingCamera(InputAction.CallbackContext _)
        {
            _cameraHolding = false;
        }

        private void TapCamera(InputAction.CallbackContext _)
        {
            _cameraTaped = !_cameraTaped;
        }

        private void HandleCameraMovement()
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            currentRotation.x += mouseX;
            currentRotation.y -= mouseY;
            currentRotation.y = Mathf.Clamp(currentRotation.y, -90f, 90f);

            _cameraTarget.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            transform.position = _cameraTarget.position + _cameraTarget.forward * 5;
            transform.LookAt(_cameraTarget);
        }

        private void IdleCameraMovement()
        {
            _cameraTarget.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            transform.position = _cameraTarget.position + _cameraTarget.forward * 5;
            transform.LookAt(_cameraTarget);
        }
    }
}