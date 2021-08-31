
    public class PlayerCrouchState : PlayerState
    {
        private IPlayerMovement _playerMovement;
        private PlayerAnimatorController _playerAnimatorController;

        public PlayerCrouchState(PlayerAnimatorController animatorController, IPlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
            _playerAnimatorController = animatorController;
        }
        public override void OnStateEnter()
        {
            _playerMovement.DefaultMovement.Invoke(true);
            _playerAnimatorController.SetAnimationBool(AnimationNameType.Crouch,true);
        }
        
        public override void OnStateExit()
        {
            _playerMovement.DefaultMovement.Invoke(false);
            _playerAnimatorController.SetAnimationBool(AnimationNameType.Crouch,false);
        }

        public override void Tick()
        {
            _playerMovement.Move(0.3f);
        }
    }
