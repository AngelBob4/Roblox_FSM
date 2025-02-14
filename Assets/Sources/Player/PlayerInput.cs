using UnityEngine;

namespace Roblox.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerInput : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            _playerMovement.SetAxises(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerMovement.Jump();
            }
        }
    }
}