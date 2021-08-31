using System;
using System.Collections.Generic;

// TODO: if this sm never used, it can be deleted. 
public abstract class State
{
    public List<Transition> Transitions { get; } = new List<Transition>();

    public virtual void Tick()
    {
    }
    
    public virtual void FixedTick()
    {
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void OnStateExit()
    {
    }

    public void AddTransition(State state, Func<bool> func)
    {
        Transitions.Add(new Transition
        {
            Condition = func,
            StateTo = state
        });
    }
}