

public class PlayerStateTransitions
{
    public PlayerState StartState { get;  }
    public PlayerState EndState { get; }
 
    public PlayerStateTransitions(PlayerState startState, PlayerState endState)
    {
        StartState = startState;
        EndState = endState;
    }
}