public class PlayerTriggerAnimationState : PlayerState
{
    private PlayerAnimatorController _playerAnimatorController;
    private AnimationNameType _animationNameType;
    private bool _interactable;
    
    public PlayerTriggerAnimationState(IPlayerMovement playerMovement,
        PlayerAnimatorController playerAnimatorController,bool interactable, AnimationNameType animationNameType) : base(playerMovement)
    {
        _interactable = interactable;
        _animationNameType = animationNameType;
        _playerAnimatorController = playerAnimatorController;
    }

    public override void Tick()
    {
        if (_interactable == false)
        {
            base.Tick();
        }
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetTrigger(_animationNameType.ToString(),_interactable);
    }
}