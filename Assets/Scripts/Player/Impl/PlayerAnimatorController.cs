using UnityEngine;
using Zenject;

public class PlayerAnimatorController
{
    private AnimatorConfig _animatorConfig;
    private IInputHandler _inputHandler;
    private float _currentAnimationValue;

    public float AnimationValue
    {
        set => _currentAnimationValue = value;
    }

    public PlayerAnimatorController(AnimatorConfig animator, IInputHandler inputHandler)
    {
        _animatorConfig = animator;
        _inputHandler = inputHandler;
        _inputHandler.DownButtonAction.Action += () => SetAnimationBool(AnimationNameType.Crouch,
            !_animatorConfig.PlayerAnimator.GetBool(Animator.StringToHash(AnimationNameType.Crouch.ToString())));
    }
    public void SetAnimationBool(AnimationNameType animationNameType, bool value)
    {
        _animatorConfig.PlayerAnimator.SetBool(Animator.StringToHash(animationNameType.ToString()), value);
    }

    public void SetTrigger(AnimationNameType animationNameType)
    {
        _animatorConfig.PlayerAnimator.SetTrigger(Animator.StringToHash(animationNameType.ToString()));
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
            _currentAnimationValue += _animatorConfig.SpeedRunAnimation * Time.deltaTime;
        }
        else
        {
            _currentAnimationValue -= _animatorConfig.SpeedRunAnimation * Time.deltaTime;
        }

        if (_currentAnimationValue > 1)
        {
            _currentAnimationValue = 1;
        }

        if (_currentAnimationValue < 0)
        {
            _currentAnimationValue = 0;
        }

        _animatorConfig.PlayerAnimator.SetFloat(Animator.StringToHash(AnimationNameType.Run.ToString()), _currentAnimationValue);
    }
}