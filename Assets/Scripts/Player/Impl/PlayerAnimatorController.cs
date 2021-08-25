using PlayerStates;
using UnityEngine;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSpeed;

    private IInputHandler _inputHandler;

    private float _currentAnimationValue;


    public void SetAnimationBool(AnimationNameType animationNameType, bool value)
    {
        animator.SetBool(Animator.StringToHash(animationNameType.ToString()), value);
    }

    public void SetTrigger(AnimationNameType animationNameType)
    {
        animator.SetTrigger(Animator.StringToHash(animationNameType.ToString()));
    }

    private void Update()
    {
        UpdateAnimationState();
    }

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _inputHandler.DownButtonAction.Action += () => SetAnimationBool(AnimationNameType.Crouch,
            !animator.GetBool(Animator.StringToHash(AnimationNameType.Crouch.ToString())));
    }

    private void UpdateAnimationState()
    {
        var moveDirection = _inputHandler.MovementDirection;
        if (moveDirection != 0)
        {
            _currentAnimationValue += animationSpeed * Time.deltaTime;
        }
        else
        {
            _currentAnimationValue -= animationSpeed * Time.deltaTime;
        }

        if (_currentAnimationValue > 1)
        {
            _currentAnimationValue = 1;
        }

        if (_currentAnimationValue < 0)
        {
            _currentAnimationValue = 0;
        }

        animator.SetFloat(Animator.StringToHash(AnimationNameType.Run.ToString()), _currentAnimationValue);
    }
}