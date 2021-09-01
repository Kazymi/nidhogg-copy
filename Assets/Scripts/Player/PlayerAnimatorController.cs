using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speedRunAnimation;

    private IInputHandler _inputHandler;
    private IPlayerMovement _playerMovement;
    private float _currentAnimationValue;
    private bool _isDead;

    [Inject]
    private void Construct(IInputHandler inputHandler, IPlayerMovement iPlayerMovement, IPlayerHealth playerHealth, PlayerRespawnSystem respawnSystem)
    {
        playerHealth.PlayerDeath += (DamageTarget damageTarget) =>
        {
            SetTrigger(AnimationNameType.Death.ToString() + damageTarget, true);
            _isDead = true;
        };
        respawnSystem.RespawnAction += Respawn;
        _playerMovement = iPlayerMovement;
        _inputHandler = inputHandler;
    }

    public void SetAnimationBool(AnimationNameType animationNameType, bool value)
    {
        if (_isDead)
        {
            return;
        }
        
        playerAnimator.SetBool(Animator.StringToHash(animationNameType.ToString()), value);
    }

    public void SetTrigger(string animationNameType, bool isInteractable)
    {
        if (_isDead)
        {
            return;
        }

        playerAnimator.applyRootMotion = isInteractable;
        playerAnimator.SetTrigger(animationNameType);
    }
    
    public void UpdateAnimation()
    {
        UpdateAnimationState();
    }

    private void Respawn()
    {
        _isDead = false;
    }
    private void UpdateAnimationState()
    {
        var moveDirection = _inputHandler.MovementDirection;
        if (moveDirection != 0)
        {
            _currentAnimationValue += speedRunAnimation * Time.deltaTime;
        }
        else
        {
            _currentAnimationValue -= speedRunAnimation * Time.deltaTime;
        }

        _currentAnimationValue = Mathf.Clamp01(_currentAnimationValue);

        playerAnimator.SetFloat(Animator.StringToHash(AnimationNameType.Run.ToString()),
            _currentAnimationValue);
    }

    private void OnAnimatorMove()
    {
        if (playerAnimator.applyRootMotion == false)
        {
            return;
        }

        _playerMovement.Rigidbody.drag = 0;
        var deltaPos = playerAnimator.deltaPosition;
        deltaPos.y = 0;
        var velocity = deltaPos / Time.deltaTime;
        _playerMovement.Rigidbody.velocity = velocity;
    }
}