using UnityEngine;

public class PlayerMoveState : PlayerState
{
    private float _currentTimeCurve;
    private readonly float _totalTimeCurve;
    private readonly PlayerMovement _playerMovement;
    private float _jumpTime;

    public PlayerMoveState(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _totalTimeCurve = playerMovement.JumpCurve.keys[playerMovement.JumpCurve.keys.Length - 1].time;
    }

    public override void OnStateEnter()
    {
        _playerMovement.DefaultMovement.Invoke(true);
        _playerMovement.InputHandler.Jump.Action += StartJump;
        _currentTimeCurve = 999;
    }

    public override void OnStateExit()
    {
        _playerMovement.DefaultMovement.Invoke(false);
        _playerMovement.IsJumped = false;
        _playerMovement.InputHandler.Jump.Action -= StartJump;
    }

    public override void Tick()
    {
        _playerMovement.MoveDirection = Vector3.zero;
        var inputVector = _playerMovement.InputHandler.MovementDirection;
        Move(inputVector);
        Jump();
        if (_playerMovement.IsJumped)
        {
            EndJump();
        }
    }

    private void StartJump()
    {
        if (_playerMovement.IsGrounded)
        {
            _playerMovement.IsJumped = true;
            _jumpTime = 1;
            _playerMovement.PlayerAnimatorController.SetTrigger(AnimationNameType.Jump,false);
            _currentTimeCurve = 0;
        }
    }

    private void Jump()
    {
        if (_currentTimeCurve < _totalTimeCurve)
        {
            _playerMovement.MoveDirection +=
                new Vector3(0, _playerMovement.JumpCurve.Evaluate(_currentTimeCurve), 0);
            _currentTimeCurve += Time.deltaTime;
        }
    }

    private void EndJump()
    {
        _jumpTime -= Time.deltaTime;
        if (_jumpTime < 0)
        {
            _playerMovement.IsJumped = false;
        }
    }

    private void Move(int moveDir)
    {
        _playerMovement.PlayerAnimatorController.Update();
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
        _playerMovement.MoveDirection *= _playerMovement.Speed;
    }
}