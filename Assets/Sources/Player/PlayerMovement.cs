using UnityEngine;

namespace Roblox.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private PlayerAnimation _playerAnimation;

        private float _moveSpeed = 6f;
        private float _jumpSpeed = 7f;
        private float _rotationSpeed = 100f;

        private Vector3 moveDirection;
        private Vector3 _verticalVelocity = Vector3.zero;
        private Vector3 _horizontalVelocity = Vector3.zero;
        private bool _jumping;
        private MovementState _currentMovementState = MovementState.None;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ResetState();
            Move();
        }

        public void SetAxises(float horizontalValue, float verticalValue)
        {
            Vector3 forward = Vector3.ProjectOnPlane(_camera.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized;
            _horizontalVelocity = (forward * verticalValue + right * horizontalValue) * _moveSpeed;
        }

        public void Jump()
        {
            _jumping = true;
        }

        private void Move()
        {
            moveDirection = _horizontalVelocity + _verticalVelocity;

            switch (_currentMovementState)
            {
                case MovementState.None:
                    break;

                case MovementState.Running:
                    if (_jumping)
                        _verticalVelocity = Vector3.up * _jumpSpeed;
                    else
                        _verticalVelocity = Vector3.down;

                    _characterController.Move(moveDirection * Time.deltaTime);
                    _playerAnimation.HandleAnimations(_jumping, _horizontalVelocity);
                    if (_horizontalVelocity != Vector3.zero)
                        Rotate(moveDirection);
                    break;

                case MovementState.Jumping:
                    _jumping = false;
                    Vector3 horizontalVelocity = _horizontalVelocity;
                    horizontalVelocity.y = 0;
                    _verticalVelocity += Physics.gravity * Time.deltaTime;
                    moveDirection = horizontalVelocity + _verticalVelocity;
                    _characterController.Move(moveDirection * Time.deltaTime);
                    _playerAnimation.HandleAnimations(_jumping, _horizontalVelocity);
                    if (_horizontalVelocity != Vector3.zero)
                        Rotate(moveDirection);
                    break;

                default:
                    break;
            }
        }

        private void Rotate(Vector3 moveDirection)
        {
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        private void ResetState()
        {
            if (_horizontalVelocity == Vector3.zero)
            {
                _currentMovementState = MovementState.None;
            }

            if (_characterController.isGrounded)
                _currentMovementState = MovementState.Running;
            else
                _currentMovementState = MovementState.Jumping;
        }
    }

    public enum MovementState
    {
        None,
        Running,
        Jumping,
    }
}