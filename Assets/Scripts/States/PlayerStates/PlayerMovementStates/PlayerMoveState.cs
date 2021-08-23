using UnityEngine;

namespace PlayerState
{
    public class PlayerMoveState : State
    {
        private float _currentTimeCurve;
        private float _totalTimeCurve;
        private bool _isJump;
        private PlayerMovement _playerMovement;

        public PlayerMoveState(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            _totalTimeCurve = playerMovement.JumpCurve.keys[playerMovement.JumpCurve.keys.Length - 1].time;
        }

        public override void Tick()
        {
            _playerMovement.MoveDirection = Vector3.zero;
            // var inputVector = InputHandler.GetMoveDirection();
            // Move(inputVector);
            // Jump(inputVector);
        }

        private void Jump(Vector3 inputVector)
        {
            if (_isJump == false)
            {
                if (inputVector.y > 0 && _playerMovement.CharacterController.isGrounded)
                {
                    _isJump = true;
                }
            }
            else
            {
                _playerMovement.MoveDirection += new Vector3(0, _playerMovement.JumpCurve.Evaluate(_currentTimeCurve), 0);
                _currentTimeCurve += Time.deltaTime;
                if (_currentTimeCurve >= _totalTimeCurve)
                {
                    _currentTimeCurve = 0;
                    _isJump = false;
                }
            }
        }

        private void Move(Vector3 inputVector)
        {
            if (inputVector.x > 0)
            {
                _playerMovement.transform.rotation = new Quaternion(0, 180, 0, 0);
                _playerMovement.MoveDirection = new Vector3(-inputVector.x, 0, 0);
            }
            else
            {
                if (inputVector.x < 0)
                {
                    _playerMovement.transform.rotation = new Quaternion(0, 0, 0, 0);
                    _playerMovement.MoveDirection = new Vector3(inputVector.x, 0, 0);
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
}