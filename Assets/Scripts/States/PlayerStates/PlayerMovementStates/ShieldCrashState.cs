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
        _playerMovement.PlayerAnimatorController.SetTrigger(AnimationNameType.ShieldCrash);
    }

    public override void Tick()
    {
        _playerMovement.MoveDirection = Vector3.zero;
        Move();
    }

    private void Move()
    {
        _playerMovement.MoveDirection += new Vector3(_playerMovement.transform.forward.z, 0, 0);
        _playerMovement.MoveDirection = _playerMovement.transform.TransformDirection(_playerMovement.MoveDirection);
    }
}