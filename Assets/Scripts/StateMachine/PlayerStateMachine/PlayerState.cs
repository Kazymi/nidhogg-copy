using System;
using System.Collections.Generic;

public abstract class PlayerState
{
    private Dictionary<PlayerState, Action> _actions = new Dictionary<PlayerState, Action>();
    private State _nextState;
    public List<PlayerTransition> Transitions { get; } = new List<PlayerTransition>();

    public virtual void Tick()
    {
    }

    public virtual void FixedTick()
    {
    }

    public virtual void OnStateEnter()
    {
        foreach (var variable in _actions)
        {
            var key = variable.Value;
        }
    }

    public virtual void OnStateExit()
    {
    }

    public virtual void InitializeTransitions()
    {
        foreach (var transition in Transitions)
        {
            transition.InitializeCondition();
        }
    }

    public virtual void DeInitializeTransitions()
    {
        foreach (var transition in Transitions)
        {
            transition.DeInitializeCondition();
        }
    }

    public void AddTransition(PlayerTransition transition)
    {
        Transitions.Add(transition);
    }

    private void ToNextStateEvent()
    {
    }
}