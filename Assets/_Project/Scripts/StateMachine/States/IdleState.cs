public class IdleState : MovementState
{
    public IdleState(IStateSwitcher stateSwitcher, Player player)
        : base(stateSwitcher, player)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.PlayerView.StartIdle();
    }

    public override void Exit()
    {
        base.Exit();

        Player.PlayerView.StopIdle();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero() == false)
            StateSwitcher.SwitcherState<RunState>();
    }
}
