using System;
using System.Collections;
using System.Collections.Generic;
using PlayerState;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float gravity;

    private StateMachine _stateMachine;
    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private IMotionVector _motionVector;

    public IMotionVector MotionVector => _motionVector;
    public float Speed => speed;
    public AnimationCurve JumpCurve => jumpCurve;
    public CharacterController CharacterController => _characterController;

    public Vector3 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        StateInitialize();
    }

    private void Update()
    {
        _stateMachine.Tick();
        _moveDirection.y -= gravity;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    public void Initialize(IMotionVector iMotionVector)
    {
        _motionVector = iMotionVector;
    }
    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(this);

        _stateMachine = new StateMachine(playerMoveState);
    }

}