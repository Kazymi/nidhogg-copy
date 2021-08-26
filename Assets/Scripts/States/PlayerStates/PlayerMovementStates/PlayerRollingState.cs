using System;
using UnityEngine;

    public class PlayerRollingState : PlayerState
    {
        private PlayerMovementConfiguration _playerMovementConfiguration;
        private PlayerMovement _playerMovement;

        public PlayerRollingState(PlayerMovement playerMovement,
            PlayerMovementConfiguration playerMovementConfiguration)
        {
            _playerMovement = playerMovement;
            _playerMovementConfiguration = playerMovementConfiguration;
        }

        public override void OnStateEnter()
        {
            _playerMovement.PlayerAnimatorController.SetTrigger(AnimationNameType.Rolling);
        }


        public override void Tick()
        {
            _playerMovement.MoveDirection = Vector3.zero;

            Move();
        }

        private void Move()
        {
            _playerMovement.MoveDirection = new Vector3(
                _playerMovementConfiguration.RollingSpeed * -_playerMovement.transform.forward.z,
                0f, 0f);
        }
    }
