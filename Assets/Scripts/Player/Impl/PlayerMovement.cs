using PlayerState;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float gravity;

    private StateMachine _stateMachine;
    private Vector3 _moveDirection;
    private CharacterController _characterController;
    private IInputHandler _inputHandler;

    public float Speed => speed;
    public IInputHandler InputHandler => _inputHandler;
    public AnimationCurve JumpCurve => jumpCurve;
    public CharacterController CharacterController => _characterController;

    public Vector3 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    [Inject]
    private void Construct(CharacterController characterController, IInputHandler inputHandler)
    {
        _characterController = characterController;
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        StateInitialize();
    }

    private void Update()
    {
        _stateMachine.Tick();
        _moveDirection.y -= gravity;
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(this);

        _stateMachine = new StateMachine(playerMoveState);
    }
}