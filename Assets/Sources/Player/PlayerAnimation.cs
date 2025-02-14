using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void HandleAnimations(bool isJumping, Vector3 moveDirection)
    {
        bool isRunning;

        if (moveDirection != Vector3.zero)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        _animator.SetBool(Constants.IsRunning, isRunning);
        _animator.SetBool(Constants.IsJumping, isJumping);
    }
}