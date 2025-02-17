using System;
using System.Collections.Generic;

public class PlayerStateMachine : IStateSwitcher
{
    private Dictionary<Type, MovementState> _states;
    private MovementState _currentState;

    public PlayerStateMachine(Player player)
    {
        _states = new Dictionary<Type, MovementState>
        {
            { typeof(IdleState), new IdleState(this, player) },
            { typeof(RunState), new RunState(this, player) },
            { typeof(JumpState), new JumpState(this, player) },
            { typeof(FallingState), new FallingState(this, player) }
        };

        _currentState = _states[typeof(IdleState)];
        _currentState.Enter();
    }

    public void SwitcherState<T>() where T : MovementState
    {
        MovementState state = _states[typeof(T)];

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void Update() => _currentState.Update();
}
