using System;

public class ButtonPressedCondition : PlayerCondition
{
    private bool _isSatisfied;
    private Action _action;

    public ButtonPressedCondition(Action action)
    {
        _action = action;
    }

    public override bool IsConditionSatisfied()
    {
        return _isSatisfied;
    }

    public override void Initialize()
    {
        _action += ButtonPressed;
    }

    public override void DeInitialize()
    {
        _action -= ButtonPressed;
        _isSatisfied = false;
    }

    private void ButtonPressed()
    {
        _isSatisfied = true;
    }
}