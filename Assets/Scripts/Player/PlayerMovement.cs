using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private PlayerMovementConfiguration playerMovementConfiguration;

    private PlayerAnimatorController _playerAnimatorController;
    private IInputHandler _inputHandler;

    private Rigidbody _rigidbody;
    private Vector3 _moveVector;
    private float _currentTimeCurve;
    private float _totalTimeCurve;
    private bool _isJumped;
    
    public bool IsGrounded { get; private set; }
    public bool IsJumped
    {
        set
        {
            if (value == false)
            {
                _currentTimeCurve = 999;
            }

            _isJumped = value;
        }
        get => _isJumped;
    }

    public Action<bool> DefaultMovement { get; set; }
    public PlayerMovementConfiguration PlayerMovementConfiguration => playerMovementConfiguration;
    public Rigidbody Rigidbody => _rigidbody;


    [Inject]
    private void Construct(IInputHandler inputHandler, PlayerAnimatorController playerAnimatorController)
    {
        _playerAnimatorController = playerAnimatorController;
        _inputHandler = inputHandler;
    }

    private void Awake()
    {
        _totalTimeCurve = playerMovementConfiguration.JumpCurve
            .keys[playerMovementConfiguration.JumpCurve.keys.Length - 1].time;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        IsGrounded = GroundCheck();
        if (IsGrounded == false)
        {
            _moveVector -= new Vector3(0, playerMovementConfiguration.Gravity, 0);
        }

        Jump();
        _rigidbody.velocity = _moveVector;
    }

    private bool GroundCheck()
    {
        return (Physics.Raycast(playerMovementConfiguration.GroundCheckPosition.position,
            -playerMovementConfiguration.GroundCheckPosition.up, 0.4f));
    }

    public void Rolling()
    {
        _moveVector = new Vector3(
            playerMovementConfiguration.RollingSpeed * -transform.forward.z,
            0f, 0f);
    }

    public void Move(float speedRedux)
    {
        _moveVector = Vector3.zero;
        var inputVector = _inputHandler.MovementDirection;
        Move(inputVector, speedRedux);
    }

    public void ShieldMove()
    {
        _moveVector = new Vector3();
        var moveDir = _inputHandler.MovementDirection;
        if (moveDir == (int) transform.forward.z)
        {
            _moveVector = new Vector3(moveDir, 0, 0);
            _moveVector *= playerMovementConfiguration.ShieldSpeed;
        }
        else
        {
            _playerAnimatorController.AnimationValue = 0;
        }

        _playerAnimatorController.UpdateAnimation();
    }

    public void StartJump()
    {
        if (_isJumped)
        {
            return;
        }

        if (IsGrounded)
        {
            StartCoroutine(EndJump());
            IsJumped = true;
            _playerAnimatorController.SetTrigger(AnimationNameType.Jump.ToString(), false);
            _currentTimeCurve = 0;
        }
    }

    public void Jump()
    {
        if (_currentTimeCurve < _totalTimeCurve)
        {
            _moveVector +=
                new Vector3(0, playerMovementConfiguration.JumpCurve.Evaluate(_currentTimeCurve), 0);
            _currentTimeCurve += Time.deltaTime;
        }
    }


    private IEnumerator EndJump()
    {
        yield return new WaitForSeconds(1f);
        IsJumped = false;
    }

    private void Move(int moveDir, float speedRedux)
    {
        _playerAnimatorController.UpdateAnimation();
        if (moveDir > 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _moveVector = new Vector3(-moveDir, 0, 0);
        }
        else
        {
            if (moveDir < 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                _moveVector = new Vector3(moveDir, 0, 0);
            }
            else
            {
                return;
            }
        }

        _moveVector = transform.TransformDirection(_moveVector);
        _moveVector *= playerMovementConfiguration.Speed * speedRedux;
    }
}