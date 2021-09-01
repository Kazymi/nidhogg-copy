using System;

public class Condition : PlayerCondition
{
    private readonly Func<bool> _func;

    public Condition(Func<bool> func)
    {
        _func = func;
    }

    public override bool IsConditionSatisfied()
    {
        return _func.Invoke();
    }
}