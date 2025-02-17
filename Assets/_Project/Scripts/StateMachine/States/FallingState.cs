using UnityEngine;

public class FallingState : MovementState
{
    public FallingState(IStateSwitcher stateSwitcher, Player player) : base(stateSwitcher, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayerView.StartJumping();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Player.GroundChecker.IsTouches)
        {
            if (IsHorizontalInputZero())
                StateSwitcher.SwitcherState<IdleState>();
            else
                StateSwitcher.SwitcherState<RunState>();
        }
    }
}