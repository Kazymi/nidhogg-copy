public class PlayerTransition
{
    public PlayerState StateTo { get; }
    public PlayerCondition Condition { get; }

    public PlayerTransition(PlayerState state, PlayerCondition playerCondition)
    {
        StateTo = state;
        Condition = playerCondition;
    }

    public void InitializeCondition()
    {
        Condition.Initialize();
    }

    public void DeInitializeCondition()
    {
        Condition.DeInitialize();
    }
}