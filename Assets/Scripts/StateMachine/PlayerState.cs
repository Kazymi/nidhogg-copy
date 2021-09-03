using System.Collections.Generic;

public abstract class PlayerState : State
{
    protected IPlayerMovement _playerMovement;
    protected PlayerState(IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override void Tick()
    {
        _playerMovement.MoveUpdate();
    }
}