using UnityEngine;

public class ShieldCrashState : PlayerState
{
    private PlayerMovement _playerMovement;

    public ShieldCrashState(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override void OnStateEnter()
    {
        _playerMovement.PlayerAnimatorController.SetTrigger(AnimationNameType.ShieldCrash.ToString(),true);
    }

    public override void Tick()
    {
        _playerMovement.MoveDirection = Vector3.zero;
    }
}