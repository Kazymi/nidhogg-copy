using States.PlayerStates;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IShieldDeactivator
{
    [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;

    private Rigidbody _rigidbody;
    private PlayerStateMachine _stateMachine;
    public float Speed => playerMovementConfiguration.Speed;
    public AnimationCurve JumpCurve => playerMovementConfiguration.JumpCurve;
    public IInputHandler InputHandler { get; private set; }
    public PlayerAnimatorController PlayerAnimatorController { get; private set; }
    public InputAction ShieldDeactivated { get; set; } = new InputAction();
    public IInventory Inventory { get; private set; }
    public IWeaponDeactivator WeaponDeactivator;
    public bool IsJumped { get; set; }
    public bool IsGrounded { get; private set; }
    public Vector3 MoveDirection { get; set; }

    [Inject]
    private void Construct(PlayerAnimatorController playerAnimatorController, IWeaponDeactivator weaponDeactivator,
        IInputHandler inputHandler, IInventory inventory)
    {
        Inventory = inventory;
        PlayerAnimatorController = playerAnimatorController;
        WeaponDeactivator = weaponDeactivator;
        InputHandler = inputHandler;
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

        _rigidbody.MovePosition(_rigidbody.position + MoveDirection * Time.deltaTime);
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.3f))
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
        playerMoveState.AddTransition(new PlayerTransition(playerShieldState, new ButtonPressedCondition(InputHandler.ShieldButtonDownAction)));
//falling
        playerRollingState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(playerMovementConfiguration.RollingTime)));
//falling
        playerFallingState.AddTransition(new PlayerTransition(playerShieldState,
            new AfterFallCondition(() => IsGrounded && Inventory.IsShieldActivated, 0f)));
        playerFallingState.AddTransition(new PlayerTransition(playerRollingState,
            new AfterFallCondition(() => IsGrounded && Inventory.IsShieldActivated == false,
                playerMovementConfiguration.NeedTimeFallingToRolling)));
        playerFallingState.AddTransition(new PlayerTransition(playerMoveState,
            new AfterFallCondition(() => IsGrounded && Inventory.IsShieldActivated == false, 0f)));
//shield
        playerShieldState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(this)));
        playerShieldState.AddTransition(new PlayerTransition(playerShieldCrashState,
            new ButtonPressedCondition(ShieldDeactivated)));
        playerShieldState.AddTransition(new PlayerTransition(playerMoveState,
            new Condition(() => Inventory.IsShieldActivated == false)));
//shield crash
        playerShieldCrashState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(playerMovementConfiguration.ShieldCrushTime)));


        _stateMachine = new PlayerStateMachine(playerMoveState);
    }
}