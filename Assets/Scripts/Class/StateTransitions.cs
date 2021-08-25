using PlayerStates;

public class PlayerStateTransitions
{
    public PlayerState StartState { get; set; }
    public PlayerState EndState { get; set; }
 
    public PlayerStateTransitions(PlayerState startState, PlayerState endState)
    {
        StartState = startState;
        EndState = endState;
    }
}