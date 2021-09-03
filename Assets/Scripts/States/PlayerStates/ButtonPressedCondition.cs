public class ButtonPressedCondition : PlayerCondition
{
    private bool _isSatisfied;
    private readonly InputAction _inputAction;

    public ButtonPressedCondition(InputAction inputAction)
    {
        _inputAction = inputAction;
    }

    public override bool IsConditionSatisfied()
    {
        return _isSatisfied;
    }

    public override void Initialize()
    {
        _inputAction.Action += ButtonPressed;
    }

    public override void DeInitialize()
    {
        _inputAction.Action -= ButtonPressed;
        _isSatisfied = false;
    }

    private void ButtonPressed()
    {
        _isSatisfied = true;
    }
}