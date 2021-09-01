public abstract class PlayerCondition
{
    public abstract bool IsConditionSatisfied();

    public virtual void Initialize()
    {
    }

    public virtual void DeInitialize()
    {
    }
}