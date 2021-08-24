using System;
using UnityEngine;

namespace PlayerState
{
    public class PlayerRollingState : State
    {
        private float _totalTimeCurve;
        private float _currentTimeCurve;
        private float _currentTime;
        private PlayerMovementConfiguration _playerMovementConfiguration;
        private PlayerMovement _playerMovement;
        private MovementActionConfiguration _movementAction;
        private State _nextState;

        public PlayerRollingState(PlayerMovement playerMovement, MovementActionConfiguration movementAction,
            PlayerMovementConfiguration playerMovementConfiguration, State nextState)
        {
            _movementAction = movementAction;
            _nextState = nextState;
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
            _currentTime += Time.deltaTime;
            if (_currentTime > _totalTimeCurve)
            {
                _currentTime = 0;
            }

            if (_currentTime > _movementAction.Time)
            {
                _playerMovement.StateMachine.SetState(_nextState);
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