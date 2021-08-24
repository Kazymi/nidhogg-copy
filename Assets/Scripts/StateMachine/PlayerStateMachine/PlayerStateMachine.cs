using UnityEngine;

public class PlayerStateMachine
{
    private PlayerState _currentState;
    private float _currentTimer;
    private bool _isActiveTimer;
    private PlayerState _nextState;

    public PlayerStateMachine(PlayerState state)
    {
        SetState(state);
    }

    public void Tick()
    {
        var newindex = IsTransitionsCondition();
        if (newindex != -1)
        {
            SetState(_currentState.Transitions[newindex].StateTo);
        }
        else
        {
            _currentState.Tick();
        }

        if (_isActiveTimer)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer < 0)
            {
                SetState(_nextState);
            }
        }
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
        _isActiveTimer = false;
        _currentState?.OnStateExit();
        _currentState?.DeInitializeTransitions();

        _currentState = state;
        _currentState.OnStateEnter();
        _currentState.InitializeTransitions();
    }

    public void SetState(PlayerState state, PlayerState nextState, float timer)
    {
        _currentState?.OnStateExit();
        _currentState = state;
        _isActiveTimer = true;
        _nextState = nextState;
        _currentTimer = timer;
        _currentState.OnStateEnter();
    }
}