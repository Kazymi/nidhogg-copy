using System;
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
            _playerMovement.MoveDirection *=_playerMovementConfiguration.ShieldSpeed;
        }
    }