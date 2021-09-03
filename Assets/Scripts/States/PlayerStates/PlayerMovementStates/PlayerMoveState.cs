using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerMoveState : PlayerState
{

    private readonly IPlayerAnimatorController _playerAnimatorController;
    public PlayerMoveState(IPlayerAnimatorController playerAnimatorController, IPlayerMovement playerMovement) : base(playerMovement)
    {
        _playerAnimatorController = playerAnimatorController;
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
        _playerMovement.Move(1f);
        _playerAnimatorController.UpdateAnimation();
    }
}