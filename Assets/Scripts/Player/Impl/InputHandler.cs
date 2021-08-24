using System;
using UnityEngine;
using Zenject;

public class InputHandler : MonoBehaviour, IInputHandler
{
    public event Action Jump;
    public event Action Rolling;

    public event Action RightButtonAction;
    public event Action RightButtonDownAction;
    public event Action LeftButtonAction;
    public event Action LeftButtonDownAction;
    
    private const float _click_threshld = 0.25f;
    private float _timeСlick = 0;
    private KeyCode _lastKey;
    private IKeyBindings _keyBindings;

    public int MovementDirection { get; private set; }

    [Inject]
    private void Construct(IKeyBindings keyBindings)
    {
        _keyBindings = keyBindings;
    }

    private void Start()
    {
        RightButtonAction += () => MovementDirection = 1;
        LeftButtonAction += () => MovementDirection = -1;

        RightButtonDownAction += () =>
        {
            var key = _keyBindings.RightButton;
            DoubleClickCheck(key);
            _lastKey = key;
        };

        LeftButtonDownAction += () =>
        {
            var key = _keyBindings.LeftButton;
            DoubleClickCheck(key);
            _lastKey = key;
        };
    }

    private void Update()
    {
        MovementDirection = 0;
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        if ((Input.GetKey(_keyBindings.RightButton) && Input.GetKey(_keyBindings.LeftButton)) == false)
        {
            if (Input.GetKey(_keyBindings.RightButton))
            {
                RightButtonAction?.Invoke();
            }

            if (Input.GetKey(_keyBindings.LeftButton))
            {
                LeftButtonAction?.Invoke();
            }
        }

        if (Input.GetKeyDown(_keyBindings.RightButton))
        {
            RightButtonDownAction?.Invoke();
        }

        if (Input.GetKeyDown(_keyBindings.LeftButton))
        {
            LeftButtonDownAction?.Invoke();
        }
    }

    private void DoubleClickCheck(KeyCode currentKey)
    {
        if (currentKey != _lastKey)
        {
            return;
        }

        if (Time.time - _timeСlick < _click_threshld)
        {
            Rolling?.Invoke();
        }

        _timeСlick = Time.time;
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(_keyBindings.JumpButton))
        {
            Jump?.Invoke();
        }
    }
}