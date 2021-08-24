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
        _action -= ToNextStateEvent;
    }

    
    public void AddTransition(PlayerState state, Func<bool> func)
    {
        Transitions.Add(new PlayerTransition
        {
            Condition = func,
            StateTo = state
        });
    }
    
    public void AddTransition(PlayerState state, Action action)
    {
        _actions.Add(state,action);
    }

    private void ToNextStateEvent()
    {
       
    }
}