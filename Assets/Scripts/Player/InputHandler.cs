using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour,IMotionVector
{
    [SerializeField] private KeyCode leftButton;
    [SerializeField] private KeyCode rightButton;
    [SerializeField] private KeyCode jumpButton;
 
    private Vector3 _moveDirection;
    
    public Vector3 GetMoveDirection()
    {
        _moveDirection = Vector3.zero;
        if (Input.GetKey(rightButton))
        {
            _moveDirection.x = 1;
        }
        else
        {
            if (Input.GetKey(leftButton))
            {
                _moveDirection.x = -1;
            }
        }

        if (Input.GetKey(jumpButton))
        {
            _moveDirection.y = 1;
        }

        return _moveDirection;
    }
}
