using System;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private AnimatorConfig animatorConfig;

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
        
        animatorConfig.PlayerAnimator.SetBool(Animator.StringToHash(animationNameType.ToString()), value);
    }

    public void SetTrigger(string animationNameType, bool isInteractable)
    {
        if (_isDead)
        {
            return;
        }

        animatorConfig.PlayerAnimator.applyRootMotion = isInteractable;
        animatorConfig.PlayerAnimator.SetTrigger(animationNameType);
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
            _currentAnimationValue += animatorConfig.SpeedRunAnimation * Time.deltaTime;
        }
        else
        {
            _currentAnimationValue -= animatorConfig.SpeedRunAnimation * Time.deltaTime;
        }

        _currentAnimationValue = Mathf.Clamp01(_currentAnimationValue);

        animatorConfig.PlayerAnimator.SetFloat(Animator.StringToHash(AnimationNameType.Run.ToString()),
            _currentAnimationValue);
    }

    private void OnAnimatorMove()
    {
        if (animatorConfig.PlayerAnimator.applyRootMotion == false)
        {
            return;
        }

        _playerMovement.Rigidbody.drag = 0;
        var deltaPos = animatorConfig.PlayerAnimator.deltaPosition;
        deltaPos.y = 0;
        var velocity = deltaPos / Time.deltaTime;
        _playerMovement.Rigidbody.velocity = velocity;
    }
}