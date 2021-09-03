public class PlayerMoveState : PlayerState
{

    private readonly IPlayerAnimatorController _playerAnimatorController;
    private readonly IInputHandler _inputHandler;
    private const float _speed = 1f;
    public PlayerMoveState(IPlayerAnimatorController playerAnimatorController, IPlayerMovement playerMovement) : base(playerMovement)
    {
        _playerAnimatorController = playerAnimatorController;
    }

    public override void OnStateEnter()
    {
        _playerMovement.DefaultMovement?.Invoke(true);
    }

    public override void OnStateExit()
    {
        _playerMovement.DefaultMovement?.Invoke(false);
    }

    public override void Tick()
    {
        Move();
        base.Tick();
    }

    private void Move()
    {
        _playerMovement.Move(_speed);
        _playerAnimatorController.UpdateAnimation();
    }
}
