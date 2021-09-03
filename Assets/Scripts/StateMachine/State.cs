using System.Collections.Generic;

public abstract class State
{
    protected IPlayerMovement _playerMovement;
    public List<PlayerTransition> Transitions { get; } = new List<PlayerTransition>();
    
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
}