public class PlayerRollingState : PlayerState
{
    private readonly PlayerAnimatorController _playerAnimatorController;

    public PlayerRollingState(PlayerAnimatorController animatorController, IPlayerMovement playerMovement) : base(playerMovement)
    {
        _playerAnimatorController = animatorController;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetTrigger(AnimationNameType.Rolling.ToString(), false);
    }


    public override void Tick()
    {
        Move();
        base.Tick();
    }

    private void Move()
    {
        _playerMovement.Rolling();
    }
}