using UnityEngine;

public class PlayerShieldState : PlayerState
{

    private const float _speed = 0.7f;
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
        _playerMovement.Move(_speed);
    }
}