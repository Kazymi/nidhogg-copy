using UnityEngine;

public class PlayerFalling : PlayerState
{
    // TODO: can be made read only
    private PlayerMovement _playerMovement;

    public PlayerFalling(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override void OnStateEnter()
    {
        _playerMovement.PlayerAnimatorController.SetAnimationBool(AnimationNameType.Falling, true);
    }

    public override void OnStateExit()
    {
        _playerMovement.PlayerAnimatorController.SetAnimationBool(AnimationNameType.Falling, false);
    }

    public override void Tick()
    {
        _playerMovement.MoveDirection = Vector3.zero;
        var inputVector = _playerMovement.InputHandler.MovementDirection;
        Move(inputVector);
    }

    // TODO: duplicated code
    private void Move(int moveDir)
    {
        if (moveDir > 0)
        {
            _playerMovement.transform.rotation = new Quaternion(0, 180, 0, 0);
            _playerMovement.MoveDirection = new Vector3(-moveDir, 0, 0);
        }
        else
        {
            if (moveDir < 0)
            {
                _playerMovement.transform.rotation = new Quaternion(0, 0, 0, 0);
                _playerMovement.MoveDirection = new Vector3(moveDir, 0, 0);
            }
            else
            {
                return;
            }
        }

        _playerMovement.MoveDirection = _playerMovement.transform.TransformDirection(_playerMovement.MoveDirection);
        _playerMovement.MoveDirection *= _playerMovement.Speed / 2;
    }
}