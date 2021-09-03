using UnityEngine;

public class PlayerShieldState : PlayerState
{
    public PlayerShieldState(IPlayerMovement playerMovement) : base(playerMovement)
    {
        
    }

    public override void OnStateEnter()
    {
        _playerMovement.DefaultMovement?.Invoke(true);
    }

    public override void OnStateExit()
    {
        _playerMovement.DefaultMovement?.Invoke(false);
    }

    public override void Tick()
    {
        Move();
        base.Tick();
    }
    
    private void Move()
    {
        _playerMovement.Move(0.7f);
    }
}