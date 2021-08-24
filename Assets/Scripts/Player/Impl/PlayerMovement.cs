using System;
using PlayerStates;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;
    [SerializeField] private MovementActionConfiguration rollingAction;

    private Rigidbody _rigidbody;
    private PlayerStateMachine _stateMachine;
    private Vector3 _moveDirection;
    private IInputHandler _inputHandler;
    // private PlayerMoveState _playerMoveState;
    // private PlayerRollingState _playerRollingState;

    public float Speed => playerMovementConfiguration.Speed;
    public AnimationCurve JumpCurve => playerMovementConfiguration.JumpCurve;
    public IInputHandler InputHandler => _inputHandler;
    public bool IsGrounded;

    public Vector3 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        StateInitialize();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsGrounded = GroundCheck();

        _stateMachine.Tick();

        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(this);
        var playerRollingState = new PlayerRollingState(this, playerMovementConfiguration);
        playerMoveState.AddTransition(new PlayerTransition(playerRollingState, new ButtonPressedCondition(_inputHandler.Rolling)));


        _stateMachine = new PlayerStateMachine(playerMoveState);
    }
}