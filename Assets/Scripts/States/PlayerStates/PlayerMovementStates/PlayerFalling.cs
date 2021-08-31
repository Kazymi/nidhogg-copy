using UnityEngine;

public class PlayerFalling : PlayerState
{
    private readonly IPlayerMovement _playerMovement;
    private readonly PlayerAnimatorController _playerAnimatorController;
    public PlayerFalling(IPlayerMovement playerMovement, PlayerAnimatorController playerAnimatorController)
    {
        _playerMovement = playerMovement;
        _playerAnimatorController = playerAnimatorController;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetAnimationBool(AnimationNameType.Falling, true);
    }

    public override void OnStateExit()
    {
        _playerAnimatorController.SetAnimationBool(AnimationNameType.Falling, false);
    }

    public override void Tick()
    {
       _playerMovement.Move(0.5f);
    }
}