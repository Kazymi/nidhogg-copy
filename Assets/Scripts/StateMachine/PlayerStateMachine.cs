using UnityEngine;

public class PlayerStateMachine
{
    private PlayerState _currentState;
    public PlayerStateMachine(PlayerState state)
    {
        SetState(state);
    }

    public void Tick()
    {
        var newIndex = IsTransitionsCondition();
        if (newIndex != -1)
        {
            SetState(_currentState.Transitions[newIndex].StateTo);
        }
        else
        {
            _currentState.Tick();
        }
        Debug.Log(_currentState);
    }
    

    public void FixedTick()
    {
        _currentState.FixedTick();
    }

    private int IsTransitionsCondition()
    {
        var currentTransitions = _currentState.Transitions;
        for (var i = 0; i != currentTransitions.Count; i++)
        {
            var condition = currentTransitions[i].Condition;
            if (condition.IsConditionSatisfied())
            {
                return i;
            }
        }

        return -1;
    }

    public void SetState(PlayerState state)
    {
        _currentState?.OnStateExit();
        _currentState?.DeInitializeTransitions();

        _currentState = state;
        _currentState.OnStateEnter();
        _currentState.InitializeTransitions();
    }
}