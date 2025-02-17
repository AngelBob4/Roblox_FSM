using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerView : MonoBehaviour
{
    private const string IsIdle = "IsIdle";
    private const string IsRun = "IsRun";
    private const string Jump = "Jump";

    private Animator _animator;

    public void Initialize() =>
        _animator = transform.GetComponent<Animator>();

    public void StartIdle() => _animator.SetBool(IsIdle, true);
    public void StopIdle() => _animator.SetBool(IsIdle, false);

    public void StartRuning() => _animator.SetBool(IsRun, true);
    public void StopRuning() => _animator.SetBool(IsRun, false);

    public void StartJumping() => _animator.Play(Jump);
}
