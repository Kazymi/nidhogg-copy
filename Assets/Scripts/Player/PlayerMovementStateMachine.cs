using System;
using System.Collections.Generic;
using States.PlayerStates;
using UnityEngine;
using Zenject;

public class PlayerMovementStateMachine : MonoBehaviour
{
    private PlayerStateMachine _stateMachine;
    private IShieldSystem _inventory;
    private IInputHandler _inputHandler;
    private IPlayerMovement _playerMovement;
    private IPlayerAnimatorController _playerAnimatorController;
    private IPlayerWeaponSystem _playerWeaponManager;

    [Inject]
    private void Construct(IPlayerAnimatorController playerAnimatorController,
        IInputHandler inputHandler, IShieldSystem inventory, IPlayerHealth playerHealth, IPlayerMovement playerMovement,
        IPlayerRespawnSystem playerRespawnSystem, IPlayerWeaponSystem playerWeaponManager)
    {
        _playerWeaponManager = playerWeaponManager;
        playerRespawnSystem.RespawnAction += Respawn;
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
        _stateMachine = null;
    }

    private void Update()
    {
        _stateMachine?.Tick();
    }


    private void Respawn()
    {
        StateInitialize();
    }

    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(_playerAnimatorController, _playerMovement);
        var playerRollingState = new PlayerRollingState(_playerAnimatorController, _playerMovement);
        var playerFallingState = new PlayerFalling(_playerMovement, _playerAnimatorController);
        var playerShieldState = new PlayerShieldState(_playerMovement);
        var playerShieldCrashState = new ShieldCrashState(_playerAnimatorController, _playerMovement);
        var playerCrouchState = new PlayerCrouchState(_playerMovement, _playerAnimatorController);
        var playerJumpState = new PlayerJumpState(_playerMovement);

        var playerSwordAttack = new PlayerTriggerAnimationState(_playerMovement, _playerAnimatorController, false,
            AnimationNameType.SwordAttack);
        var playerSwordFastAttack = new PlayerTriggerAnimationState(_playerMovement, _playerAnimatorController, true,
            AnimationNameType.SwordFastAttack);
        var playerSwordRollingAttack = new PlayerTriggerAnimationState(_playerMovement, _playerAnimatorController, true,
            AnimationNameType.SwordRollingAttack); 
        //movement
        playerMoveState.AddTransition(new PlayerTransition(playerCrouchState,
            new ButtonPressedCondition(_inputHandler.DownButtonAction)));
        playerMoveState.AddTransition(new PlayerTransition(playerRollingState,
            new ButtonPressedCondition(_inputHandler.Rolling)));
        playerMoveState.AddTransition(new PlayerTransition(playerFallingState, new FallingCondition(_playerMovement)));
        playerMoveState.AddTransition(new PlayerTransition(playerShieldState,
            new Condition(() => _inventory.IsShieldActivated)));
        playerMoveState.AddTransition(new PlayerTransition(playerJumpState,
            new ButtonPressedCondition(_inputHandler.Jump)));

        playerMoveState.AddTransition(new PlayerTransition(playerSwordAttack, new AnimationCondition(
            () => _playerWeaponManager.IsCurrentWeaponMelee,
            new ButtonPressedCondition(_inputHandler.Fire)
        ))); 
        //rolling
        playerRollingState.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(_playerMovement.PlayerMovementConfiguration.RollingTime)));
        playerRollingState.AddTransition(new PlayerTransition(playerSwordRollingAttack, new AnimationCondition(
            () => _playerWeaponManager.IsCurrentWeaponMelee,
            new ButtonPressedCondition(_inputHandler.Fire)
        ))); 
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
        //jump state
        playerJumpState.AddTransition(new PlayerTransition(playerMoveState,
            new Condition(() => _playerMovement.IsJumped == false && _playerMovement.IsGrounded)));
        playerJumpState.AddTransition(new PlayerTransition(playerFallingState,
            new Condition(() => _playerMovement.IsJumped == false && _playerMovement.IsGrounded == false))); 
        //crouch state
        playerCrouchState.AddTransition(new PlayerTransition(playerShieldCrashState,
            new ButtonPressedCondition(_inventory.ShieldCrash)));
        playerCrouchState.AddTransition(new PlayerTransition(playerMoveState,
            new ButtonPressedCondition(_inputHandler.DownButtonAction)));
        playerCrouchState.AddTransition(new PlayerTransition(playerMoveState,
            new ButtonPressedCondition(_inputHandler.Rolling))); 
        //sword state
        playerSwordAttack.AddTransition(new PlayerTransition(playerSwordFastAttack, new AnimationCondition(
            () => _playerWeaponManager.IsCurrentWeaponMelee,
            new ButtonPressedCondition(_inputHandler.FastAttack)
        )));
        playerSwordAttack.AddTransition(new PlayerTransition(playerMoveState,
            new TimerCondition(1f))); 
        //sword fast attack
        playerSwordFastAttack.AddTransition(new PlayerTransition(playerMoveState, new TimerCondition(1.20f)));
        //sword rolling attack
        playerSwordRollingAttack.AddTransition(new PlayerTransition(playerMoveState, new TimerCondition(1.20f)));
        _stateMachine = new PlayerStateMachine(playerMoveState);
    }
}