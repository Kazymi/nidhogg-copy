public class PlayerTriggerAnimationState : PlayerState
{
    private readonly IPlayerAnimatorController _playerAnimatorController;
    private readonly AnimationNameType _animationNameType;
    private readonly bool _interactable;

    public PlayerTriggerAnimationState(IPlayerMovement playerMovement,
        IPlayerAnimatorController playerAnimatorController, bool interactable, AnimationNameType animationNameType) :
        base(playerMovement)
    {
        _interactable = interactable;
        _animationNameType = animationNameType;
        _playerAnimatorController = playerAnimatorController;
    }

    public override void Tick()
    {
        if (_interactable == false)
        {
            _playerMovement.Move(1);
            base.Tick();
        }
    }

    public override void OnStateEnter()
    {
        _playerMovement.DefaultMovement?.Invoke(true);
        _playerAnimatorController.SetTrigger(_animationNameType.ToString(), _interactable);
    }

    public override void OnStateExit()
    {
        _playerMovement.DefaultMovement?.Invoke(false);
    }
}
