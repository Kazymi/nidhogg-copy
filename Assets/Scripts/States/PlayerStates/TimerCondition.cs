using System;
using UnityEngine;

public class TimerCondition : PlayerCondition
{
    private float _startTime;
    private InputAction _inputAction;
    private float _timer;

    public TimerCondition(float timer)
    {
        _timer = timer;
    }

    public override bool IsConditionSatisfied()
    {
        return (Time.time - _startTime > _timer);
    }

    public override void Initialize()
    {
        _startTime = Time.time;
    }
}