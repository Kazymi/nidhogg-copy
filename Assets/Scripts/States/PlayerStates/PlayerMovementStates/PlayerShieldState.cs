using UnityEngine;

public class PlayerShieldState : PlayerState
{
    private IPlayerMovement _playerMovement;

    public PlayerShieldState(IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
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
    }
    
    private void Move()
    {
        _playerMovement.Move(0.9f);
    }
}