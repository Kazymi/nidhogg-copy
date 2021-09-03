using UnityEngine;

public class PlayerFalling : PlayerState
{
    private readonly IPlayerAnimatorController _playerAnimatorController;
    public PlayerFalling(IPlayerMovement playerMovement, IPlayerAnimatorController playerAnimatorController) : base(playerMovement)
    {
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
       base.Tick();
    }
}