using UnityEngine;

public class ShieldCrashState : PlayerState
{
    private readonly PlayerAnimatorController _playerAnimatorController;

    public ShieldCrashState(PlayerAnimatorController playerAnimatorController)
    {
        _playerAnimatorController = playerAnimatorController;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetTrigger(AnimationNameType.ShieldCrash.ToString(),true);
    }
}