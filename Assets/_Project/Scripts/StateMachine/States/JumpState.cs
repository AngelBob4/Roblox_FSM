using UnityEngine;

public class JumpState : MovementState
{
    public JumpState(IStateSwitcher stateSwitcher, Player player) : base(stateSwitcher, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (Player.GroundChecker.IsTouches)
        {
            Player.Rigidbody.AddForce(Vector3.up * Player.PlayerConfig.JumpForce, ForceMode.Impulse);
            StateSwitcher.SwitcherState<FallingState>();
        }
    }
}
