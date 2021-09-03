using UnityEngine;

public class ShieldCrashState : PlayerState
{
    private readonly IPlayerAnimatorController _playerAnimatorController;

    public ShieldCrashState(IPlayerAnimatorController playerAnimatorController, IPlayerMovement playerMovement) : base(playerMovement)
    {
        _playerAnimatorController = playerAnimatorController;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetTrigger(AnimationNameType.ShieldCrash.ToString(),true);
    }
}