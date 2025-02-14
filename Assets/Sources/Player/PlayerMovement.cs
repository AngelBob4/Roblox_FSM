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

        private Vector3 _verticalVelocity = Vector3.zero;
        private Vector3 _horizontalVelocity = Vector3.zero;
        private bool _jumping;
        private MovementType _currentMovementType = MovementType.None;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ResetMovementType();
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

        public void Rotate(Vector3 moveDirection)
        {
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        private void ResetMovementType()
        {
            Vector3 moveDirection = _horizontalVelocity + _verticalVelocity;

            switch (_currentMovementType)
            {
                case MovementType.None:
                    break;

                case MovementType.Running:
                    break;

                case MovementType.Jumping:
                    break;

                default:
                    break;
            }

            if (_characterController.isGrounded)
            {
                if (_jumping)
                {
                    _verticalVelocity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelocity = Vector3.down;
                }

                _characterController.Move(moveDirection * Time.deltaTime);
            }
            else
            {
                _jumping = false;
                Vector3 horizontalVelocity = _horizontalVelocity;
                horizontalVelocity.y = 0;
                _verticalVelocity += Physics.gravity * Time.deltaTime;

                moveDirection = horizontalVelocity + _verticalVelocity;
                _characterController.Move(moveDirection * Time.deltaTime);
            }

            if (_horizontalVelocity != Vector3.zero)
            {
                Rotate(moveDirection);
            }

            _playerAnimation.HandleAnimations(_jumping, _horizontalVelocity);
        }
    }

    public enum MovementType
    {
        None,
        Running,
        Jumping,
    }
}