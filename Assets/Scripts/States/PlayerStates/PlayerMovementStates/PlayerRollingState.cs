using System;
using UnityEngine;

namespace PlayerStates
{
    public class PlayerRollingState : PlayerState
    {
        private float _totalTimeCurve;
        private float _currentTimeCurve;
        private float _currentTime;
        private PlayerMovementConfiguration _playerMovementConfiguration;
        private PlayerMovement _playerMovement;

        public PlayerRollingState(PlayerMovement playerMovement,
            PlayerMovementConfiguration playerMovementConfiguration)
        {
            _playerMovement = playerMovement;
            _playerMovementConfiguration = playerMovementConfiguration;
            var curve = playerMovementConfiguration.RollingCurve;
            _totalTimeCurve = curve.keys[curve.keys.Length - 1].time;
        }

        public override void OnStateEnter()
        {
            _currentTimeCurve = 0;
            _currentTime = 0;
        }


        public override void Tick()
        {
            _playerMovement.MoveDirection = Vector3.zero;
            _currentTimeCurve += Time.deltaTime;
            if (_currentTime > _totalTimeCurve)
            {
                _currentTime = 0;
            }

            Move();
        }

        private void Move()
        {
            _playerMovement.MoveDirection = new Vector3(_playerMovementConfiguration.Speed,
                _playerMovementConfiguration.RollingCurve.Evaluate(_currentTimeCurve), 0f);

            _playerMovement.MoveDirection = _playerMovement.transform.TransformDirection(_playerMovement.MoveDirection);
            _playerMovement.MoveDirection *= _playerMovement.Speed;
        }
    }
}