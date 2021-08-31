using UnityEngine;

public class PlayerShieldState : PlayerState
{
    private PlayerAnimatorController _playerAnimatorController;
    private IPlayerMovement _playerMovement;

    public PlayerShieldState(IPlayerMovement playerMovement, PlayerAnimatorController playerAnimatorController)
    {
        _playerAnimatorController = playerAnimatorController;
        _playerMovement = playerMovement;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetAnimationBool(AnimationNameType.Shield, true);
    }

    public override void OnStateExit()
    {
        _playerAnimatorController.SetAnimationBool(AnimationNameType.Shield, false);
    }

    public override void Tick()
    {
        Move();
    }
    
    private void Move()
    {
        _playerMovement.ShieldMove();
    }
}