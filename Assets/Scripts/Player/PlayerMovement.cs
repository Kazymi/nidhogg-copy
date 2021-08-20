using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float gravity;

    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private IMotionVector _motionVector;

    private float _currentTimeCurve;
    private float _totalTimeCurve;
    private bool _isJump;

    private void Awake()
    {
        _totalTimeCurve = jumpCurve.keys[jumpCurve.keys.Length - 1].time;
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var inputVector = _motionVector.GetMoveDirection();

        Move(inputVector);
        Jump(inputVector);
        _moveDirection.y -= gravity;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void Jump(Vector3 inputVector)
    {
        if (_isJump == false)
        {
            if (inputVector.y > 0 && _characterController.isGrounded)
            {
                _isJump = true;
            }
        }
        else
        {
            _moveDirection.y += jumpCurve.Evaluate(_currentTimeCurve);
            _currentTimeCurve += Time.deltaTime;
            if (_currentTimeCurve >= _totalTimeCurve)
            {
                _currentTimeCurve = 0;
                _isJump = false;
            }
        }
    }

    public void Initialize(IMotionVector motionVector)
    {
        _motionVector = motionVector;
    }

    private void Move(Vector3 inputVector)
    {
        if (inputVector.x > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _moveDirection = new Vector3(-inputVector.x, 0, 0);
        }
        else
        {
            if (inputVector.x < 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                _moveDirection = new Vector3(inputVector.x, 0, 0);
            }
            else
            {
                return;
            }
        }

        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed;
    }
}