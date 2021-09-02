public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(IPlayerMovement playerMovement) : base(playerMovement)
    {
        
    }

    public override void OnStateEnter()
    {
       _playerMovement.StartJump();
    }

    public override void OnStateExit()
    {
        _playerMovement.IsJumped = false;
    }

    public override void Tick()
    {
        _playerMovement.Move(0.5f);
        base.Tick();
    }
}