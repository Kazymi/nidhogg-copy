using UnityEngine;

public class PlayerRollingState : PlayerState
{
    private readonly IPlayerAnimatorController _playerAnimatorController;

    public PlayerRollingState(IPlayerAnimatorController animatorController, IPlayerMovement playerMovement) : base(playerMovement)
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