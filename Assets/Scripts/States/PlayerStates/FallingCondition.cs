using UnityEngine;

public class FallingCondition : PlayerCondition
{
    private PlayerMovement _playerMovement;
    // TODO: timer here is useless
    private float _currentFallingTime;

    public FallingCondition(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override bool IsConditionSatisfied()
    {
        // TODO: what if player jumped of the ledge? He will not transition in falling state
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