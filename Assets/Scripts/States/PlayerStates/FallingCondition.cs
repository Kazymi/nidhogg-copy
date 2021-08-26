using System;
using UnityEngine;

public class FallingCondition : PlayerCondition
{
    private PlayerMovement _playerMovement;
    private float _currentFallingTime;
    private float _toFallingTime;
   
    public FallingCondition(PlayerMovement playerMovement, float fallingTime)
    {
        _playerMovement = playerMovement;
        _toFallingTime = fallingTime;
    }

    public override bool IsConditionSatisfied()
    {
        if (_playerMovement.IsGrounded == false)
        {
            _currentFallingTime += Time.deltaTime;
            if (_currentFallingTime >= _toFallingTime)
            {
                return true;
            }
        }
        else
        {
            _currentFallingTime = 0;
        }
        return false;
    }
}