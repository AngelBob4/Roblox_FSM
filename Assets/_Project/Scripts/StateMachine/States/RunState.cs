public class RunState : MovementState
{
    public RunState(IStateSwitcher stateSwitcher, Player player) : base(stateSwitcher, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayerView.StartRuning();
    }

    public override void Exit()
    {
        base.Exit();

        Player.PlayerView.StopRuning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitcherState<IdleState>();
    }
}
