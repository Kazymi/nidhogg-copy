using System;
using States.PlayerStates;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;


    private Rigidbody _rigidbody;
    private PlayerStateMachine _stateMachine;
    private IInventory _inventory;
    private bool _isCrouched;

    public float Speed => playerMovementConfiguration.Speed;
    public AnimationCurve JumpCurve => playerMovementConfiguration.JumpCurve;
    public PlayerAnimatorController PlayerAnimatorController { get; private set; }
    public bool IsGrounded { get; private set; }
    public IInputHandler InputHandler { get; private set; }
    public bool IsJumped { get; set; }
    public Vector3 MoveDirection { get; set; }
    public Action<bool> DefaultMovement { get; set; }
    public Rigidbody Rigidbody => _rigidbody;

    [Inject]
    private void Construct(PlayerAnimatorController playerAnimatorController,
        IInputHandler inputHandler, IInventory inventory)
    {
        PlayerAnimatorController = playerAnimatorController;
        InputHandler = inputHandler;
        _inventory = inventory;
    }

    private void OnEnable()
    {
        InputHandler.DownButtonAction.Action += SetCrouch;
    }

    private void OnDisable()
    {
        InputHandler.DownButtonAction.Action -= SetCrouch;
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
        if (IsGrounded == false)
        {
            MoveDirection -= new Vector3(0, playerMovementConfiguration.Gravity, 0);
        }

        _rigidbody.velocity = MoveDirection;
    }

    private void SetCrouch()
    {
        _isCrouched = !_isCrouched;
        PlayerAnimatorController.SetAnimationBool(AnimationNameType.Crouch, _isCrouched);
    }

    private bool GroundCheck()
    {
        RaycastHit ray;
        if (Physics.SphereCast(playerMovementConfiguration.GroundCheckPosition.position,
            playerMovementConfiguration.SphereCastRadius, -transform.up,
            out ray, 0.2f))
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
        var playerShieldCrashState = new ShieldCrashState(this);
//movement
        playerMoveState.AddTransition(new PlayerTransition(playerRollingState,
            new ButtonPressedCondition(InputHandler.Rolling)));
        playerMoveState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(this)));
        playerMoveState.AddTransition(new PlayerTransition(playerShieldState,
            new Condition(() => _inventory.IsShieldActivated)));
//falling
        playerRollingState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(playerMovementConfiguration.RollingTime)));
//falling
        playerFallingState.AddTransition(new PlayerTransition(playerShieldState,
            new AfterFallCondition(() => IsGrounded && _inventory.IsShieldActivated, 0f)));
        playerFallingState.AddTransition(new PlayerTransition(playerRollingState,
            new AfterFallCondition(() => IsGrounded && _inventory.IsShieldActivated == false,
                playerMovementConfiguration.NeedTimeFallingToRolling)));
        playerFallingState.AddTransition(new PlayerTransition(playerMoveState,
            new AfterFallCondition(() => IsGrounded && _inventory.IsShieldActivated == false, 0f)));
//shield
        playerShieldState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(this)));

        playerShieldState.AddTransition(new PlayerTransition(playerShieldCrashState,
            new ButtonPressedCondition(_inventory.ShieldCrash)));

        playerShieldState.AddTransition(new PlayerTransition(playerMoveState,
            new Condition(() => _inventory.IsShieldActivated == false)));
//shield crash
        playerShieldCrashState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(playerMovementConfiguration.ShieldCrushTime)));


        _stateMachine = new PlayerStateMachine(playerMoveState);
    }
}