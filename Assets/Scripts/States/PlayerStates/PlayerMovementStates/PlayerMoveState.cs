using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerMoveState : PlayerState
{

    private readonly PlayerAnimatorController _playerAnimatorController;
    private readonly IInputHandler _inputHandler;
    private readonly IPlayerMovement _playerMovement;
    public PlayerMoveState(IInputHandler inputHandler, PlayerAnimatorController playerAnimatorController, IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _playerAnimatorController = playerAnimatorController;
        _inputHandler = inputHandler;
    }

    public override void OnStateEnter()
    {
        _playerMovement.DefaultMovement?.Invoke(true);
        _inputHandler.Jump.Action += _playerMovement.StartJump;
    }

    public override void OnStateExit()
    {
        _playerMovement.DefaultMovement?.Invoke(false);
        _playerMovement.IsJumped = false;
        _inputHandler.Jump.Action -= _playerMovement.StartJump;
    }

    public override void Tick()
    {
        Move();
    }

    private void Move()
    {
        _playerMovement.Move(1f);
        _playerAnimatorController.UpdateAnimation();
    }
}