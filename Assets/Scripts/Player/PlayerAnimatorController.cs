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

    public float AnimationValue
    {
        set => _currentAnimationValue = value;
    }

    [Inject]
    private void Construct(IInputHandler inputHandler, IPlayerMovement iPlayerMovement, IPlayerHealth playerHealth)
    {
        playerHealth.PlayerDeath += (DamageTarget damageTarget) =>
        {
            SetTrigger(AnimationNameType.Death.ToString() + damageTarget, true);
            _isDead = true;
        };

        _playerMovement = iPlayerMovement;
        ;
        _inputHandler = inputHandler;
        _inputHandler.DownButtonAction.Action += () => SetAnimationBool(AnimationNameType.Crouch,
            !animatorConfig.PlayerAnimator.GetBool(Animator.StringToHash(AnimationNameType.Crouch.ToString())));
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

    public void Update()
    {
        UpdateAnimationState();
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