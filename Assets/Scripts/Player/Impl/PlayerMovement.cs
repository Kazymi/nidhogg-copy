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
    private IInputHandler _inputHandler;

    public float Speed => speed;
    public IInputHandler InputHandler => _inputHandler;
    public AnimationCurve JumpCurve => jumpCurve;
    public bool IsGrounded;

    public Vector3 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    [Inject]
    private void Construct(IInputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        StateInitialize();
    }

    private void Update()
    {
        IsGrounded = GroundCheck();
        Debug.Log(IsGrounded);
        _stateMachine.Tick();
        if (IsGrounded == false)
        {
            _moveDirection.y -= gravity;
        }
        transform.position += _moveDirection*Time.deltaTime;
    }

    private bool GroundCheck()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.1f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void StateInitialize()
    {
        var playerMoveState = new PlayerMoveState(this);

        _stateMachine = new StateMachine(playerMoveState);
    }
}