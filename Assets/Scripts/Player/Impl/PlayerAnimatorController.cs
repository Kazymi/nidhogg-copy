using PlayerState;
using UnityEngine;
using Zenject;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float animationSpeed;

    private IInputHandler _inputHandler;
    private readonly int _runHash = Animator.StringToHash("Run");
    private readonly int _stunHash = Animator.StringToHash("Stun");
    private readonly int _weaponHash = Animator.StringToHash("Weapon");
    private float _currentAnimationValue;


    public void PlayerTakeWeapon()
    {
        animator.SetBool(_weaponHash, true);
    }

    public void PlayerWithoutWeapon()
    {
        animator.SetBool(_weaponHash, false);
    }

    public void Stun()
    {
        animator.Play(_stunHash);
        animator.SetBool(_stunHash, true);
    }

    public void StunOff()
    {
        animator.SetBool(_stunHash,false);
    }

    private void Update()
    {
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