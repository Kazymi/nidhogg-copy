using UnityEngine;

public class FallingCondition : PlayerCondition
{
    private readonly IPlayerMovement _playerMovement;
    
    public FallingCondition(IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override bool IsConditionSatisfied()
    {
        return (_playerMovement.IsGrounded == false && _playerMovement.IsJumped == false);
    }
}