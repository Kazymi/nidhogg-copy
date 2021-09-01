using UnityEngine;

public class PlayerRollingState : PlayerState
{

    private readonly IPlayerMovement _playerMovement;
    private readonly PlayerAnimatorController _playerAnimatorController;

    public PlayerRollingState(PlayerAnimatorController animatorController, IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _playerAnimatorController = animatorController;
    }

    public override void OnStateEnter()
    {
        _playerAnimatorController.SetTrigger(AnimationNameType.Rolling.ToString(), false);
    }


    public override void Tick()
    {
        Move();
    }

    private void Move()
    {
        _playerMovement.Rolling();
    }
}