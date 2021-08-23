using PlayerState;
using UnityEngine;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSpeed;

    private IInputHandler _inputHandler;
    private StateMachine _animationStateMachine;
    private int _runHash = Animator.StringToHash("Run");
    private float _currentAnimationValue;
    private PlayerWithFirearms _stateWithFirearms;
    private PlayerWithoutWeapon _stateWithoutWeapon;

    private void Start()
    {
        _stateWithFirearms = new PlayerWithFirearms(animator);
        _stateWithoutWeapon = new PlayerWithoutWeapon(animator);
        _animationStateMachine = new StateMachine(_stateWithoutWeapon);
    }

    public void PlayerTakeWeapon()
    {
        _animationStateMachine.SetState(_stateWithFirearms);
    }

    public void PlayerWithoutWeapon()
    {
        _animationStateMachine.SetState(_stateWithoutWeapon);
    }

    private void Update()
    {
        _animationStateMachine.Tick();
        UpdateAnimationState();
    }

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
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

        animator.SetFloat(_runHash, _currentAnimationValue);
    }
}