using System;
using UnityEngine;
using Zenject;

public class PlayerMovementStateMachine : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    private IInventory _inventory;
    private IInputHandler _inputHandler;
    private IPlayerMovement _playerMovement;
    private PlayerAnimatorController _playerAnimatorController;

    [Inject]
    private void Construct(PlayerAnimatorController playerAnimatorController,
        IInputHandler inputHandler, IInventory inventory, IPlayerHealth playerHealth, IPlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        playerHealth.PlayerDeath += PlayerDeath;
        _playerAnimatorController = playerAnimatorController;
        _inputHandler = inputHandler;
        _inventory = inventory;
    }

    private void Awake()
    {
        StateInitialize();
    }

    private void PlayerDeath(DamageTarget damageTarget)
    {
        Destroy(this);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }


    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(_inputHandler, _playerAnimatorController, _playerMovement);
        var playerRollingState = new PlayerRollingState(_playerAnimatorController, _playerMovement);
        var playerFallingState = new PlayerFalling(_playerMovement, _playerAnimatorController);
        var playerShieldState = new PlayerShieldState(_playerMovement, _playerAnimatorController);
        var playerShieldCrashState = new ShieldCrashState(_playerAnimatorController);
        var playerCrouchState = new PlayerCrouchState(_playerAnimatorController, _playerMovement);

//movement
        playerMoveState.AddTransition(new PlayerTransition(playerCrouchState,
            new ButtonPressedCondition(_inputHandler.DownButtonAction)));
        playerMoveState.AddTransition(new PlayerTransition(playerRollingState,
            new ButtonPressedCondition(_inputHandler.Rolling)));
        playerMoveState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(_playerMovement)));
        playerMoveState.AddTransition(new PlayerTransition(playerShieldState,
            new Condition(() => _inventory.IsShieldActivated)));
//falling
        playerRollingState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(_playerMovement.PlayerMovementConfiguration.RollingTime)));
//falling
        playerFallingState.AddTransition(new PlayerTransition(playerShieldState,
            new AfterFallCondition(() => _playerMovement.IsGrounded && _inventory.IsShieldActivated, 0f)));
        playerFallingState.AddTransition(new PlayerTransition(playerRollingState,
            new AfterFallCondition(() => _playerMovement.IsGrounded && _inventory.IsShieldActivated == false,
                _playerMovement.PlayerMovementConfiguration.NeedTimeFallingToRolling)));
        playerFallingState.AddTransition(new PlayerTransition(playerMoveState,
            new AfterFallCondition(() => _playerMovement.IsGrounded && _inventory.IsShieldActivated == false, 0f)));
//shield
        playerShieldState.AddTransition(new PlayerTransition(playerFallingState,
            new FallingCondition(_playerMovement)));
        playerShieldState.AddTransition(new PlayerTransition(playerShieldCrashState,
            new ButtonPressedCondition(_inventory.ShieldCrash)));
        playerShieldState.AddTransition(new PlayerTransition(playerMoveState,
            new Condition(() => _inventory.IsShieldActivated == false)));
//shield crash
        playerShieldCrashState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(_playerMovement.PlayerMovementConfiguration.ShieldCrushTime)));
//crouch state
        playerCrouchState.AddTransition(new PlayerTransition(playerMoveState,
            new ButtonPressedCondition(_inputHandler.DownButtonAction)));
        playerCrouchState.AddTransition(new PlayerTransition(playerMoveState, new ButtonPressedCondition(_inputHandler.Rolling)));
        _stateMachine = new PlayerStateMachine(playerMoveState);
    }
}