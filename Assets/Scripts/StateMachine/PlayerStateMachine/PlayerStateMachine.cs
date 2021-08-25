using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private PlayerState _currentState;
    private Dictionary<PlayerStateTransitions, float> playerStates = new Dictionary<PlayerStateTransitions, float>();
    
    private float _currentTimer;
    private PlayerState _nextState;
    private bool _isTimerActivated;
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

        
        if (_isTimerActivated)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer < 0)
            {
                SetState(_nextState);
            }
        }
        else
        {
            foreach (var states in playerStates)
            {
                if (_currentState == states.Key.StartState)
                {
                    _nextState = states.Key.EndState;
                    _currentTimer = states.Value;
                    _isTimerActivated = true;
                    break;
                }
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
        _isTimerActivated = false;
        _currentState?.OnStateExit();
        _currentState?.DeInitializeTransitions();

        _currentState = state;
        _currentState.OnStateEnter();
        _currentState.InitializeTransitions();
    }

    public void SetInterimState(PlayerState startState, PlayerState endState, float timer)
    {
        var stateTransition = new PlayerStateTransitions(startState, endState);
        playerStates.Add(stateTransition,timer);
    }
}