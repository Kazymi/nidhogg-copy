using UnityEngine;

public class PlayerShieldState : PlayerState
{
    private PlayerMovementConfiguration _playerMovementConfiguration;
    private PlayerMovement _playerMovement;

    public PlayerShieldState(PlayerMovement playerMovement,
        PlayerMovementConfiguration playerMovementConfiguration)
    {
        _playerMovement = playerMovement;
        _playerMovementConfiguration = playerMovementConfiguration;
    }

    public override void OnStateEnter()
    {
        _playerMovement.PlayerAnimatorController.SetAnimationBool(AnimationNameType.Shield, true);
    }

    public override void OnStateExit()
    {
        _playerMovement.PlayerAnimatorController.SetAnimationBool(AnimationNameType.Shield, false);
    }

    public override void Tick()
    {
        _playerMovement.MoveDirection = Vector3.zero;
        var inputVector = _playerMovement.InputHandler.MovementDirection;
        Move(inputVector);
    }

    private void Move(int moveDir)
    {
        if (moveDir == (int)_playerMovement.transform.forward.z)
        {
            _playerMovement.MoveDirection = new Vector3(moveDir,0,0);
            _playerMovement.MoveDirection *= _playerMovementConfiguration.ShieldSpeed;
        }
        else
        {
            _playerMovement.PlayerAnimatorController.AnimationValue = 0;
        }
        _playerMovement.PlayerAnimatorController.Update();
    }
}