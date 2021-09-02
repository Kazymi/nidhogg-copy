using System.Collections.Generic;

public abstract class PlayerState
{
    protected IPlayerMovement _playerMovement;
    public List<PlayerTransition> Transitions { get; } = new List<PlayerTransition>();

    public PlayerState(IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }
    public virtual void Tick()
    {
        _playerMovement.MoveUpdate();
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