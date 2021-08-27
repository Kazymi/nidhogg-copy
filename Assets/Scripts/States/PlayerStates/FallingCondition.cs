using UnityEngine;

public class FallingCondition : PlayerCondition
{
    private PlayerMovement _playerMovement;
    private float _currentFallingTime;

    public FallingCondition(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override bool IsConditionSatisfied()
    {
        if (_playerMovement.IsGrounded == false && _playerMovement.IsJumped == false)
        {
            _currentFallingTime += Time.deltaTime;
            return true;
        }
        else
        {
            _currentFallingTime = 0;
        }

        return false;
    }
}