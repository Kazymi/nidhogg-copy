using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;
    [SerializeField] private MovementActionConfiguration rollingAction;

    private PlayerAnimatorController _playerAnimatorController;
    private Rigidbody _rigidbody;
    private PlayerStateMachine _stateMachine;
    private Vector3 _moveDirection;
    private IInputHandler _inputHandler;
    private bool _isShieldActivated;

    public float Speed => playerMovementConfiguration.Speed;
    public AnimationCurve JumpCurve => playerMovementConfiguration.JumpCurve;
    public IInputHandler InputHandler => _inputHandler;

    public PlayerAnimatorController PlayerAnimatorController => _playerAnimatorController;

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

        _inputHandler.ShieldButtonDownAction.Action += () => _isShieldActivated = !_isShieldActivated;
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
        var playerFallingState = new PlayerFalling(this);
        var playerShieldState = new PlayerShieldState(this, playerMovementConfiguration);
        
        playerMoveState.AddTransition(new PlayerTransition(playerRollingState, new ButtonPressedCondition(_inputHandler.Rolling)));
        playerMoveState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(this,playerMovementConfiguration.ToFallingTime)));
        playerMoveState.AddTransition(new PlayerTransition(playerShieldState, new Condition(() => _isShieldActivated)));
        
        playerRollingState.AddTransition(new PlayerTransition(playerMoveState, new TimerCondition(rollingAction.Time)));

        playerFallingState.AddTransition(new PlayerTransition(playerRollingState, new Condition(() => IsGrounded && _isShieldActivated == false)));
        playerFallingState.AddTransition(new PlayerTransition(playerShieldState, new Condition(() => IsGrounded && _isShieldActivated)));
        
        playerShieldState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(this,playerMovementConfiguration.ToFallingTime)));
        playerShieldState.AddTransition(new PlayerTransition(playerMoveState, new Condition(() => _isShieldActivated == false)));
        
        
        _stateMachine = new PlayerStateMachine(playerMoveState);
    }

    [Inject]
    private void Construct(PlayerAnimatorController playerAnimatorController)
    {
        _playerAnimatorController = playerAnimatorController;
    }
}