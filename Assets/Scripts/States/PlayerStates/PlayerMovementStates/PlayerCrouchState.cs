
    public class PlayerCrouchState : PlayerState
    {
        private readonly IPlayerAnimatorController _playerAnimatorController;
        
        public PlayerCrouchState(IPlayerMovement playerMovement, IPlayerAnimatorController playerAnimatorController) : base(playerMovement)
        {
            _playerAnimatorController = playerAnimatorController;
        }
        public override void OnStateEnter()
        {
            _playerMovement.DefaultMovement?.Invoke(true);
            _playerAnimatorController.SetAnimationBool(AnimationNameType.Crouch,true);
        }
        
        public override void OnStateExit()
        {
            _playerMovement.DefaultMovement?.Invoke(false);
            _playerAnimatorController.SetAnimationBool(AnimationNameType.Crouch,false);
        }

        public override void Tick()
        {
            _playerMovement.Move(0.3f);
            base.Tick();
        }
    }
