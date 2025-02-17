using UnityEngine;

public class MovementState : IState
{
    protected IStateSwitcher StateSwitcher;
    protected Player Player;

    public MovementState(IStateSwitcher stateSwitcher, Player player)
    {
        StateSwitcher = stateSwitcher;
        Player = player;
    }

    public virtual void Enter()
    {
        Debug.Log(GetType());
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnJumpKeyPressed();

        Vector3 direction = ConvertInputToCameraSpace(ReadHorizontalInput());

        Move(direction);
        Rotation(direction);
    }

    protected bool IsHorizontalInputZero() => ReadHorizontalInput() == Vector2.zero;

    protected void OnJumpKeyPressed()
    {
        if (Player.GroundChecker.IsTouches)
            StateSwitcher.SwitcherState<JumpState>();
    }

    private void Move(Vector3 direction)
    {
        if (IsHorizontalInputZero() == false)
            Player.transform.Translate(direction * (Player.PlayerConfig.Speed * Time.deltaTime), Space.World);
    }

    private void Rotation(Vector3 direction)
    {
        if (IsHorizontalInputZero() == false)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Player.transform.rotation = Quaternion.Slerp(
                Player.transform.rotation,
                targetRotation,
                Time.deltaTime * Player.PlayerConfig.SpeedRotation
            );
        }
    }

    private Vector2 ReadHorizontalInput() =>
        Player.InputSystem.Gamepley.Movement.ReadValue<Vector2>();

    private Vector3 ConvertInputToCameraSpace(Vector2 input)
    {
        Vector3 cameraForward = Player.CameraRotateAround.CameraDirection;
        cameraForward.y = 0; 
        cameraForward.Normalize();

        Vector3 cameraRight = Vector3.Cross(Vector3.up, cameraForward).normalized;

        return (cameraForward * input.y + cameraRight * input.x).normalized;
    }
}
